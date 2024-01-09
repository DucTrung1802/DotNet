using System.Collections;

namespace MyCollection
{
    class List<T> : IEnumerable<T>
    {
        private int _Capacity = 0;
        private int _Count = 0;
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
        private object[]? Items;

        private void InitializeList(int capacity = 0)
        {
            this.Items = new object[capacity];
        }

        private void ReallocateList(int capacity)
        {
            object?[]? new_array = new object[capacity];
            if (this.Items is not null)
            {
                this.Items.CopyTo(new_array, 0);
            }
            this.Items = (object[])new_array;
        }

        public List()
        {
            this.InitializeList();
        }

        public List(IEnumerable<T> collection)
        {
            this.Capacity = collection.Count();
            this._Count = this.Capacity;
            this.Items = (object[])collection.Select(item => (object?)item).ToArray();
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
                if (this.Items is null)
                {
                    this._Capacity = 4;
                }
                this.ReallocateList(this.Capacity * 2);
            }
            this._Count++;
            this.Items[this._Count - 1] = (object)item;
        }

        public void AddRange(IEnumerable<T> items)
        {

        }

        public int BinarySearch(T item)
        {
            return 0;
        }

        public void Clear()
        {

        }

        public bool Contains(T item)
        {
            return false;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {

        }

        public bool Exists(Predicate<T> predicate)
        {
            return false;
        }

        public T Find(Predicate<T> predicate)
        {
            Object result = new Object();
            return (T)result;
        }

        public void ForEach(Action<T> action)
        {

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

        public void Sort(IComparer<T> comparer)
        {

        }

        public object[] ToArray(object obj)
        {
            var myobj = new Object[1];
            return myobj;
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
                yield return (T)this.Items[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }

    class Program
    {
        public static void Main()
        {
            var mylist = new List<int>([1, 2, 3, 4]);
            mylist.Add(5);
            mylist.Add(6);
            foreach (var item in mylist)
            {
                Console.WriteLine(item);
            }
        }
    }
}