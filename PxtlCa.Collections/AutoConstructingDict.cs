namespace PxtlCa.Collections;
using PxtlCa.Collections.DictMixins;

public class AutoConstructingDict<K, V> : MixableDict<K, V> 
where V : new() {
    public AutoConstructingDict() : base() {
        Initialize();
    }

    public AutoConstructingDict(IEqualityComparer<K> comparer) 
    : base(comparer) {
        Initialize();
    }

    public AutoConstructingDict(int capacity)
    : base(capacity) {
        Initialize();
    }

    public AutoConstructingDict(int capacity, IEqualityComparer<K> comparer)
    : base(capacity, comparer) {
        Initialize();
    }

    public AutoConstructingDict(IDictionary<K, V> originalDictionary)
    : base(originalDictionary) {
        Initialize();
    }

    protected AutoConstructingDict(IDictionary<K, V> originalDictionary, bool wrap)
    : base(originalDictionary, wrap) {
        Initialize();
    }

    public AutoConstructingDict(IDictionary<K, V> originalDictionary, IEqualityComparer<K> comparer)
    : base(originalDictionary, comparer) {
        Initialize();
    }

    private void Initialize() {
        Mixins = new[] { new AutoConstructingDictMixin<K, V>() };
    }
}
