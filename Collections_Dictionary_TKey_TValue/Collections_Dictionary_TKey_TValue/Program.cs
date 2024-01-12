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
        int[] _Buckets;
        Entry<TKey, TValue>[] _Entries;
        int _Free_list;
        int _Free_count;
        int _Count;
        int _Size;

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
            if (this._Count + number_of_new_pairs > this._Size)
            {
                this.Resize();
            }
        }

        private void Resize()
        {
            this._Size = this.ExpandPrime(this._Size);
            int[] new_bucket = new int[this._Size];
            Entry<TKey, TValue>[] new_entries = new Entry<TKey, TValue>[this._Size];

            if (this._Count > 0)
            {
                // commpute new hascode
                // rearrange to new dictionary
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
            this._Size = 0;
        }

        public Dictionary(IDictionary<TKey, TValue> keyValuePairs)
        {
        }

        public Dictionary(IDictionary<TKey, TValue> keyValuePairs, IEqualityComparer<TKey> equalityComparer)
        {

        }

        public Dictionary(IEqualityComparer<TKey> equalityComparer)
        {

        }

        public Dictionary(int capacity)
        {

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

