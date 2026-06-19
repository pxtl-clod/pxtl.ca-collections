using System.Collections;

namespace PxtlCa.Collections
{
    public class VirtualDict<K, V> : IDictionary<K, V>
    {
        /// <summary>
        /// Provides the underlying collection for this wrapper.
        /// </summary>
        // to this.

        private IDictionary<K, V> _dict;
        protected virtual internal IDictionary<K, V> BaseDict
        {
            get
            {
                return _dict;
            }
        }

        #region Constructors
        // We provide all the same constructors that the generic
        // Dictionary provides.

        public VirtualDict()
        {
            _dict = new Dictionary<K, V>();
        }

        public VirtualDict(IEqualityComparer<K> comparer)
        {
            _dict = new Dictionary<K, V>(comparer);
        }

        public VirtualDict(int capacity)
        {
            _dict = new Dictionary<K, V>(capacity);
        }

        public VirtualDict(int capacity, IEqualityComparer<K> comparer)
        {
            _dict = new Dictionary<K, V>(capacity, comparer);
        }

        public VirtualDict(IDictionary<K, V> originalDictionary)
            : this(originalDictionary, false)
        {
        }

        public VirtualDict(IDictionary<K, V> originalDictionary, IEqualityComparer<K> comparer)
        {
            _dict = new Dictionary<K, V>(originalDictionary, comparer);
        }
        #endregion

        // This methods allow an existing dictionary to be wrapped.
        // (The public constructors that take an IDictionary all
        // copy the data.)
        public static VirtualDict<K, V> Wrap(IDictionary<K, V> originalDictionary)
        {
            return new VirtualDict<K, V>(originalDictionary, true);
        }

        // Private constructor used to enable a dictionary either to be
        // copied, or used as the underying storage.
        protected VirtualDict(IDictionary<K, V> originalDictionary, bool wrap)
        {
            if (wrap)
            {
                _dict = originalDictionary;
            }
            else
            {
                _dict = new Dictionary<K, V>(originalDictionary);
            }
        }

        public virtual V this[K key]
        {
            get
            {
                return _dict[key];
            }
            set
            {
                _dict[key] = value;
            }
        }

        // Delegating implementations of all other methods.

        public virtual bool Remove(K key)
        {
            return _dict.Remove(key);
        }
        public virtual void Add(K key, V value)
        {
            _dict.Add(key, value);
        }
        public virtual ICollection<K> Keys
        {
            get { return _dict.Keys; }
        }

        public virtual ICollection<V> Values
        {
            get { return _dict.Values; }
        }

        public virtual bool ContainsKey(K key)
        {
            return _dict.ContainsKey(key);
        }

        public virtual bool IsReadOnly
        {
            get { return _dict.IsReadOnly; }
        }

        public virtual bool Contains(KeyValuePair<K, V> item)
        {
            return _dict.Contains(item);
        }
        public virtual bool Remove(KeyValuePair<K, V> item)
        {
            return _dict.Remove(item);
        }
        public virtual void Clear()
        {
            _dict.Clear();
        }
        public virtual int Count
        {
            get { return _dict.Count; }
        }

        public virtual void Add(KeyValuePair<K, V> item)
        {
            _dict.Add(item);
        }
        public virtual void CopyTo(KeyValuePair<K, V>[] array, int arrayIndex)
        {
            _dict.CopyTo(array, arrayIndex);
        }

        public virtual IEnumerator GetObjectEnumerator()
        {
            return _dict.GetEnumerator();
        }

        IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetObjectEnumerator();
        }

        public virtual IEnumerator<KeyValuePair<K, V>> GetGenericEnumerator()
        {
            return _dict.GetEnumerator();
        }

        IEnumerator<KeyValuePair<K, V>> IEnumerable<KeyValuePair<K, V>>.GetEnumerator()
        {
            return GetGenericEnumerator();
        }

        public virtual bool TryGetValue(K key, out V val)
        {
            return _dict.TryGetValue(key, out val);
        }
    }
}
