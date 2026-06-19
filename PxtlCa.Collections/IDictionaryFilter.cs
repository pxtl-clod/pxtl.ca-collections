using System.Collections;

namespace PxtlCa.Collections;

/// <summary>
/// Similar to an <see cref="IDictionary{K, V}"/> interface, this interface describes
/// every operation for a dictionary, but implementors can also redirect to the
/// next Filter in the linked list.
/// </summary>
public interface IDictionaryFilter<K, V> {
    void Add(IDictionary<K, V> thisDictionary, IDictionaryFilterNode<K, V> nextFilter, K key, V value);
    void Clear(IDictionary<K, V> thisDictionary, IDictionaryFilterNode<K, V> nextFilter);
    bool Contains(IDictionary<K, V> thisDictionary, IDictionaryFilterNode<K, V> nextFilter, KeyValuePair<K, V> item);
    bool ContainsKey(IDictionary<K, V> thisDictionary, IDictionaryFilterNode<K, V> nextFilter, K key);
    void CopyTo(IDictionary<K, V> thisDictionary, IDictionaryFilterNode<K, V> nextFilter, KeyValuePair<K, V>[] array, int arrayIndex);
    int GetCount(IDictionary<K, V> thisDictionary, IDictionaryFilterNode<K, V> nextFilter);
    IEnumerator<KeyValuePair<K, V>> GetEnumerator(IDictionary<K, V> thisDictionary, IDictionaryFilterNode<K, V> nextFilter);
    ICollection<K> GetKeys(IDictionary<K, V> thisDictionary, IDictionaryFilterNode<K, V> nextFilter);
    IEnumerator GetObjectEnumerator(IDictionary<K, V> thisDictionary, IDictionaryFilterNode<K, V> nextFilter);
    V GetVal(IDictionary<K, V> thisDictionary, IDictionaryFilterNode<K, V> NextFilter, K key);
    ICollection<V> GetValues(IDictionary<K, V> thisDictionary, IDictionaryFilterNode<K, V> nextFilter);
    bool Remove(IDictionary<K, V> thisDictionary, IDictionaryFilterNode<K, V> nextFilter, K key);
    bool Remove(IDictionary<K, V> thisDictionary, IDictionaryFilterNode<K, V> nextFilter, KeyValuePair<K, V> item);
    void SetVal(IDictionary<K, V> thisDictionary, IDictionaryFilterNode<K, V> NextFilter, K key, V value);
    bool TryGetValue(IDictionary<K, V> thisDictionary, IDictionaryFilterNode<K, V> nextFilter, K key, out V val);
}
