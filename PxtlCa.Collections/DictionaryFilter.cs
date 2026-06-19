using System.Collections;

namespace PxtlCa.Collections;

public class DictionaryFilter<K, V> : IDictionaryFilter<K, V> {
    #region Dictionary Methods
    public virtual bool Remove(IDictionary<K, V> thisDictionary, IDictionaryFilterNode<K, V> nextFilter, K key) {
        return nextFilter.Remove(key);
    }

    public virtual void Add(IDictionary<K, V> thisDictionary, IDictionaryFilterNode<K, V> nextFilter, K key, V value) {
        nextFilter.Add(key, value);
    }

    public virtual ICollection<K> GetKeys(IDictionary<K, V> thisDictionary, IDictionaryFilterNode<K, V> nextFilter) {
        return nextFilter.Keys;
    }

    public virtual ICollection<V> GetValues(IDictionary<K, V> thisDictionary, IDictionaryFilterNode<K, V> nextFilter) {
        return nextFilter.Values;
    }

    public virtual bool ContainsKey(IDictionary<K, V> thisDictionary, IDictionaryFilterNode<K, V> nextFilter, K key) {
        return nextFilter.ContainsKey(key);
    }

    public virtual bool Contains(IDictionary<K, V> thisDictionary, IDictionaryFilterNode<K, V> nextFilter, KeyValuePair<K, V> item) {
        return nextFilter.Contains(item);
    }

    public virtual bool Remove(IDictionary<K, V> thisDictionary, IDictionaryFilterNode<K, V> nextFilter, KeyValuePair<K, V> item) {
        return nextFilter.Remove(item);
    }

    public virtual void Clear(IDictionary<K, V> thisDictionary, IDictionaryFilterNode<K, V> nextFilter) {
        nextFilter.Clear();
    }

    public virtual int GetCount(IDictionary<K, V> thisDictionary, IDictionaryFilterNode<K, V> nextFilter) {
        return nextFilter.Count;
    }

    public virtual void CopyTo(IDictionary<K, V> thisDictionary, IDictionaryFilterNode<K, V> nextFilter, KeyValuePair<K, V>[] array, int arrayIndex) {
        nextFilter.CopyTo(array, arrayIndex);
    }

    public virtual IEnumerator GetObjectEnumerator(IDictionary<K, V> thisDictionary, IDictionaryFilterNode<K, V> nextFilter) {
        return nextFilter.GetEnumerator();
    }

    public virtual IEnumerator<KeyValuePair<K, V>> GetEnumerator(IDictionary<K, V> thisDictionary, IDictionaryFilterNode<K, V> nextFilter) {
        return nextFilter.GetEnumerator();
    }

    public virtual bool TryGetValue(IDictionary<K, V> thisDictionary, IDictionaryFilterNode<K, V> nextFilter, K key, out V val) {
        return nextFilter.TryGetValue(key, out val);
    }

    public virtual V GetVal(IDictionary<K, V> thisDictionary, IDictionaryFilterNode<K, V> NextFilter, K key) {
        return NextFilter[key];
    }

    public virtual void SetVal(IDictionary<K, V> thisDictionary, IDictionaryFilterNode<K, V> NextFilter, K key, V value) {
        NextFilter[key] = value;
    }
    #endregion
}