using System.Collections;

namespace MyCollection
{
    internal enum NodeColor
    {
        BLACK = 'B',
        RED = 'R',
    }

    internal enum Strategy : byte
    {
        UNCLE_RED,
        TRIANGLE_LEFT,
        TRIANGLE_RIGHT,
        LINE_LEFT,
        LINE_RIGHT,
    }

    public class SortedSet<T> : ISet<T>, ICollection<T>, ICollection
    {

        private Node? root;
        private IComparer<T> comparer = Comparer<T>.Default;
        private int count;
        private int deep;
        private int totalConsoleWidth = 250;

        public int Count { get { return this.count; } }

        internal static bool IsNonNullBlack(Node? node) => node != null && node.IsBlack;

        internal static bool IsNonNullRed(Node? node) => node != null && node.IsRed;

        internal static bool IsNullOrBlack(Node? node) => node == null || node.IsBlack;

        bool ICollection<T>.IsReadOnly => false;

        bool ICollection.IsSynchronized => false;

        object ICollection.SyncRoot => this;

        public SortedSet()
        {
        }

        public SortedSet(IEnumerable<T> collection)
        {
            foreach (var item in collection)
            {
                this.Add(item);
            }
        }

        public bool Contains(T item) => FindNode(item) != null;

        internal Node? FindNode(T item)
        {
            Node? current = this.root;
            while (current != null)
            {
                int order = comparer.Compare(item, current.Item);
                if (order == 0)
                {
                    return current;
                }
                current = order < 0 ? current.Left : current.Right;
            }

            return null;
        }

        internal NodeColor GetUncleColor(Node node)
        {
            if (node == null || node.Parent == null || node.Parent.Parent == null)
            {
                throw new ArgumentNullException("node.Parent.Parent is NULL");
            }

            //      Left Uncle
            // 
            //                  Grandparent
            //                   /       \
            //                 Uncle     Parent
            //                             \
            //                         Current_Node
            //
            //      Right Uncle
            //
            //                  Grandparent
            //                   /       \
            //                Parent    Uncle
            //                  /          
            //            Current_Node
            //
            //

            // Get the color of the Left Uncle
            if (node.Parent.Parent.Left == node.Parent)
            {
                if (IsNullOrBlack(node.Parent.Parent.Right))
                {
                    return NodeColor.BLACK;
                }
                return NodeColor.RED;
            }

            // Get the color of the Left Uncle
            if (node.Parent.Parent.Right == node.Parent)
            {
                if (IsNullOrBlack(node.Parent.Parent.Left))
                {
                    return NodeColor.BLACK;
                }
                return NodeColor.RED;
            }

            return default;
        }

        internal Strategy GetStrategy(Node node)
        {
            if (node == null || node.Parent == null || node.Parent.Parent == null)
            {
                throw new ArgumentNullException("node.Parent.Parent is NULL");
            }

            if (this.GetUncleColor(node) == NodeColor.RED)
            {
                return Strategy.UNCLE_RED;
            }

            if (node.Parent == node.Parent.Parent.Left && node == node.Parent.Right)
            {
                return Strategy.TRIANGLE_LEFT;
            }

            if (node.Parent == node.Parent.Parent.Right && node == node.Parent.Left)
            {
                return Strategy.TRIANGLE_RIGHT;
            }

            if (node.Parent == node.Parent.Parent.Left && node == node.Parent.Left)
            {
                return Strategy.LINE_LEFT;
            }

            if (node.Parent == node.Parent.Parent.Right && node == node.Parent.Right)
            {
                return Strategy.LINE_RIGHT;
            }

            return default;
        }

        internal Node RotateLeft(Node node)
        {
            // 
            //  RotateLeft: Maximum 6 Operations since each line contains 2 relationships.
            // 
            //         Parent                           Parent
            //            |                                |
            //         DownNode (node)                   UpNode
            //          /     \          =>              /   \
            //        ...    UpNode               DownNode    ...
            //                /  \                 /   \
            //           SubNode?  ...            ...  SubNode?
            //
            //     SubNode maybe NULL
            //
            if (node.Right != null)
            {
                Node? ParentNode = node.Parent;
                Node DownNode = node;
                Node UpNode = node.Right;
                Node? SubNode = node.Right.Left;

                if (SubNode != null)
                {
                    SubNode.Parent = DownNode;
                }

                UpNode.Left = DownNode;
                DownNode.Right = SubNode;
                DownNode.Parent = UpNode;

                if (ParentNode != null)
                {
                    UpNode.Parent = ParentNode;

                    if (DownNode == ParentNode.Left)
                    {
                        ParentNode.Left = UpNode;
                    }
                    else if (DownNode == ParentNode.Right)
                    {
                        ParentNode.Right = UpNode;
                    }
                }
                else
                {
                    UpNode.Parent = null;
                    this.root = UpNode;
                }

                return DownNode;
            }

            return node;
        }

        internal Node RotateRight(Node node)
        {
            // 
            //  RotateRight: Maximum 6 Operations since each line contains 2 relationships.
            // 
            //         Parent                           Parent
            //            |                                |
            //         DownNode (node)                   UpNode
            //          /     \          =>              /   \
            //      UpNode    ...                      ...   DownNode    
            //       /  \                                     /   \
            //    ...   SubNode?                        SubNode?  ...
            //
            //     SubNode maybe NULL
            //
            if (node.Left != null)
            {
                Node ParentNode = node.Parent!;
                Node DownNode = node;
                Node UpNode = node.Left;
                Node? SubNode = node.Left.Right;

                if (SubNode != null)
                {
                    SubNode.Parent = DownNode;
                }

                UpNode.Right = DownNode;
                DownNode.Left = SubNode;
                DownNode.Parent = UpNode;

                if (ParentNode != null)
                {
                    UpNode.Parent = ParentNode;

                    if (DownNode == ParentNode.Right)
                    {
                        ParentNode.Right = UpNode;
                    }
                    else if (DownNode == ParentNode.Left)
                    {
                        ParentNode.Left = UpNode;
                    }
                }
                else
                {
                    UpNode.Parent = null;
                    this.root = UpNode;
                }

                return DownNode;
            }

            return node;
        }

        internal void BalanceTree(Node node)
        {
            if (node == null || node.Parent == null || node.Parent.Parent == null)
            {
                return;
            }

            while (node.Parent != null && node.Color == NodeColor.RED && node.Parent.Color == NodeColor.RED)
            {
                Strategy current_strategy = this.GetStrategy(node);
                if (current_strategy == Strategy.UNCLE_RED)
                {
                    node.Parent.Parent!.Left!.Recolor();
                    node.Parent.Parent!.Right!.Recolor();
                    if (node.Parent.Parent != this.root)
                    {
                        node.Parent.Parent!.Recolor();
                    }
                    node = node.Parent.Parent!;
                }

                // 
                //  TRIANGLE_LEFT (BLACK Uncle) => RotateLeft
                // 
                //                  Grandparent
                //                   /       \
                //                Parent    Uncle
                //                   \          
                //                Current_Node
                //
                else if (current_strategy == Strategy.TRIANGLE_LEFT)
                {
                    // Change to Parent
                    // Rotate opposite with current node
                    node = node.Parent;
                    node = this.RotateLeft(node);
                }

                // 
                //  TRIANGLE_RIGHT (BLACK Uncle) => RotateRight
                // 
                //                  Grandparent
                //                   /       \
                //                Uncle    Parent
                //                           /          
                //                      Current_Node
                //
                else if (current_strategy == Strategy.TRIANGLE_RIGHT)
                {
                    // Change to Parent
                    // Rotate opposite with current node
                    node = node.Parent;
                    node = this.RotateRight(node);
                }

                // 
                //  LINE_LEFT (BLACK Uncle) => RotateRight
                // 
                //                  Grandparent
                //                   /       \
                //                Parent    Uncle
                //                  /          
                //             Current_Node
                //
                else if (current_strategy == Strategy.LINE_LEFT)
                {
                    // Change to Grandparent
                    // Rotate opposite with current node
                    if (node.Parent!.Parent != null)
                    {
                        node = node.Parent.Parent;
                        node = this.RotateRight(node);
                        node.Recolor();
                        node.Parent!.Recolor();
                    }
                }

                // 
                //  LINE_RIGHT (BLACK Uncle) => RotateLeft
                // 
                //                  Grandparent
                //                   /       \
                //                Uncle    Parent
                //                            \          
                //                        Current_Node
                //
                else if (current_strategy == Strategy.LINE_RIGHT)
                {
                    // Change to Grandparent
                    // Rotate opposite with current node
                    if (node.Parent!.Parent != null)
                    {
                        node = node.Parent.Parent;
                        node = this.RotateLeft(node);
                        node.Recolor();
                        node.Parent!.Recolor();
                    }
                }
            }
        }

        public bool Add(T item)
        {
            if (!this.Contains(item))
            {
                Node new_node = new Node(item, NodeColor.RED);

                // Case 1: The Tree is NULL => Just add and color BLACK
                if (this.root == null)
                {
                    new_node.ColorBlack();
                    this.root = new_node;
                    this.count = 1;
                    return true;
                }

                Node? current_node = this.root;

                // Case 2: The Tree root has no Left child => Add to Left child
                if (current_node.Left == null
                    && comparer.Compare(new_node.Item, current_node.Item) < 0)
                {
                    current_node.Left = new_node;
                    new_node.Parent = current_node;
                    this.count++;
                    return true;
                }

                // Case 3: The Tree root has no Right child => Add to Right child
                else if (current_node.Right == null
                    && comparer.Compare(new_node.Item, current_node.Item) < 0)
                {
                    current_node.Right = new_node;
                    new_node.Parent = current_node;
                    this.count++;
                    return true;
                }

                // Travel to the suitable NIL node before 
                while ((current_node.Left != null && comparer.Compare(item, current_node.Item) < 0)
                    || (current_node.Right != null && comparer.Compare(item, current_node.Item) > 0))
                {
                    if (current_node.Left != null && comparer.Compare(item, current_node.Item) < 0)
                    {
                        current_node = current_node.Left;
                    }

                    if (current_node.Right != null && comparer.Compare(item, current_node.Item) > 0)
                    {
                        current_node = current_node.Right;
                    }
                }

                // Add node
                if (comparer.Compare(item, current_node.Item) < 0)
                {
                    current_node.Left = new_node;
                    new_node.Parent = current_node;
                    this.count++;
                }
                else
                {
                    current_node.Right = new_node;
                    new_node.Parent = current_node;
                    this.count++;
                }

                // Rebalance the Tree
                this.BalanceTree(new_node);
                return true;
            }

            return false;
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(Array array, int index)
        {
            throw new NotImplementedException();
        }

        public void ExceptWith(IEnumerable<T> other)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public void IntersectWith(IEnumerable<T> other)
        {
            throw new NotImplementedException();
        }

        public bool IsProperSubsetOf(IEnumerable<T> other)
        {
            throw new NotImplementedException();
        }

        public bool IsProperSupersetOf(IEnumerable<T> other)
        {
            throw new NotImplementedException();
        }

        public bool IsSubsetOf(IEnumerable<T> other)
        {
            throw new NotImplementedException();
        }

        public bool IsSupersetOf(IEnumerable<T> other)
        {
            throw new NotImplementedException();
        }

        public bool Overlaps(IEnumerable<T> other)
        {
            throw new NotImplementedException();
        }

        public bool Remove(T item)
        {
            throw new NotImplementedException();
        }

        public bool SetEquals(IEnumerable<T> other)
        {
            throw new NotImplementedException();
        }

        public void SymmetricExceptWith(IEnumerable<T> other)
        {
            throw new NotImplementedException();
        }

        public void UnionWith(IEnumerable<T> other)
        {
            throw new NotImplementedException();
        }

        void ICollection<T>.Add(T item)
        {
            throw new NotImplementedException();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        // Print the tree diagram
        int max_pad = 100;

        private void PrintPad(int number)
        {
            for (int i = 0; i < number; i++)
            {
                Console.Write(' ');
            }
        }

        private List<List<string>>? GetLevels()
        {
            if (this.root != null)
            {
                this.deep = this.GetDepth();
                List<List<string>> result = new List<List<string>>();
                for (int i = 0; i < this.deep; i++)
                {
                    result.Add(new List<string>());
                }
                GetLevelsHelper(this.root, 0, result);
                return result;
            }
            return null;
        }

        private void GetLevelsHelper(Node? node, int level, List<List<string>> result)
        {
            if (node == null)
            {
                result[level].Add("NIL");
                if (level < this.deep - 1)
                {
                    result[level + 1].Add("NIL");
                    result[level + 1].Add("NIL");
                }
            }
            else
            {
                result[level].Add(node.Item.ToString() + (char)node.Color);

                if (level < this.deep - 1)
                {
                    GetLevelsHelper(node.Left, level + 1, result);
                    GetLevelsHelper(node.Right, level + 1, result);
                }
            }
        }

        public void ShowTreeDiagram()
        {
            List<List<string>> levels = this.GetLevels();
            if (levels != null)
            {
                bool lastLineIsAllNil = true;
                foreach (var value in levels[levels.Count - 1])
                {
                    if (value != null)
                    {
                        lastLineIsAllNil = false;
                        break;
                    }
                }

                // Print levels with proper padding
                for (int i = 0; i < levels.Count - (lastLineIsAllNil ? 1 : 0); i++)
                {
                    int totalNodes = levels[i].Count;
                    int spaceBetweenNodes = this.totalConsoleWidth / (totalNodes + 1);

                    foreach (var value in levels[i])
                    {
                        string nodeValue = value != null ? value.ToString() : "NIL";
                        Console.Write(nodeValue.PadLeft(spaceBetweenNodes / 2).PadRight(spaceBetweenNodes / 2));
                    }
                    Console.WriteLine();
                }
            }
        }

        public int GetDepth()
        {
            return GetDepthHelper(this.root);
        }

        private int GetDepthHelper(Node? node)
        {
            if (node == null)
            {
                return 0;
            }

            int leftDepth = GetDepthHelper(node.Left);
            int rightDepth = GetDepthHelper(node.Right);

            // The depth of the tree is the maximum depth of its left and right subtrees, plus 1 for the current node
            return Math.Max(leftDepth, rightDepth) + 1;
        }

        internal sealed class Node
        {
            public Node(T item, NodeColor color)
            {
                Item = item;
                Color = color;
            }

            public T Item { get; set; }

            public Node? Parent { get; set; }

            public Node? Left { get; set; }

            public Node? Right { get; set; }

            public NodeColor Color { get; set; }

            public bool IsBlack => Color == NodeColor.BLACK;

            public bool IsRed => Color == NodeColor.RED;

            public void ColorBlack() => Color = NodeColor.BLACK;

            public void ColorRed() => Color = NodeColor.RED;

            public void Recolor() => Color = Color == NodeColor.RED ? NodeColor.BLACK : NodeColor.RED;

            internal Node Rotation(Strategy strategy)
            {

                return this;
            }

            internal Node RotationLeft()
            {
                return this;
            }

            internal Node RotationRight()
            {
                return this;
            }
        }
    }

    class Program
    {
        public static void Main()
        {
            SortedSet<int> my_set = new SortedSet<int>([43, 12, 76, 29, 58, 91, 34, 72, 19, 50, 87, 5, 23, 69, 81, 37, 99, 64, 10, 53]);
            my_set.ShowTreeDiagram();
        }
    }
}
