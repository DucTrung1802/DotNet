using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace MyCollection
{


    class Dictionary<TKey, TValue> : IDictionary<TKey, TValue>, IDictionary
    {
        struct Entry
        {
            public TKey key;
            public TValue value;
            public uint _Hashcode;
            public int _Next;

            public Entry()
            {
                this._Hashcode = 0;
                this._Next = -1;
            }
        }

        // Private Fields and Properties
        private int[] _Buckets;
        private Entry[] _Entries;
        private TKey[] _Keys;
        private TValue[] _Values;
        private int _Free_list;     // Index of first free slot
        private int _Count;         // Current number of pairs
        private int _Capacity;      // Maximum slot in current dictionary

        private IEqualityComparer<TKey>? _Comparer;

        // Public Fields and Properties
        public int Count { get { return _Count; } }

        public ICollection<TKey> Keys => this._Keys;

        public ICollection<TValue> Values => this._Values;

        bool IDictionary.IsReadOnly => false;

        bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly => false;

        bool IDictionary.IsFixedSize => false;

        ICollection IDictionary.Keys => (ICollection)this.Keys;

        ICollection IDictionary.Values => (ICollection)this.Values;

        bool ICollection.IsSynchronized => false;

        object ICollection.SyncRoot => this;

        public object? this[object key] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public TValue this[TKey key]
        {
            get
            {
                ref TValue value = ref FindValue(key);
                if (!Unsafe.IsNullRef(ref value))
                {
                    return value;
                }
                return default;
            }
            set
            {
                if (this.ContainsKey(key))
                {
                    ref TValue oldValue = ref FindValue(key);
                    oldValue = value;
                }
                else
                {
                    this.Add(key, value);
                }
            }
        }

        //public TValue this[TKey key] { get; set; }

        // Private Methods
        private void InitializeDictionary()
        {
            this._Buckets = new int[0];
            this._Entries = new Entry[0];
            this._Keys = new TKey[0];
            this._Values = new TValue[0];
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
            Entry[] new_entries = new Entry[this._Capacity];
            TKey[] new_keys = new TKey[this._Capacity];
            TValue[] new_values = new TValue[this._Capacity];
            int new_Free_list = 0;

            if (this._Count > 0)
            {
                for (int i = 0; i < this._Count; i++)
                {
                    Entry new_entry = new Entry();
                    new_entry.key = this._Entries[i].key;
                    new_entry.value = this._Entries[i].value;
                    new_entry._Hashcode = (uint)new_entry.key.GetHashCode();    // Hascode maybe negative but the index in bucket is positive
                    int modulus = (int)(new_entry._Hashcode % this._Capacity);
                    new_entry._Next = new_bucket[modulus] - 1;
                    new_bucket[modulus] = new_Free_list + 1;
                    new_entries[new_Free_list] = new_entry;
                    new_keys[new_Free_list] = new_entry.key;
                    new_values[new_Free_list] = new_entry.value;
                    new_Free_list++;
                }

                this._Free_list = new_Free_list;
            }
            else
            {
                this._Free_list = 0;
            }

            this._Buckets = new_bucket;
            this._Entries = new_entries;
            this._Keys = new_keys;
            this._Values = new_values;
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
            Entry new_entry = new Entry();
            new_entry.key = key;
            new_entry.value = value;
            new_entry._Hashcode = (uint)new_entry.key.GetHashCode();    // Hascode maybe negative but the index in bucket is positive
            int modulus = (int)(new_entry._Hashcode % this._Capacity);
            new_entry._Next = this._Buckets[modulus] - 1;
            this._Buckets[modulus] = this._Free_list + 1;
            this._Entries[this._Free_list] = new_entry;
            this._Keys[this._Free_list] = new_entry.key;
            this._Values[this._Free_list] = new_entry.value;
            this._Free_list = this.FindFreeListIndex();
            this._Count++;
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            this.Add(item.Key, item.Value);
        }

        private ref TValue FindValue(TKey key)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (this._Capacity == 0)
            {
                return ref Unsafe.NullRef<TValue>();
            }

            ref Entry entry = ref Unsafe.NullRef<Entry>();
            ref TValue value = ref Unsafe.NullRef<TValue>();

            if (this._Buckets != null)
            {
                uint hashcode = (uint)key.GetHashCode();
                int modulus = (int)(hashcode % this._Capacity);
                Entry[]? entries = this._Entries;
                if (this._Buckets[modulus] == 0)
                {
                    value = ref Unsafe.NullRef<TValue>();
                }
                else
                {
                    int next_index = this._Buckets[modulus] - 1;
                    entry = ref Unsafe.NullRef<Entry>();
                    while (next_index != -1)
                    {
                        entry = ref this._Entries[next_index];
                        if (entry._Hashcode == hashcode && EqualityComparer<TKey>.Default.Equals(entry.key, key))
                        {
                            value = ref entry.value;
                        }
                        next_index = entry._Next;
                    }

                }
            }

            return ref value;
        }

        public bool ContainsKey(TKey key) =>
            !Unsafe.IsNullRef(ref FindValue(key));

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
            for (int i = 0; i < this._Capacity; i++)
            {
                if (this._Entries[i]._Hashcode != 0)
                {
                    KeyValuePair<TKey, TValue> kvp = new KeyValuePair<TKey, TValue>(
                        this._Entries[i].key,
                        this._Entries[i].value);

                    yield return kvp;
                }
            }
        }

        public void Add(object key, object? value)
        {
            this.Add((TKey)key, (TValue?)value);
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

            // Create a new dictionary  
            // of strings, with string keys. 
            Dictionary<string, string> my_dictionary =
               new Dictionary<string, string>();

            // Adding key/value pairs in my_dictionary  
            my_dictionary["Australia"] = "Canberra";
            my_dictionary["Belgium"] = "Brussels";
            my_dictionary["Netherlands"] = "Amsterdam";
            my_dictionary["China"] = "Beijing";
            my_dictionary["Russia"] = "Moscow";
            my_dictionary["India"] = "New Delhi";

            foreach (var item in my_dictionary)
            {
                Console.WriteLine(item);
            }
        }
    }
}

