using System.Collections;
using PxtlCa.Collections.FilterInternals;

namespace PxtlCa.Collections;

/// <summary>
/// A dictionary allowing filter extensions for custom behavior.
/// </summary>
public class MixableDictionary<K, V> : VirtualDictionary<K, V> {
    #region Constructors
    public MixableDictionary() : base() {
        _filterListHead = new DictionaryFilterTerminalNode<K, V>(WrappedDictionary);
    }

    public MixableDictionary(IEqualityComparer<K> comparer)
        : base(comparer) {
        _filterListHead = new DictionaryFilterTerminalNode<K, V>(WrappedDictionary);
    }

    public MixableDictionary(int capacity)
        : base(capacity) {
        _filterListHead = new DictionaryFilterTerminalNode<K, V>(WrappedDictionary);
    }

    public MixableDictionary(int capacity, IEqualityComparer<K> comparer)
        : base(capacity, comparer) {
        _filterListHead = new DictionaryFilterTerminalNode<K, V>(WrappedDictionary);
    }

    public MixableDictionary(IDictionary<K, V> originalDictionary)
        : base(originalDictionary) {
        _filterListHead = new DictionaryFilterTerminalNode<K, V>(WrappedDictionary);
    }

    public MixableDictionary(IDictionary<K, V> originalDictionary, bool wrap)
        : base(originalDictionary, wrap) {
        _filterListHead = new DictionaryFilterTerminalNode<K, V>(WrappedDictionary);
    }

    public MixableDictionary(IDictionary<K, V> originalDictionary, IEqualityComparer<K> comparer)
        : base(originalDictionary, comparer) {
        _filterListHead = new DictionaryFilterTerminalNode<K, V>(WrappedDictionary);
    }
    #endregion

    #region Properties
    /// <summary>
    /// The head of the filter linked-list for extending collection behavior.
    /// </summary>
    private IDictionaryFilterNode<K, V> _filterListHead;

    /// <summary>
    /// Gets/Sets all filters applied to this dictionary for extension.
    /// </summary>
    public IEnumerable<IDictionaryFilter<K, V>> Filters {
        get {
            var filterHolder = _filterListHead;
            while (!(filterHolder is DictionaryFilterTerminalNode<K, V>)) {
                if (filterHolder is DictionaryFilterLinkNode<K, V> dictionaryFilterLinkNode) {
                    filterHolder = dictionaryFilterLinkNode.NextFilter;
                    yield return dictionaryFilterLinkNode.Filter;
                } else {
                    break;
                }
            }
        }
        set {
            IDictionaryFilterNode<K, V> headFilterHolder = new DictionaryFilterTerminalNode<K, V>(WrappedDictionary);
            foreach (IDictionaryFilter<K, V> filter in value) {
                IDictionaryFilterNode<K, V> oldHeadFilterHolder = headFilterHolder;
                headFilterHolder = new DictionaryFilterLinkNode<K, V>(filter, WrappedDictionary) {
                    NextFilter = oldHeadFilterHolder
                };
            }

            _filterListHead = headFilterHolder;
        }
    }
    #endregion

    #region Dictionary Implementation
    public override V this[K key] {
        get {
            return _filterListHead[key];
        }
        set {
            _filterListHead[key] = value;
        }
    }

    public override bool Remove(K key) {
        return _filterListHead.Remove(key);
    }

    public override void Add(K key, V value) {
        _filterListHead.Add(key, value);
    }

    public override ICollection<K> Keys {
        get { return _filterListHead.Keys; }
    }

    public override ICollection<V> Values {
        get { return _filterListHead.Values; }
    }

    public override bool ContainsKey(K key) {
        return _filterListHead.ContainsKey(key);
    }

    public override bool Contains(KeyValuePair<K, V> item) {
        return _filterListHead.Contains(item);
    }
    public override bool Remove(KeyValuePair<K, V> item) {
        return _filterListHead.Remove(item);
    }
    public override void Clear() {
        _filterListHead.Clear();
    }
    public override int Count {
        get { return _filterListHead.Count; }
    }

    public override void Add(KeyValuePair<K, V> item) {
        _filterListHead.Add(item.Key, item.Value);
    }
    public override void CopyTo(KeyValuePair<K, V>[] array, int arrayIndex) {
        _filterListHead.CopyTo(array, arrayIndex);
    }

    public override IEnumerator GetObjectEnumerator() {
        return _filterListHead.GetEnumerator();
    }

    /// <summary>
    /// Returns an ordered KeyValuePair enumerator.
    /// </summary>
    public override IEnumerator<KeyValuePair<K, V>> GetGenericEnumerator() {
        return _filterListHead.GetEnumerator();
    }

    public override bool TryGetValue(K key, out V val) {
        return _filterListHead.TryGetValue(key, out val);
    }
    #endregion

}

public static class MixableDictFactory {
    public static MixableDictionary<K, V> Wrap<K, V>(IDictionary<K, V> dictionaryToWrap) => new MixableDictionary<K, V>(dictionaryToWrap, wrap: true);
    public static MixableDictionary<K, V> WrapInMixableDictionary<K, V>(this IDictionary<K, V> dictionaryToWrap) => Wrap(dictionaryToWrap);
}