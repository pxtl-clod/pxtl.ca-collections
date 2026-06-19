using System.Collections;
using PxtlCa.Collections.Polyfills;

namespace PxtlCa.Collections.FilterInternals;

/// <summary>
/// The body nodes of the linked-list of a <see cref="MixableDictionary{K,
/// V}"/>.  Each node contains a link to its successor.
/// </summary>
internal sealed class DictionaryFilterLinkNode<K, V> : VirtualDictionary<K, V>, IDictionaryFilterNode<K, V> {
    #region Constructors
    internal DictionaryFilterLinkNode(IDictionaryFilter<K, V> filter, IDictionary<K, V> mixableDictionary) : base(mixableDictionary, wrap: true) {
        ArgumentGuard.ThrowIfNull(filter, nameof(filter));
        NextFilter = new DictionaryFilterTerminalNode<K, V>(mixableDictionary);
        Filter = filter!;
    }

    internal DictionaryFilterLinkNode(IDictionaryFilter<K, V> filter, IDictionary<K, V> mixableDictionary, IDictionaryFilterNode<K, V> nextFilter) : this(filter, mixableDictionary) {
        ArgumentGuard.ThrowIfNull(nextFilter, nameof(nextFilter));
        NextFilter = nextFilter;
    }
    #endregion

    #region Data Members
    public IDictionaryFilter<K, V> Filter { get; }
    internal IDictionaryFilterNode<K, V> NextFilter { get; set; }
    #endregion

    private void ThrowIfWrappedDictNull() {
        if (WrappedDictionary == null) {
            throw new NullPropertyException($"Cannot complete operation. '{nameof(WrappedDictionary)}' is null.  Use '{nameof(SetDictionary)}' to provide a value.", nameof(WrappedDictionary));
        }
    }

    public void SetDictionary(MixableDictionary<K, V> wrappedDictionary) {
        ThrowIfWrappedDictNull();
        WrappedDictionary = wrappedDictionary;
        NextFilter.SetDictionary(wrappedDictionary);
    }

    #region Dictionary Methods

    public override V this[K key] {
        get {
            return Filter.GetVal(WrappedDictionary, NextFilter, key);
        }
        set {
            Filter.SetVal(WrappedDictionary, NextFilter, key, value);
        }
    }

    public override bool Remove(K key) {
        return Filter.Remove(WrappedDictionary, NextFilter, key);
    }
    public override void Add(K key, V value) {
        Filter.Add(WrappedDictionary, NextFilter, key, value);
    }

    public override void Add(KeyValuePair<K, V> pair) {
        Add(pair.Key, pair.Value);
    }

    public override ICollection<K> Keys {
        get {
            return Filter.GetKeys(WrappedDictionary, NextFilter);
        }
    }
    public override ICollection<V> Values {
        get {
            return Filter.GetValues(WrappedDictionary, NextFilter);
        }
    }

    public override bool ContainsKey(K key) {
        return Filter.ContainsKey(WrappedDictionary, NextFilter, key);
    }

    public override bool Contains(KeyValuePair<K, V> item) {
        return Filter.Contains(WrappedDictionary, NextFilter, item);
    }

    public override bool Remove(KeyValuePair<K, V> item) {
        return Filter.Remove(WrappedDictionary!, NextFilter, item);
    }

    public override void Clear() {
        Filter.Clear(WrappedDictionary!, NextFilter);
    }

    public override int Count {
        get {
            return Filter.GetCount(WrappedDictionary!, NextFilter);
        }
    }

    public override void CopyTo(KeyValuePair<K, V>[] array, int arrayIndex) {
        Filter.CopyTo(WrappedDictionary!, NextFilter, array, arrayIndex);
    }

    public override IEnumerator GetObjectEnumerator() {
        return Filter.GetObjectEnumerator(WrappedDictionary!, NextFilter);
    }

    public override IEnumerator<KeyValuePair<K, V>> GetGenericEnumerator() {
        return Filter.GetEnumerator(WrappedDictionary!, NextFilter);
    }

    public override bool TryGetValue(K key, out V val) {
        return Filter.TryGetValue(WrappedDictionary!, NextFilter, key, out val);
    }

    public override bool IsReadOnly => false;
    #endregion
}