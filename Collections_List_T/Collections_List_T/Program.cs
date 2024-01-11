using System.Collections;

namespace MyCollection
{
    class List<T> : Comparer<T>, IEnumerable<T>
    {
        // Private Fields and Properties
        private int _Capacity = 0;
        private int _Count = 0;
        private T[] Items;

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
            this.Items = new T[0];
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
            this.Items = new T[capacity];
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
            return Array.BinarySearch(this.Items, 0, this._Count, item);
        }

        public int BinarySearch(T item, IComparer<T> comparer)
        {
            return Array.BinarySearch(this.Items, 0, this._Count, item, comparer);
        }

        public int BinarySearch(int index, int count, T item, IComparer<T> comparer)
        {
            return Array.BinarySearch(this.Items, index, count, item, comparer);
        }

        public void Clear()
        {
            this.Items = new T[0];
            this._Count = 0;
        }

        public bool Contains(T? item)
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
            for (int i = 0; i < _Count; i++)
            {
                action(this.Items[i]);
            }
        }

        public int IndexOf(T item)
        {
            for (int i = 0; i < _Count; i++)
            {
                if (item.Equals(Items[i]))
                {
                    return i;
                }
            }
            return -1;
        }

        public void Insert(int index, T item)
        {
            if (index < 0 || index >= _Count)
            {
                throw new ArgumentOutOfRangeException("index");
            }
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            if (_Count + index > _Capacity)
            {
                this.ReallocateList(_Capacity * 2);
                for (int i = _Count - 1; i >= index; i--)
                {
                    this.Items[i + 1] = this.Items[i];
                }
                this.Items[index] = item;
            }
        }

        public void InsertRange(int index, IEnumerable<T> items)
        {
            if (index < 0 || index >= _Count)
            {
                throw new ArgumentOutOfRangeException("index");
            }
            ArgumentNullException.ThrowIfNull(items);
            while (_Count + items.Count() > _Capacity)
            {
                this.ReallocateList(_Capacity * 2);
            }

            for (int i = _Count - 1; i >= index; i--)
            {
                this.Items[i + items.Count()] = this.Items[i];
            }

            for (int i = 0; i < items.Count(); i++)
            {
                this.Items[index + i] = items.ElementAt(i);
            }
        }

        public bool Remove(T item)
        {
            int target_index = this.IndexOf(item);
            if (target_index >= 0)
            {
                for (int i = target_index; i < _Count - 1; i++)
                {
                    this.Items[i] = this.Items[i + 1];
                }
                return true;
            }
            return false;
        }

        public void RemoveRange(int from_index, int to_index)
        {
            int length_to_remove = to_index - from_index;
            if (length_to_remove < 0)
            {
                throw new ArgumentException();
            }
            if (length_to_remove == 0)
            {
                Remove(this.Items[from_index]);
            }
            else
            {
                for (int i = from_index; i < length_to_remove; i++)
                {
                    this.Items[i] = this[i + length_to_remove];
                }
            }
        }

        public void Reverse()
        {
            Array.Reverse(this.Items);
        }

        public void Sort()
        {
            Array.Sort(this.Items);
        }

        public void Sort(IComparer<T> comparer)
        {
            Array.Sort(this.Items, comparer);
        }

        public T[] ToArray(T input)
        {
            return default;
        }

        public string ToString()
        {
            return this.ToString();
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
        public static void Main(String[] args)
        {

        }
    }
}