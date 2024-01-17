using System.Collections;

namespace MyCollection
{
    public class LinkedList<T> : ICollection<T>, ICollection
    {
        internal LinkedListNode<T>? head;
        internal int count;

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

        internal void InternalRemoveNode(LinkedListNode<T> node)
        {
            if (node.next == node)
            {
                // Remove the only node in the list
                head = null;
            }
            else
            {
                node.prev!.next = node.next;
                node.next!.prev = node.prev;
                if (this.head == node)
                {
                    this.head = node.next;
                }
            }
            node.Invalidate();
            this.count--;
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
            this.head = new_node;

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
            this.head = new_node;
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

        public void Clear()
        {
            LinkedListNode<T>? current_node = this.head;
            while (current_node != null)
            {
                LinkedListNode<T> temp_node = current_node;
                current_node = current_node.next;
                temp_node.Invalidate();
            }

            this.head = null;
            this.count = 0;
        }

        public bool Contains(T value)
        {
            return Find(value) != null;
        }

        public LinkedListNode<T>? Find(T value)
        {
            LinkedListNode<T>? current_node = this.head;
            EqualityComparer<T> comparator = EqualityComparer<T>.Default;
            if (current_node != null)
            {
                if (value != null)
                {
                    do
                    {
                        if (comparator.Equals(value, current_node!.item))
                        {
                            return current_node;
                        }
                        current_node = current_node.next;
                    } while (current_node != this.head);
                }
                else
                {
                    do
                    {
                        if (current_node!.item == null)
                        {
                            return current_node;
                        }
                        current_node = current_node.next;
                    } while (current_node != this.head);
                }
            }
            return null;
        }

        public LinkedListNode<T>? FindLast(T value)
        {
            if (head == null) return null;

            LinkedListNode<T>? last = head.prev;
            LinkedListNode<T>? node = last;
            EqualityComparer<T> c = EqualityComparer<T>.Default;
            if (node != null)
            {
                if (value != null)
                {
                    do
                    {
                        if (c.Equals(node!.item, value))
                        {
                            return node;
                        }

                        node = node.prev;
                    } while (node != last);
                }
                else
                {
                    do
                    {
                        if (node!.item == null)
                        {
                            return node;
                        }
                        node = node.prev;
                    } while (node != last);
                }
            }
            return null;
        }

        public int Count => throw new NotImplementedException();

        public bool IsReadOnly => throw new NotImplementedException();

        public bool IsSynchronized => throw new NotImplementedException();

        public object SyncRoot => throw new NotImplementedException();


        public void CopyTo(T[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(Array array, int index)
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable<T>)this).GetEnumerator();

        public bool Remove(T value)
        {
            LinkedListNode<T>? node = Find(value);
            if (node != null)
            {
                InternalRemoveNode(node);
                return true;
            }
            return false;
        }

        public void Remove(LinkedListNode<T> node)
        {
            ValidateNode(node);
            this.InternalRemoveNode(node);
        }

        public void RemoveFirst()
        {
            if (this.head != null)
            {
                this.InternalRemoveNode(this.head);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public void RemoveLast()
        {
            if (this.head != null)
            {
                this.InternalRemoveNode(this.head.prev!);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public IEnumerator<T?> GetEnumerator()
        {
            if (this.head != null)
            {
                LinkedListNode<T>? temp_node = this.head;
                for (int i = 0; i < this.count; i++)
                {
                    yield return temp_node!.item;
                    temp_node = temp_node.next;
                }
            }
        }

        void ICollection<T>.Add(T value)
        {
            AddLast(value);
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

        public LinkedListNode<T>? Next
        {
            get { return next == null || next == list!.head ? null : next; }
        }

        public LinkedListNode<T>? Previous
        {
            get { return prev == null || this == list!.head ? null : prev; }
        }

        public T Value
        {
            get { return item; }
            set { item = value; }
        }

        public ref T ValueRef => ref item;

        internal void Invalidate()
        {
            list = null;
            next = null;
            prev = null;
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

            my_linkedlist.Clear();

            foreach (var item in my_linkedlist)
            {
                Console.WriteLine(item);
            }

            LinkedListNode<int> last_node = my_linkedlist.AddLast(4);
            LinkedListNode<int> first_node = my_linkedlist.AddFirst(1);
            my_linkedlist.AddBefore(last_node, 3);
            my_linkedlist.AddAfter(first_node, 2);

            foreach (var item in my_linkedlist)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine(my_linkedlist.Find(3));
            Console.WriteLine(my_linkedlist.Find(10));

            Console.WriteLine(my_linkedlist.Contains(2));

            my_linkedlist.Remove(3);
            my_linkedlist.RemoveFirst();
            my_linkedlist.RemoveLast();

            foreach (var item in my_linkedlist)
            {
                Console.WriteLine(item);
            }
        }
    }
}