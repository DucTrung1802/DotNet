using System.Collections;

namespace MyCollection
{
    public class LinkedList<T> : ICollection<T>, ICollection
    {
        private LinkedListNode<T>? head;
        private int count;

        private void AddNodeToEmptyLinkedList(LinkedListNode<T> new_node)
        {
            this.head = new_node;
            new_node.list = this;
            this.count = 1;
        }

        private void ValidateNode(LinkedListNode<T> node)
        {
            ArgumentNullException.ThrowIfNull(node);

            if (node.list != this)
            {
                throw new InvalidOperationException();
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

        public LinkedListNode<T> AddFrist(T item)
        {
            LinkedListNode<T> new_node = new LinkedListNode<T>(item);
            if (this.head == null)
            {
                this.AddNodeToEmptyLinkedList(new_node);
            }
            else
            {
                new_node.next = this.head;
                this.head.prev = new_node;
                new_node.list = this;
                this.head = new_node;
                this.count++;
            }

            return this.head!;
        }

        public LinkedListNode<T> AddLast(T item)
        {
            LinkedListNode<T> new_node = new LinkedListNode<T>(item);
            if (this.head == null)
            {
                this.AddNodeToEmptyLinkedList(new_node);
            }
            else
            {
                LinkedListNode<T> current_node = this.head;
                while (current_node.next != null)
                {
                    current_node = current_node.next;
                }
                new_node.list = this;
                new_node.prev = current_node;
                current_node.next = new_node;
                this.count++;
            }

            return new_node;
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
                while (temp_node != null)
                {
                    yield return temp_node.item;
                    temp_node = temp_node.next;
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
            my_linkedlist.AddLast(3);
            my_linkedlist.AddFrist(1);
            my_linkedlist.AddLast(100);
            my_linkedlist.AddFrist(0);

            foreach (var item in my_linkedlist)
            {
                Console.WriteLine(item);
            }
        }
    }
}