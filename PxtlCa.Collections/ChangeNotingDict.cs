namespace PxtlCa.Collections;

public class ChangeNotingDict<K, V> : VirtualDict<K, V>
where V: ICloneable {
    /// <summary>
    /// Underlying dictionary - everything delegates to this.
    /// </summary>
    private IDictionary<K, ChangeNote<V>> changes;
    public IDictionary<K, ChangeNote<V>> Changes
    {
        get
        {
            return changes;
        }
    }

    public delegate void ChangeHandler(K key, ChangeNote<V> change);
    public ChangeHandler? Changed;

    #region New Methods and Members

    public bool NoteChanges = false;

    protected void NoteChange(K key, ChangeType type, V? val)
    {
        if (NoteChanges)
        {
            ChangeNote<V> change = new ChangeNote<V>(type, val);
            changes[key] = change;
            if (Changed != null) Changed(key, change);
        }
    }

    #endregion

    #region Constructors
    // We provide all the same constructors that the generic
    // Dictionary provides.

    public ChangeNotingDict() : base()
    {
        changes = new Dictionary<K, ChangeNote<V>>();
    }

    public ChangeNotingDict(ChangeHandler onChanged)
        : this()
    {
        Changed = onChanged;
    }

    public ChangeNotingDict(ChangeHandler onChanged, IEqualityComparer<K> comparer)
        : this(comparer)
    {
        Changed = onChanged;
    }

    public ChangeNotingDict(IEqualityComparer<K> comparer) 
        : base(comparer)
    {
        changes = new Dictionary<K, ChangeNote<V>>(comparer);
    }

    public ChangeNotingDict(int capacity)
        : base(capacity)
    {
        changes = new Dictionary<K, ChangeNote<V>>();
    }

    public ChangeNotingDict(int capacity, IEqualityComparer<K> comparer)
        : base(capacity, comparer)
    {
        changes = new Dictionary<K, ChangeNote<V>>(comparer);
    }

    public ChangeNotingDict(IDictionary<K, V> originalDictionary)
        : base(originalDictionary)
    {
        changes = new Dictionary<K, ChangeNote<V>>();
    }

    public ChangeNotingDict(IDictionary<K, V> originalDictionary, IEqualityComparer<K> comparer) 
        : base(originalDictionary, comparer)
    {
        changes = new Dictionary<K, ChangeNote<V>>(comparer);
    }
    #endregion

    // This methods allow an existing dictionary to be wrapped.
    // (The public constructors that take an IDictionary all
    // copy the data.)
    public static ChangeNotingDict<K, V> Wrap(IDictionary<K, V> originalDictionary, IEqualityComparer<K> comparer)
    {
        return new ChangeNotingDict<K, V>(originalDictionary, comparer, true);
    }

    // Private constructor used to enable a dictionary either to be
    // copied, or used as the underying storage.
    protected ChangeNotingDict(IDictionary<K, V> originalDictionary, IEqualityComparer<K> comparer, bool wrap)
        : base(originalDictionary, wrap) 
    {
        changes = new Dictionary<K, ChangeNote<V>>(comparer);
    }

    // This is the raison d'etre of this whole class - an indexer
    // which notes any changes
    public override V this[K key]
    {
        get
        {
                return base[key];
        }
        set
        {
            base[key] = value;
            NoteChange(key, ChangeType.Set, value);
        }
    }

    // Delegating implementations of all other methods.
    public override bool Remove(K key)
    {
        NoteChange(key, ChangeType.Remove, default(V));
        return base.Remove(key);          
    }
    public override void Add(K key, V value)
    {
        base.Add(key, value);
        NoteChange(key, ChangeType.Add, value);
    }
    public override bool Remove(KeyValuePair<K, V> item)
    {
        NoteChange(item.Key, ChangeType.Remove, default(V));
        return base.Remove(item);
    }

    public override void Clear()
    {
        foreach (K key in Keys)
        {
            NoteChange(key, ChangeType.Remove, default(V));
        }
        base.Clear();
    }

    public override void Add(KeyValuePair<K, V> item)
    {
        Changes[item.Key] = new ChangeNote<V>(ChangeType.Add, item.Value);
        base.Add(item);
    }
}
