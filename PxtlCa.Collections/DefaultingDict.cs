using PxtlCa.Collections.DictMixins;

namespace PxtlCa.Collections;

/// <summary>
/// A dictionary that provides default values for missing keys.
/// </summary>
public class DefaultingDict<K, V> : MixableDict<K, V> {
    public DefaultingDict() : base() {
        Mixins = new[] { _delegateDefaultingDictMixin };
    }

    public DefaultingDict(IEqualityComparer<K> comparer)
        : base(comparer) {}

    public DefaultingDict(int capacity)
        : base(capacity) {}

    public DefaultingDict(int capacity, IEqualityComparer<K> comparer)
        : base(capacity, comparer) {}

    public DefaultingDict(IDictionary<K, V> originalDictionary)
        : base(originalDictionary) {}

    protected DefaultingDict(IDictionary<K, V> originalDictionary, bool wrap)
        : base(originalDictionary, wrap) {}

    public DefaultingDict(IDictionary<K, V> originalDictionary, IEqualityComparer<K> comparer)
        : base(originalDictionary, comparer) {}

    private DelegateDefaultingDictMixin<K, V> _delegateDefaultingDictMixin = new DelegateDefaultingDictMixin<K, V>();
    public ValueConstructor<K, V>? ValueConstructionHandler => _delegateDefaultingDictMixin.ValueConstructionHandler;
}