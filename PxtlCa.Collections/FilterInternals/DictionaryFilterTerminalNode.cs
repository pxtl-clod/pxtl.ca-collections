using System.Collections;

namespace PxtlCa.Collections.FilterInternals;

/// <summary>
/// The terminal node of the linked-list of a <see cref="MixableDictionary{K,
/// V}"/>.  This node implements its own filter that directly goes straight to
/// the underlying wrapped dictionary.
/// </summary>
internal sealed class DictionaryFilterTerminalNode<K, V>
: VirtualDictionary<K, V>, IDictionaryFilterNode<K, V> {
    #region IDictionaryFilterHolder Members

    public void SetDictionary(MixableDictionary<K, V> baseDictionary) {
        WrappedDictionary = baseDictionary;
    }
    #endregion

    #region Constuctors
    internal DictionaryFilterTerminalNode(IDictionary<K, V> baseDictionary) : base(baseDictionary, wrap: true) { }
    #endregion

    #region IDictionaryFilter Members
    public bool Remove(IDictionary<K, V> thisDictionary, IDictionaryFilterNode<K, V> nextFilter, K key) {
        return this.Remove(key);
    }
    public void Add(IDictionary<K, V> thisDictionary, IDictionaryFilterNode<K, V> nextFilter, K key, V value) {
        this.Add(key, value);
    }
    public ICollection<K> GetKeys(IDictionary<K, V> thisDictionary, IDictionaryFilterNode<K, V> nextFilter) {
        return this.Keys;
    }
    public ICollection<V> GetValues(IDictionary<K, V> thisDictionary, IDictionaryFilterNode<K, V> nextFilter) {
        return this.Values;
    }

    public bool ContainsKey(IDictionary<K, V> thisDictionary, IDictionaryFilterNode<K, V> nextFilter, K key) {
        return this.ContainsKey(key);
    }

    public bool Contains(IDictionary<K, V> thisDictionary, IDictionaryFilterNode<K, V> nextFilter, KeyValuePair<K, V> item) {
        return this.Contains(item);
    }

    public bool Remove(IDictionary<K, V> thisDictionary, IDictionaryFilterNode<K, V> nextFilter, KeyValuePair<K, V> item) {
        return this.Remove(item);
    }

    public void Clear(IDictionary<K, V> thisDictionary, IDictionaryFilterNode<K, V> nextFilter) {
        this.Clear();
    }

    public int GetCount(IDictionary<K, V> thisDictionary, IDictionaryFilterNode<K, V> nextFilter) {
        return this.Count;
    }

    public void CopyTo(IDictionary<K, V> thisDictionary, IDictionaryFilterNode<K, V> nextFilter, KeyValuePair<K, V>[] array, int arrayIndex) {
        this.CopyTo(array, arrayIndex);
    }

    public IEnumerator GetObjectEnumerator(IDictionary<K, V> thisDictionary, IDictionaryFilterNode<K, V> nextFilter) {
        return (this as IEnumerable).GetEnumerator();
    }

    public IEnumerator<KeyValuePair<K, V>> GetEnumerator(IDictionary<K, V> thisDictionary, IDictionaryFilterNode<K, V> nextFilter) {
        return (this as IEnumerable<KeyValuePair<K, V>>).GetEnumerator();
    }

    public bool TryGetValue(IDictionary<K, V> thisDictionary, IDictionaryFilterNode<K, V> nextFilter, K key, out V val) {
        return this.TryGetValue(key, out val);
    }

    public V GetVal(IDictionary<K, V> thisDictionary, IDictionaryFilterNode<K, V> NextFilter, K key) {
        return this[key];
    }

    public void SetVal(IDictionary<K, V> thisDictionary, IDictionaryFilterNode<K, V> NextFilter, K key, V value) {
        this[key] = value;
    }

    #endregion
}