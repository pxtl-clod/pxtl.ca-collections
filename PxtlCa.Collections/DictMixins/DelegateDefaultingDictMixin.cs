namespace PxtlCa.Collections.DictMixins;

public class DelegateDefaultingDictMixin<K, V> : DefaultingDictMixin<K, V> {
    public DelegateDefaultingDictMixin() : base() { }

    public DelegateDefaultingDictMixin(ValueConstructor<K, V>? constructor)
        : this()
    {
        ValueConstructionHandler = constructor;
    }

    /// <exception cref="NullPropertyException">Throws if ValueConstructionHandler is not set.</exception>
    protected override bool TryGetDefaultValue(K key, out V val)
    {
        if (ValueConstructionHandler == null) {
            val = default(V)!;
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