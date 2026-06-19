using System.Collections;
using PxtlCa.Collections.Polyfills;

namespace PxtlCa.Collections;

public class VirtualDictionary<K, V> : VirtualWrappedDictionary<IDictionary<K, V>, K, V> {
    #region Dictionary Constructors
    // We provide all the same constructors that the generic
    // Dictionary provides.

    public VirtualDictionary() : base(new Dictionary<K, V>()) { }

    public VirtualDictionary(IEqualityComparer<K> comparer) : base(new Dictionary<K, V>(comparer)) { }

    public VirtualDictionary(int capacity) : base(new Dictionary<K, V>(capacity)) { }

    public VirtualDictionary(int capacity, IEqualityComparer<K> comparer) : base(new Dictionary<K, V>(capacity, comparer)) { }

    public VirtualDictionary(IDictionary<K, V> originalDictionary) : this(originalDictionary, false) { }

    public VirtualDictionary(IDictionary<K, V> originalDictionary, IEqualityComparer<K> comparer) : base(new Dictionary<K, V>(originalDictionary, comparer)) { }
    #endregion

    #region Wrapping Constructors
    public VirtualDictionary(IDictionary<K, V> originalDictionary, bool wrap)
    : base(wrap ? originalDictionary : new Dictionary<K, V>(originalDictionary)) { }
    #endregion
}

public class VirtualWrappedDictionary<D, K, V> : IDictionary<K, V>
where D : IDictionary<K, V> {
    /// <summary>
    /// Provides the underlying collection for this wrapper.
    /// </summary>
    // to this.

    protected D WrappedDictionary { get; set; }

    #region Wrapping Constructors
    public VirtualWrappedDictionary(D originalDictionary) {
        ArgumentGuard.ThrowIfNull(originalDictionary, nameof(originalDictionary));
        WrappedDictionary = originalDictionary;
    }
    #endregion

    public virtual V this[K key] {
        get {
            return WrappedDictionary[key];
        }
        set {
            WrappedDictionary[key] = value;
        }
    }

    // Delegating implementations of all other methods.

    public virtual bool Remove(K key) {
        return WrappedDictionary.Remove(key);
    }
    public virtual void Add(K key, V value) {
        WrappedDictionary.Add(key, value);
    }
    public virtual ICollection<K> Keys {
        get { return WrappedDictionary.Keys; }
    }

    public virtual ICollection<V> Values {
        get { return WrappedDictionary.Values; }
    }

    public virtual bool ContainsKey(K key) {
        return WrappedDictionary.ContainsKey(key);
    }

    public virtual bool IsReadOnly {
        get { return WrappedDictionary.IsReadOnly; }
    }

    public virtual bool Contains(KeyValuePair<K, V> item) {
        return WrappedDictionary.Contains(item);
    }
    public virtual bool Remove(KeyValuePair<K, V> item) {
        return WrappedDictionary.Remove(item);
    }
    public virtual void Clear() {
        WrappedDictionary.Clear();
    }
    public virtual int Count {
        get { return WrappedDictionary.Count; }
    }

    public virtual void Add(KeyValuePair<K, V> item) {
        WrappedDictionary.Add(item);
    }
    public virtual void CopyTo(KeyValuePair<K, V>[] array, int arrayIndex) {
        WrappedDictionary.CopyTo(array, arrayIndex);
    }

    public virtual IEnumerator GetObjectEnumerator() {
        return WrappedDictionary.GetEnumerator();
    }

    IEnumerator System.Collections.IEnumerable.GetEnumerator() {
        return GetObjectEnumerator();
    }

    public virtual IEnumerator<KeyValuePair<K, V>> GetGenericEnumerator() {
        return WrappedDictionary.GetEnumerator();
    }

    IEnumerator<KeyValuePair<K, V>> IEnumerable<KeyValuePair<K, V>>.GetEnumerator() {
        return GetGenericEnumerator();
    }

    public virtual bool TryGetValue(K key, out V val) {
        return WrappedDictionary.TryGetValue(key, out val);
    }
}

public static class VirtualDictFactory {
    public static VirtualDictionary<K, V> Wrap<K, V>(IDictionary<K, V> dictionaryToWrap) => new VirtualDictionary<K, V>(dictionaryToWrap, wrap: true);
    public static VirtualDictionary<K, V> WrapInVirtualDictionary<K, V>(this IDictionary<K, V> dictionaryToWrap) => Wrap(dictionaryToWrap);
}