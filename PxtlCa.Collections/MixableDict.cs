using System.Collections;

namespace PxtlCa.Collections;

/// <summary>
/// A dictionary allowing mixin extensions for custom behavior.
/// </summary>
public class MixableDict<K, V> : VirtualDict<K, V> {
    public MixableDict() : base()
    {
        _mixinListHead = new DictMixinHolder(new TerminalDictMixin(_BaseDict));
    }

    public MixableDict(IEqualityComparer<K> comparer)
        : base(comparer)
    {
        _mixinListHead = new DictMixinHolder(new TerminalDictMixin(_BaseDict));
    }

    public MixableDict(int capacity)
        : base(capacity)
    {
        _mixinListHead = new DictMixinHolder(new TerminalDictMixin(_BaseDict));
    }

    public MixableDict(int capacity, IEqualityComparer<K> comparer)
        : base(capacity, comparer)
    {
        _mixinListHead = new DictMixinHolder(new TerminalDictMixin(_BaseDict));
    }

    public MixableDict(IDictionary<K, V> originalDictionary)
        : base(originalDictionary)
    {
        _mixinListHead = new DictMixinHolder(new TerminalDictMixin(_BaseDict));
    }

    protected MixableDict(IDictionary<K, V> originalDictionary, bool wrap)
        : base(originalDictionary, wrap)
    {
        _mixinListHead = new DictMixinHolder(new TerminalDictMixin(_BaseDict));
    }

    public MixableDict(IDictionary<K, V> originalDictionary, IEqualityComparer<K> comparer)
        : base(originalDictionary, comparer)
    {
        _mixinListHead = new DictMixinHolder(new TerminalDictMixin(_BaseDict));
    }

    /// <summary>
    /// The head of the mixin list for extending collection behavior.
    /// </summary>
    private DictMixinHolder _mixinListHead;

    /// <summary>
    /// Gets all mixins applied to this dictionary for extension.
    /// </summary>
    public IEnumerable<DictMixin<K, V>> Mixins {
        get
        {
            DictMixinHolder mixinHolder = _mixinListHead;
            while (mixinHolder.NextMixin != null)
            {
                yield return mixinHolder.Mixin;
                mixinHolder = mixinHolder.NextMixin;
            }
        }
        set
        {
            DictMixinHolder mixins = new DictMixinHolder(new TerminalDictMixin(_BaseDict));
            foreach (DictMixin<K, V> mixin in value)
            {
                DictMixinHolder oldHeadMixin = mixins;
                mixins = new DictMixinHolder(mixin);
                mixins.NextMixin = oldHeadMixin;
            }

            _mixinListHead = mixins;
            _mixinListHead.SetDict(this);
        }
    }

    protected void _InitializeMixins()
    {
        _mixinListHead = new DictMixinHolder(new TerminalDictMixin(_BaseDict));
        _mixinListHead.SetDict(this);
    }

    public override V this[K key]
    {
        get
        {
            return _mixinListHead[key];
        }
        set
        {
            _mixinListHead[key] = value;
        }
    }

    public override bool Remove(K key)
    {
        return _mixinListHead.Remove(key);
    }

    public override void Add(K key, V value)
    {
        _mixinListHead.Add(key, value);
    }

    public override ICollection<K> Keys
    {
        get { return _mixinListHead.Keys; }
    }

    public override ICollection<V> Values
    {
        get { return _mixinListHead.Values; }
    }

    public override bool ContainsKey(K key)
    {
        return _mixinListHead.ContainsKey(key);
    }

    public override bool Contains(KeyValuePair<K, V> item)
    {
        return _mixinListHead.Contains(item);
    }
    public override bool Remove(KeyValuePair<K, V> item)
    {
        return _mixinListHead.Remove(item);
    }
    public override void Clear()
    {
        _mixinListHead.Clear();
    }
    public override int Count
    {
        get { return _mixinListHead.Count; }
    }

    public override void Add(KeyValuePair<K, V> item)
    {
        _mixinListHead.Add(item.Key, item.Value);
    }
    public override void CopyTo(KeyValuePair<K, V>[] array, int arrayIndex)
    {
        _mixinListHead.CopyTo(array, arrayIndex);
    }

    public override IEnumerator GetObjectEnumerator()
    {
        return _mixinListHead.GetObjectEnumerator();
    }

    /// <summary>
    /// Returns an ordered KeyValuePair enumerator.
    /// </summary>
    public override IEnumerator<KeyValuePair<K, V>> GetGenericEnumerator()
    {
        return _mixinListHead.GetGenericEnumerator();
    }

    public override bool TryGetValue(K key, out V val)
    {
        return _mixinListHead.TryGetValue(key, out val);
    }

    public class TerminalDictMixin : DictMixin<K, V>
    {
        private IDictionary<K, V> _baseDict;
        internal protected TerminalDictMixin(IDictionary<K, V> baseDict) 
        {
            _baseDict = baseDict;
        }

        internal protected virtual TerminalDictMixin NextMixin
        {
            get
            {
                return null;
            }
        }

        #region Dictionary Methods
        public override bool Remove(IDictionary<K, V> thisDict, MixableDict<K, V>.DictMixinHolder nextMixin, K key)
        {
            return _baseDict.Remove(key);
        }
        public override void Add(IDictionary<K, V> thisDict, MixableDict<K, V>.DictMixinHolder nextMixin, K key, V value)
        {
            _baseDict.Add(key, value);
        }
        public override ICollection<K> GetKeys(IDictionary<K, V> thisDict, MixableDict<K, V>.DictMixinHolder nextMixin)
        {
            return _baseDict.Keys;
        }
        public override ICollection<V> GetValues(IDictionary<K, V> thisDict, MixableDict<K, V>.DictMixinHolder nextMixin)
        {
            return _baseDict.Values;
        }

        public override bool ContainsKey(IDictionary<K, V> thisDict, MixableDict<K, V>.DictMixinHolder nextMixin, K key)
        {
            return _baseDict.ContainsKey(key);
        }

        public override bool Contains(IDictionary<K, V> thisDict, MixableDict<K, V>.DictMixinHolder nextMixin, KeyValuePair<K, V> item)
        {
            return _baseDict.Contains(item);
        }

        public override bool Remove(IDictionary<K, V> thisDict, MixableDict<K, V>.DictMixinHolder nextMixin, KeyValuePair<K, V> item)
        {
            return _baseDict.Remove(item);
        }

        public override void Clear(IDictionary<K, V> thisDict, MixableDict<K, V>.DictMixinHolder nextMixin)
        {
            _baseDict.Clear();
        }

        public override int GetCount(IDictionary<K, V> thisDict, MixableDict<K, V>.DictMixinHolder nextMixin)
        {
            return _baseDict.Count;
        }

        public override void CopyTo(IDictionary<K, V> thisDict, MixableDict<K, V>.DictMixinHolder nextMixin, KeyValuePair<K, V>[] array, int arrayIndex)
        {
            _baseDict.CopyTo(array, arrayIndex);
        }

        public override IEnumerator GetObjectEnumerator(IDictionary<K, V> thisDict, MixableDict<K, V>.DictMixinHolder nextMixin)
        {
            return (_baseDict as IEnumerable).GetEnumerator();
        }

        public override IEnumerator<KeyValuePair<K, V>> GetGenericEnumerator(IDictionary<K, V> thisDict, MixableDict<K, V>.DictMixinHolder nextMixin)
        {
            return (_baseDict as IEnumerable<KeyValuePair<K, V>>).GetEnumerator();
        }

        public override bool TryGetValue(IDictionary<K, V> thisDict, MixableDict<K, V>.DictMixinHolder nextMixin, K key, out V val)
        {
            return _baseDict.TryGetValue(key, out val);
        }

        public override V GetVal(IDictionary<K, V> thisDict, MixableDict<K, V>.DictMixinHolder NextMixin, K key)
        {
            return _baseDict[key];
        }

        public override void SetVal(IDictionary<K, V> thisDict, MixableDict<K, V>.DictMixinHolder NextMixin, K key, V value)
        {
            _baseDict[key] = value;
        }

        #endregion
    }

    public class DictMixinHolder
    {
        private DictMixin<K, V> _mixin;
        internal DictMixin<K, V> Mixin { get { return _mixin; } }
        internal DictMixinHolder(DictMixin<K, V> mixin)
        {
            _mixin = mixin;
        }

        private DictMixinHolder _NextMixin;
        protected internal DictMixinHolder NextMixin
        {
            get 
            { 
                    return _NextMixin;
            }
            set
            {
                _NextMixin = value;
            }
        }

        private MixableDict<K, V> _thisDict;
        protected internal void SetDict(MixableDict<K, V> thisDict)
        {
            _thisDict = thisDict;
            if(NextMixin != null) NextMixin.SetDict(thisDict);
        }

        #region Dictionary Methods

        public virtual V this[K key]
        {
            get
            {
                return Mixin.GetVal(_thisDict, NextMixin, key);
            }
            set
            {
                Mixin.SetVal(_thisDict, NextMixin, key, value);
            }
        }

        public bool Remove(K key)
        {
            return Mixin.Remove(_thisDict, NextMixin, key);
        }
        public void Add(K key, V value)
        {
            Mixin.Add(_thisDict, NextMixin, key, value);
        }
        public ICollection<K> Keys
        {
            get { return Mixin.GetKeys(_thisDict, NextMixin); }
        }
        public ICollection<V> Values
        {
            get{return Mixin.GetValues(_thisDict, NextMixin);}
        }

        public bool ContainsKey(K key)
        {
            return Mixin.ContainsKey(_thisDict, NextMixin, key);
        }

        public bool Contains(KeyValuePair<K, V> item)
        {
            return Mixin.Contains(_thisDict, NextMixin, item);
        }

        public bool Remove(KeyValuePair<K, V> item)
        {
            return Mixin.Remove(_thisDict, NextMixin, item);
        }

        public void Clear()
        {
            Mixin.Clear(_thisDict, NextMixin);
        }

        public int Count
        {
            get { return Mixin.GetCount(_thisDict, NextMixin); }
        }

        public void CopyTo(KeyValuePair<K, V>[] array, int arrayIndex)
        {
            Mixin.CopyTo(_thisDict, NextMixin, array, arrayIndex);
        }

        public IEnumerator GetObjectEnumerator()
        {
            return Mixin.GetObjectEnumerator(_thisDict, NextMixin);
        }

        public IEnumerator<KeyValuePair<K, V>> GetGenericEnumerator()
        {
            return Mixin.GetGenericEnumerator(_thisDict, NextMixin);
        }

        public bool TryGetValue(K key, out V val)
        {
            return Mixin.TryGetValue(_thisDict, NextMixin, key, out val);
        }
        #endregion
    }
}