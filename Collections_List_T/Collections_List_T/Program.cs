using System.Collections;

namespace MyCollection
{
    class List<T> : Comparer<T>, IEnumerable<T>
    {
        // Private Fields and Properties
        private int _Capacity = 0;
        private int _Count = 0;
        private T[]? Items;

        // Public Fields and Properties
        public int Capacity
        {
            set
            {
                if (value < this._Count)
                {
                    throw new ArgumentOutOfRangeException("capacity was less than the current size.");
                }
                else
                {
                    this._Capacity = value;
                }
            }
            get
            {
                return this._Capacity;
            }
        }
        public int Count
        {
            get
            {
                return this._Count;
            }
        }

        public T this[int index]
        {
            set
            {
                this.Items[index] = value;
            }

            get
            {
                return (T)this.Items[index];
            }
        }

        // Private Methods
        private void InitializeList(int capacity = 0)
        {
            this.Items = new T[capacity];
        }

        private void ReallocateList(int capacity)
        {
            T[] new_array = new T[capacity];
            if (this.Items is not null)
            {
                this.Items.CopyTo(new_array, 0);
            }
            this.Items = new_array;
            this._Capacity = capacity;
        }

        // Public Methods
        public List()
        {
            this.InitializeList();
        }

        public List(IEnumerable<T> collection)
        {
            this.Capacity = collection.Count();
            this._Count = this.Capacity;
            this.Items = collection.Select(item => item).ToArray();
        }

        public List(int capacity)
        {
            this.Capacity = capacity;
            this.InitializeList(this._Capacity);
        }

        public void Add(T item)
        {
            if (this._Count == this._Capacity)
            {
                if (this._Count == 0)
                {
                    this.ReallocateList(4);
                }
                else
                {
                    this.ReallocateList(this.Capacity * 2);
                }
            }
            this.Items[this._Count] = item;
            this._Count++;
        }

        public void AddRange(IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                this.Add(item);
            }
        }

        public int BinarySearch(T item)
        {
            return 0;
        }

        public int BinarySearch(T item, IComparer<T> comparer)
        {
            return 0;
        }

        public int BinarySearch(int index, int count, T item, IComparer<T> comparer)
        {
            return 0;
        }

        public void Clear()
        {
            this.Items = null;
            this._Count = 0;
        }

        public bool Contains(T item)
        {
            for (int i = 0; i < _Count; i++)
            {
                if (Items[i].Equals(item))
                {
                    return true;
                }
            }
            return false;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {

        }

        public bool Exists(Predicate<T> match)
        {
            for (int i = 0; i < _Count; i++)
            {
                if (match(Items[i]))
                {
                    return true;
                }
            }
            return false;
        }

        public T? Find(Predicate<T> match)
        {
            for (int i = 0; i < _Count; i++)
            {
                if (match(Items[i]))
                {
                    return Items[i];
                }
            }
            return default;
        }

        public void ForEach(Action<T> action)
        {
            foreach (var item in this.Items)
            {

            }
        }

        public int IndexOf(T item)
        {
            return 0;
        }

        public void Insert(int index, T item)
        {

        }

        public void InsertRange(int index, IEnumerable<T> items)
        {

        }

        public bool Remove(T item)
        {
            return false;
        }

        public void RemoveRange(int from_index, int to_index)
        {

        }

        public void Reverse()
        {

        }

        public void Sort()
        {
            Array.Sort(this.Items);
        }

        public void Sort(IComparer<T> comparer)
        {

        }

        public T[] ToArray(T input)
        
            return ;
        }

        public string ToString()
        {
            return "";
        }

        public void TrimToSize()
        {

        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < this.Count; i++)
            {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                yield return (T)this.Items[i];
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public override int Compare(T? x, T? y)
        {
            throw new NotImplementedException();
        }
    }

    class Program
    {
        public static void Main()
        {
            var mylist = new List<int>([1, 2, 5, 3, 4]);
            foreach (var i in mylist)
            {
                Console.WriteLine(i);
            }
            mylist.Sort();
            foreach (var i in mylist)
            {
                Console.WriteLine(i);
            }
        }
    }
}