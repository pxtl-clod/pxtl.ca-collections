using PxtlCa.Collections.DictionaryFilters;

namespace PxtlCa.Collections;

/// <summary>
/// A dictionary that provides default values for missing keys.
/// </summary>
public class DefaultingDictionary<K, V> : MixableDictionary<K, V> {
    public DefaultingDictionary() : base() {
        Filters = [_delegateDefaultingDictionaryFilter];
    }

    public DefaultingDictionary(IEqualityComparer<K> comparer)
        : base(comparer) { }

    public DefaultingDictionary(int capacity)
        : base(capacity) { }

    public DefaultingDictionary(int capacity, IEqualityComparer<K> comparer)
        : base(capacity, comparer) { }

    public DefaultingDictionary(IDictionary<K, V> originalDictionary)
        : base(originalDictionary) { }

    protected DefaultingDictionary(IDictionary<K, V> originalDictionary, bool wrap)
        : base(originalDictionary, wrap) { }

    public DefaultingDictionary(IDictionary<K, V> originalDictionary, IEqualityComparer<K> comparer)
        : base(originalDictionary, comparer) { }

    private DelegateDefaultingDictionaryFilter<K, V> _delegateDefaultingDictionaryFilter = new();

    public ValueConstructor<K, V>? ValueConstructionHandler {
        get => _delegateDefaultingDictionaryFilter.ValueConstructionHandler;
        set { _delegateDefaultingDictionaryFilter.ValueConstructionHandler = value; }
    }
}