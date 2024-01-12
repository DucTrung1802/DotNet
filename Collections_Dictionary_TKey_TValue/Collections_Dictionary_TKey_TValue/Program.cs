using System.Runtime.Serialization;

namespace MyCollection
{
    struct Entry<TKey, TValue>
    {
        TKey _Key;
        TValue _Value;
        int _Hashcode;
        int _Next;
    }

    class Dictionary<TKey, TValue>
    {
        // Private Fields and Properties
        private int[] _Buckets;
        private Entry<TKey, TValue>[] _Entries;
        private int _Free_list;
        private int _Free_count;
        private int _Count;
        private int _Capacity;

        private IEqualityComparer<TKey>? _Comparer;

        // Public Fields and Properties
        //public TValue this[TKey key] { get; set; }


        // Private Methods

        private bool IsPrime(int number)
        {
            if (number == 0 || number == 1) return false;

            for (int i = 2; i <= Math.Sqrt(number); i++)
            {
                if (number % i == 0)
                {
                    return false;
                }
            }
            return true;
        }
        private int ExpandPrime(int count)
        {
            count *= 2;
            for (int i = count; true; i++)
            {
                if (this.IsPrime(i))
                {
                    return i; // new size
                }
            }
        }

        private void CheckResize(int number_of_new_pairs)
        {
            if (this._Count + number_of_new_pairs > this._Capacity)
            {
                this.SetCapacity(this.ExpandPrime((this._Count + number_of_new_pairs) * 2));
                this.Resize();
            }
        }

        private void SetCapacity(int capacity)
        {
            if (capacity < this._Count)
            {
                throw new ArgumentOutOfRangeException();
            }

            this._Capacity = capacity;
        }

        private void Resize()
        {
            int[] new_bucket = new int[this._Capacity];
            Entry<TKey, TValue>[] new_entries = new Entry<TKey, TValue>[this._Capacity];

            if (this._Count > 0)
            {
                // commpute new hascode
                // rearrange to new arrays (buckets and entries)
                // assign to new arrays (buckets and entries)
            }
        }

        // Public Methods
        public Dictionary()
        {
            this._Buckets = new int[0];
            this._Entries = new Entry<TKey, TValue>[0];
            this._Free_list = -1;
            this._Free_count = -1;
            this._Count = 0;
            this._Capacity = 0;
        }

        public Dictionary(IDictionary<TKey, TValue> keyValuePairs)
        {
            this.CheckResize(keyValuePairs.Count);
        }

        public Dictionary(IDictionary<TKey, TValue> keyValuePairs, IEqualityComparer<TKey> equalityComparer)
        {
            this.CheckResize(keyValuePairs.Count);
        }

        public Dictionary(IEqualityComparer<TKey> equalityComparer)
        {

        }

        public Dictionary(int capacity)
        {
            this.SetCapacity(capacity);
        }

        public Dictionary(int capacity, IEqualityComparer<TKey> equalityComparer)
        {

        }

        public Dictionary(SerializationInfo serializationEntries, StreamingContext streamingContext)
        {

        }

        public void Add(TKey key, TValue value)
        {

        }
    }
    class Program
    {
        public static void Main()
        {
            Dictionary<string, int> MyDictionary = new Dictionary<string, int>();
            int modulus = 15;
            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine("{0} % {1} = {2}", i, modulus, (i % modulus));
            }
        }
    }
}

