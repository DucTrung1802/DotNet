using System.Collections;

namespace MyCollection
{
    internal enum NodeColor
    {
        BLACK,
        RED,
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
        #region Local variables/constants

        private Node? root;
        private IComparer<T> comparer = default!;
        private int count;

        public int Count { get { return this.count; } }

        internal static bool IsNonNullBlack(Node? node) => node != null && node.IsBlack;

        internal static bool IsNonNullRed(Node? node) => node != null && node.IsRed;

        internal static bool IsNullOrBlack(Node? node) => node == null || node.IsBlack;

        bool ICollection<T>.IsReadOnly => false;

        bool ICollection.IsSynchronized => false;

        object ICollection.SyncRoot => this;

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
            //                  /       \
            //                Uncle     Parent
            //                            \
            //                        Current_Node
            //
            //
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

        internal Node RotationLeft(Node node)
        {
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
                if (this.GetStrategy(node) == Strategy.UNCLE_RED)
                {
                    node.Parent.Recolor();
                    node.Parent.Parent!.Recolor();
                    node = node.Parent;
                }

                if (this.GetStrategy(node) == Strategy.TRIANGLE_LEFT)
                {
                    this.Rotation
                }

                if (this.GetStrategy(node) == Strategy.TRIANGLE_RIGHT)
                {

                }

                if (this.GetStrategy(node) == Strategy.LINE_LEFT)
                {

                }

                if (this.GetStrategy(node) == Strategy.LINE_RIGHT)
                {

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

                // Travel to the suitable NIL node before adding
                while (current_node.Left != null && current_node.Right != null)
                {
                    current_node = comparer.Compare(item, current_node.Item) < 0 ? current_node.Left : current_node.Right;
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

        #endregion

        internal sealed class Node(T item, NodeColor color)
        {
            private Node? parent;
            private Node? left;
            private Node? right;
            private T item = item;
            private NodeColor color = color;

            public T Item { get; set; }

            public Node? Parent { get; set; }

            public Node? Left { get; set; }

            public Node? Right { get; set; }

            public NodeColor Color { get; set; }

            public bool IsBlack => Color == NodeColor.BLACK;

            public bool IsRed => Color == NodeColor.RED;

            public void ColorBlack() => Color = NodeColor.BLACK;

            public void ColorRed() => Color = NodeColor.RED;

            public void Recolor()
            {
                if (Color == NodeColor.RED)
                {
                    Color = NodeColor.BLACK;
                }
                Color = NodeColor.RED;
            }

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
}