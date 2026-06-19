namespace PxtlCa.Collections;

public class AutoConstructingDict<K, V> : DefaultingDict<K, V> 
where V : new() {
    #region Constructors
    //custom constructor to use a custom valueconstructor
    public AutoConstructingDict(ValueConstructor con)
        : base(con)
    { }

    public AutoConstructingDict(ValueConstructor con, IEqualityComparer<K> comparer)
        : base(con, comparer)
    { }

    public AutoConstructingDict() : base()
    { }

    public AutoConstructingDict(IEqualityComparer<K> comparer)
        : base(comparer)
    { }

    public AutoConstructingDict(int capacity)
        : base(capacity)
    { }

    public AutoConstructingDict(int capacity, IEqualityComparer<K> comparer)
        : base(capacity, comparer)
    { }

    public AutoConstructingDict(IDictionary<K, V> originalDictionary)
        : base(originalDictionary)
    { }

    protected AutoConstructingDict(IDictionary<K, V> originalDictionary, bool wrap)
        : base(originalDictionary, wrap)
    { }

    public AutoConstructingDict(IDictionary<K, V> originalDictionary, IEqualityComparer<K> comparer)
        : base(originalDictionary, comparer)
    { }
    #endregion

    protected override V defaultValueConstructorImplementation()
    {
        return new V();
    }
}
