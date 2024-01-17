using System.Collections;

namespace MyCollection
{
    public class LinkedList<T> : ICollection<T>, ICollection
    {
        private LinkedListNode<T>? head;
        private int count;

        private void InternalInsertNodeToEmptyList(LinkedListNode<T> new_node)
        {
            new_node.next = new_node;
            new_node.prev = new_node;
            new_node.list = this;
            this.head = new_node;
            this.count = 1;
        }

        private void InternalInsertNodeBefore(LinkedListNode<T> node, LinkedListNode<T> new_node)
        {
            new_node.next = node;
            new_node.prev = node.prev;
            node.prev!.next = new_node;
            node.prev = new_node;
            new_node.list = this;
            this.count++;
        }

        private void ValidateNode(LinkedListNode<T> node)
        {
            ArgumentNullException.ThrowIfNull(node);

            if (node.list != this)
            {
                throw new InvalidOperationException(nameof(node));
            }
        }

        public LinkedList() { }

        public LinkedList(IEnumerable<T> collection)
        {
            ArgumentNullException.ThrowIfNull(collection);

            foreach (T item in collection)
            {
                AddLast(item);
            }
        }

        public LinkedListNode<T> AddFirst(T item)
        {
            LinkedListNode<T> new_node = new LinkedListNode<T>(item);
            this.AddFirst(new_node);

            return this.head!;
        }

        public void AddFirst(LinkedListNode<T> new_node)
        {
            if (this.head == null)
            {
                this.InternalInsertNodeToEmptyList(new_node);
            }
            else
            {
                this.InternalInsertNodeBefore(this.head, new_node);
            }
        }

        public LinkedListNode<T> AddLast(T item)
        {
            LinkedListNode<T> new_node = new LinkedListNode<T>(item);
            this.AddLast(new_node);

            return new_node;
        }

        public void AddLast(LinkedListNode<T> new_node)
        {
            if (this.head == null)
            {
                this.InternalInsertNodeToEmptyList(new_node);
            }
            else
            {
                this.InternalInsertNodeBefore(this.head, new_node);
            }
        }

        public LinkedListNode<T> AddAfter(LinkedListNode<T> node, T value)
        {
            this.ValidateNode(node);
            LinkedListNode<T> new_node = new LinkedListNode<T>(node.list!, value);
            this.InternalInsertNodeBefore(node.next!, new_node);
            return new_node;
        }

        public void AddAfter(LinkedListNode<T> node, LinkedListNode<T> new_node)
        {
            this.ValidateNode(node);
            this.InternalInsertNodeBefore(node.next!, new_node);
        }

        public LinkedListNode<T> AddBefore(LinkedListNode<T> node, T value)
        {
            this.ValidateNode(node);
            LinkedListNode<T> new_node = new LinkedListNode<T>(node.list!, value);
            this.InternalInsertNodeBefore(node, new_node);
            return new_node;
        }

        public void AddBefore(LinkedListNode<T> node, LinkedListNode<T> new_node)
        {
            this.ValidateNode(node);
            this.InternalInsertNodeBefore(node, new_node);
        }

        public int Count => throw new NotImplementedException();

        public bool IsReadOnly => throw new NotImplementedException();

        public bool IsSynchronized => throw new NotImplementedException();

        public object SyncRoot => throw new NotImplementedException();

        public void Add(T item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(T item)
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

        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable<T>)this).GetEnumerator();


        public bool Remove(T item)
        {
            throw new NotImplementedException();
        }


        public IEnumerator<T> GetEnumerator()
        {
            if (this.head == null)
            {
                throw new ArgumentNullException(nameof(this.head));
            }
            else
            {
                LinkedListNode<T>? temp_node = this.head;
                for (int i = 0; i < this.count; i++)
                {

                }
            }
        }


    }

    public sealed class LinkedListNode<T>
    {
        internal LinkedList<T>? list;
        internal LinkedListNode<T>? next;
        internal LinkedListNode<T>? prev;
        internal T item;

        public LinkedListNode(T value)
        {
            item = value;
        }

        internal LinkedListNode(LinkedList<T> list, T value)
        {
            this.list = list;
            item = value;
        }

        public LinkedList<T>? List
        {
            get { return list; }
        }
    }

    class Program
    {
        public static void Main()
        {
            LinkedList<int> my_linkedlist = new LinkedList<int>();
            my_linkedlist.AddLast(2);
            my_linkedlist.AddLast(new LinkedListNode<int>(3));
            my_linkedlist.AddFirst(1);
            my_linkedlist.AddLast(100);
            my_linkedlist.AddFirst(new LinkedListNode<int>(0));

            foreach (var item in my_linkedlist)
            {
                Console.WriteLine(item);
            }
        }
    }
}