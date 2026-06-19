namespace PxtlCa.Collections;

/// <summary>
/// A dictionary that provides default values for missing keys.
/// </summary>
public class DefaultingDict<K, V> : VirtualDict<K, V> {
    // Underlying dictionary - everything delegates
    // to this.

    protected virtual V defaultValueConstructorImplementation()
    {
        return default(V);
    }
    
    //ValueConstructor system to construct defaults.
    public delegate V ValueConstructor();
    private ValueConstructor valueConstructionHandler;
    public ValueConstructor ValueConstructionHandler
    {
        get
        {
            return valueConstructionHandler;
        }
        set
        {
            valueConstructionHandler = value;
        }
    }


    #region Constructors
    //custom constructor to use a custom valueconstructor
    public DefaultingDict(ValueConstructor con)
        : this()
    {
        this.ValueConstructionHandler = con;
    }

    public DefaultingDict(ValueConstructor con, IEqualityComparer<K> comparer)
        : this(comparer)
    {
        this.ValueConstructionHandler = con;
    }

    // We provide all the same constructors that the generic
    // Dictionary provides.

    public DefaultingDict() : base() { }

    public DefaultingDict(IEqualityComparer <K> comparer) : base(new Dictionary<K, V>(comparer)) { }

    public DefaultingDict(int capacity) : base(capacity) { }

    public DefaultingDict(int capacity, IEqualityComparer<K> comparer) : base(capacity, comparer) { }

    public DefaultingDict(IDictionary<K, V> originalDictionary) : this(originalDictionary, false) {}

    public DefaultingDict(IDictionary<K, V> originalDictionary, IEqualityComparer<K> comparer) : base(originalDictionary, comparer) { }
    #endregion

    // This methods allow an existing dictionary to be wrapped.
    // (The public constructors that take an IDictionary all
    // copy the data.)
    public static new DefaultingDict<K, V> Wrap(IDictionary<K, V> originalDictionary)
    {
        return new DefaultingDict<K, V>(originalDictionary, true);
    }

    // Private constructor used to enable a dictionary either to be
    // copied, or used as the underying storage.
    protected DefaultingDict(IDictionary<K, V> originalDictionary, bool wrap) : base(originalDictionary, wrap) {}

    // This is the raison d'etre of this whole class - an indexer
    // which adds an empty V when K does not exist.
    public override V this[K key]
    {
        get
        {
            V val;
            if(base.TryGetValue(key, out val))
            {
                return base[key];
            }
            else
            {
                V temp = (ValueConstructionHandler == null) ? defaultValueConstructorImplementation() : ValueConstructionHandler();
                this[key] = temp;
                return temp;
            }
        }
        set
        {
            base[key] = value;
        }
    }
}