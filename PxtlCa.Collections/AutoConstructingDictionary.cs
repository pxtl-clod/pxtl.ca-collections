namespace PxtlCa.Collections;

using PxtlCa.Collections.DictionaryFilters;

public class AutoConstructingDictionary<K, V> : MixableDictionary<K, V>
where V : new() {
    public AutoConstructingDictionary() : base() {
        Initialize();
    }

    public AutoConstructingDictionary(IEqualityComparer<K> comparer)
    : base(comparer) {
        Initialize();
    }

    public AutoConstructingDictionary(int capacity)
    : base(capacity) {
        Initialize();
    }

    public AutoConstructingDictionary(int capacity, IEqualityComparer<K> comparer)
    : base(capacity, comparer) {
        Initialize();
    }

    public AutoConstructingDictionary(IDictionary<K, V> originalDictionary)
    : base(originalDictionary) {
        Initialize();
    }

    protected AutoConstructingDictionary(IDictionary<K, V> originalDictionary, bool wrap)
    : base(originalDictionary, wrap) {
        Initialize();
    }

    public AutoConstructingDictionary(IDictionary<K, V> originalDictionary, IEqualityComparer<K> comparer)
    : base(originalDictionary, comparer) {
        Initialize();
    }

    private void Initialize() {
        Filters = new[] { new AutoConstructingDictionaryFilter<K, V>() };
    }
}
