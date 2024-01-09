namespace MyCollection
{
    class List<T>
    {
        private int _Capacity = 1;
        private int _Count = 0;
        public int Capacity { set; get; }
        public int Count { get; }
        public object[]? Item;

        public void Add(T item)
        {

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
    }

    class Program
    {
        public static void Main()
        {
            var mylist = new List<int>();
            //mylist.Capacity = -1;
            //Console.WriteLine(mylist.Capacity);
            //mylist.Add(2);
        }
    }
}