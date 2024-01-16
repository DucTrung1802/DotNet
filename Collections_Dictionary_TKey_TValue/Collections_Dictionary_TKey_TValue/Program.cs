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
                    new_entry._Hashcode = (uint)new_entry.key!.GetHashCode();    // Hascode maybe negative but the index in bucket is positive
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
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (this._Capacity == 0)
            {
                return false;
            }

            ref Entry entry = ref Unsafe.NullRef<Entry>();
            ref TValue value = ref Unsafe.NullRef<TValue>();

            if (this._Buckets != null)
            {
                uint hashcode = (uint)key.GetHashCode();
                int modulus = (int)(hashcode % this._Capacity);
                ref Entry[] entries = ref this._Entries!;
                if (this._Buckets[modulus] == 0)
                {
                    return false;
                }
                else
                {
                    int next_index = this._Buckets[modulus] - 1;
                    int last_index = this._Buckets[modulus] - 1;
                    while (next_index != -1)
                    {
                        entry = ref this._Entries[next_index];
                        if (entry._Hashcode == hashcode && EqualityComparer<TKey>.Default.Equals(entry.key, key))
                        {
                            entry._Hashcode = default!;
                            entry.key = default!;
                            entry.value = default!;
                            this._Count--;
                            this._Free_list = next_index;
                            entries[last_index]._Next = entry._Next;
                            return true;
                        }
                        last_index = next_index;
                        next_index = entry._Next;
                    }
                }
            }

            return false;
        }

        public bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            if (this._Count > 0)
            {
                Array.Clear(this._Buckets);
                Array.Clear(this._Entries);
                this._Count = 0;
            }
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            ref TValue get_value = ref this.FindValue(item.Key);
            if (get_value == null)
            {
                return false;
            }
            else if (get_value.Equals(item.Value))
            {
                return true;
            }
            return false;
        }

        private void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            if (array == null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            if ((uint)arrayIndex > (uint)array.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(arrayIndex));
            }

            if (this._Count + arrayIndex > array.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(arrayIndex));
            }

            int count = this._Count;
            Entry[]? entries = this._Entries;
            for (int i = 0; i < count; i++)
            {
                if (entries![i]._Next >= -1)
                {
                    array[arrayIndex++] = new KeyValuePair<TKey, TValue>(entries[i].key, entries[i].value);
                }
            }
        }

        bool Remove(KeyValuePair<TKey, TValue> item)
        {
            ref TValue value = ref FindValue(item.Key);
            if (!Unsafe.IsNullRef(ref value) && EqualityComparer<TValue>.Default.Equals(value, item.Value))
            {
                Remove(item.Key);
                return true;
            }

            return false;
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
            return this.ContainsKey((TKey)key);
        }

        IDictionaryEnumerator IDictionary.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public void Remove(object key)
        {
            this.Remove((TKey)key);
        }

        public void CopyTo(Array array, int index)
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable<TKey>)this).GetEnumerator();

        void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            this.CopyTo(array, arrayIndex);
        }

        bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item)
        {
            return this.Remove(item.Key);
        }
    }
    class Program
    {
        public static void Main()
        {
            // Creating a dictionary
            Dictionary<int, string> myDictionary = new Dictionary<int, string>();

            // Adding key-value pairs to the dictionary
            myDictionary.Add(1, "One");
            myDictionary.Add(2, "Two");
            myDictionary.Add(3, "Three");

            // Accessing values using keys
            Console.WriteLine("Value for key 2: " + myDictionary[2]);

            // Checking if a key exists
            int keyToCheck = 4;
            if (myDictionary.ContainsKey(keyToCheck))
            {
                Console.WriteLine("Key " + keyToCheck + " exists.");
            }
            else
            {
                Console.WriteLine("Key " + keyToCheck + " does not exist.");
            }

            // Iterating over key-value pairs in the dictionary
            Console.WriteLine("All key-value pairs in the dictionary:");
            foreach (KeyValuePair<int, string> kvp in myDictionary)
            {
                Console.WriteLine("Key: " + kvp.Key + ", Value: " + kvp.Value);
            }

            // Removing a key-value pair
            int keyToRemove = 2;
            if (myDictionary.ContainsKey(keyToRemove))
            {
                myDictionary.Remove(keyToRemove);
                Console.WriteLine("Key " + keyToRemove + " removed.");
            }
            else
            {
                Console.WriteLine("Key " + keyToRemove + " does not exist, so no removal performed.");
            }

            // Clearing all key-value pairs from the dictionary
            myDictionary.Clear();
            Console.WriteLine("Dictionary cleared. Count: " + myDictionary.Count);
        }
    }
}

