using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace MyCollection
{
    struct Entry<TKey, TValue>
    {
        public TKey _Key;
        public TValue _Value;
        public int _Hashcode;
        public int _Next;

        public Entry()
        {
            this._Hashcode = 0;
            this._Next = -1;
        }

    }

    class Dictionary<TKey, TValue> : IDictionary<TKey, TValue>, IDictionary
    {
        // Private Fields and Properties
        private int[] _Buckets;
        private Entry<TKey, TValue>[] _Entries;
        private int _Free_list;     // Index of first free slot
        private int _Count;         // Current number of pairs
        private int _Capacity;      // Maximum slot in current dictionary

        private IEqualityComparer<TKey>? _Comparer;

        // Public Fields and Properties
        public int Count { get { return _Count; } }

        public ICollection<TKey> Keys => throw new NotImplementedException();

        public ICollection<TValue> Values => throw new NotImplementedException();

        public bool IsReadOnly => throw new NotImplementedException();

        public bool IsFixedSize => throw new NotImplementedException();

        ICollection IDictionary.Keys => throw new NotImplementedException();

        ICollection IDictionary.Values => throw new NotImplementedException();

        public bool IsSynchronized => throw new NotImplementedException();

        public object SyncRoot => throw new NotImplementedException();

        public object? this[object key] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public TValue this[TKey key] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        //public TValue this[TKey key] { get; set; }


        // Private Methods
        private void InitializeDictionary()
        {
            this._Buckets = new int[0];
            this._Entries = new Entry<TKey, TValue>[0];
            this._Free_list = -1;
            this._Count = 0;
            this._Capacity = 0;
        }

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
                this.SetCapacity(this.ExpandPrime(this._Count + number_of_new_pairs));
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
                for (int i = 0; i < this._Count; i++)
                {
                    if (this._Entries[i]._Hashcode != 0)
                    {
                        Entry<TKey, TValue> new_entry = new Entry<TKey, TValue>();
                        new_entry._Key = this._Entries[i]._Key;
                        new_entry._Value = this._Entries[i]._Value;
                        new_entry._Hashcode = Math.Abs(new_entry.GetHashCode());    // Hascode maybe negative but the index in bucket is positive
                        int modulus = new_entry._Hashcode % this._Capacity;

                        if (this._Buckets[modulus] != 0)
                        {
                            new_entry._Next = this._Buckets[modulus] - 1;
                        }

                        this._Buckets[modulus] = this._Free_list + 1;
                        this._Entries[this._Free_list] = new_entry;
                        this._Free_list++;
                        this._Count++;
                    }
                }

                this._Entries = new_entries;
            }
            else
            {
                this._Free_list = 0;
            }
            this._Buckets = new_bucket;
            this._Entries = new_entries;
        }

        private int FindFreeListIndex()
        {
            for (int i = 0; i < this._Capacity; i++)
            {
                if (this._Entries[i]._Hashcode == 0)
                {
                    return i;
                }
            }
            return -1;
        }

        // Public Methods
        public Dictionary()
        {
            InitializeDictionary();
        }

        public Dictionary(IDictionary<TKey, TValue> keyValuePairs)
        {
            InitializeDictionary();
            foreach (var item in keyValuePairs)
            {
                this.Add(item.Key, item.Value);
            }
        }

        public Dictionary(IDictionary<TKey, TValue> keyValuePairs, IEqualityComparer<TKey> equalityComparer)
        {
            InitializeDictionary();
            foreach (var item in keyValuePairs)
            {
                this.Add(item.Key, item.Value);
            }
            this._Comparer = equalityComparer;
        }

        public Dictionary(IEqualityComparer<TKey> equalityComparer)
        {
            InitializeDictionary();
            this._Comparer = equalityComparer;
        }

        public Dictionary(int capacity)
        {
            InitializeDictionary();
            this.SetCapacity(capacity);
        }

        public Dictionary(int capacity, IEqualityComparer<TKey> equalityComparer)
        {
            InitializeDictionary();
            this.SetCapacity(capacity);
            this._Comparer = equalityComparer;
        }

        public Dictionary(SerializationInfo serializationEntries, StreamingContext streamingContext)
        {
            InitializeDictionary();
        }

        public void Add(TKey key, TValue value)
        {
            this.CheckResize(1);
            Entry<TKey, TValue> new_entry = new Entry<TKey, TValue>();
            new_entry._Key = key;
            new_entry._Value = value;
            new_entry._Hashcode = Math.Abs(new_entry.GetHashCode());    // Hascode maybe negative but the index in bucket is positive
            int modulus = new_entry._Hashcode % this._Capacity;

            if (this._Buckets[modulus] != 0)
            {
                new_entry._Next = this._Buckets[modulus] - 1;
            }

            this._Buckets[modulus] = this._Free_list + 1;
            this._Entries[this._Free_list] = new_entry;
            this._Free_list = this.FindFreeListIndex();
            this._Count++;
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            this.Add(item.Key, item.Value);
        }


        public bool ContainsKey(TKey key)
        {
            throw new NotImplementedException();
        }

        public bool Remove(TKey key)
        {
            throw new NotImplementedException();
        }

        public bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public void Add(object key, object? value)
        {
            throw new NotImplementedException();
        }

        public bool Contains(object key)
        {
            throw new NotImplementedException();
        }

        IDictionaryEnumerator IDictionary.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public void Remove(object key)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(Array array, int index)
        {
            throw new NotImplementedException();
        }
        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable<TKey>)this).GetEnumerator();
    }
    class Program
    {
        public static void Main()
        {
            Dictionary<string, int> my_dictionary = new Dictionary<string, int>();
            my_dictionary.Add("a", 1);
        }
    }
}

