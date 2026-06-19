namespace PxtlCa.Collections.DictionaryFilters;

public class DelegateDefaultingDictionaryFilter<K, V> : DefaultingDictionaryFilter<K, V> {
    public DelegateDefaultingDictionaryFilter() : base() { }

    public DelegateDefaultingDictionaryFilter(ValueConstructor<K, V>? constructor)
    : this() {
        ValueConstructionHandler = constructor;
    }

    /// <exception cref="NullPropertyException">
    /// Throws if ValueConstructionHandler is not set.
    /// </exception>
    protected override bool TryGetDefaultValue(K key, out V val) {
        if (ValueConstructionHandler == null) {
            val = default!;
            return false;
        } else {
            val = ValueConstructionHandler(key);
            return true;
        }
    }

    #region ValueConstructor system to construct defaults.
    /// <summary>
    /// The default constructor to make dictionary objects if a value is not
    /// already present in the dictionary. 
    /// </summary>
    public ValueConstructor<K, V>? ValueConstructionHandler { get; set; }
    #endregion
}

public delegate V ValueConstructor<K, V>(K key);