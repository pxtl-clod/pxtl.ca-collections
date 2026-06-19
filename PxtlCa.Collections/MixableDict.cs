using System.Collections;
using PxtLCa.Collections.Polyfills;

namespace PxtlCa.Collections;

/// <summary>
/// A dictionary allowing mixin extensions for custom behavior.
/// </summary>
public class MixableDict<K, V> : VirtualDict<K, V> {
    public MixableDict() : base()
    {
        _mixinListHead = new DictMixinLinkNode(new TerminalDictMixin(BaseDict), BaseDict);
    }

    public MixableDict(IEqualityComparer<K> comparer)
        : base(comparer)
    {
        _mixinListHead = new DictMixinLinkNode(new TerminalDictMixin(BaseDict), BaseDict);
    }

    public MixableDict(int capacity)
        : base(capacity)
    {
        _mixinListHead = new DictMixinLinkNode(new TerminalDictMixin(BaseDict), BaseDict);
    }

    public MixableDict(int capacity, IEqualityComparer<K> comparer)
        : base(capacity, comparer)
    {
        _mixinListHead = new DictMixinLinkNode(new TerminalDictMixin(BaseDict), BaseDict);
    }

    public MixableDict(IDictionary<K, V> originalDictionary)
        : base(originalDictionary)
    {
        _mixinListHead = new DictMixinLinkNode(new TerminalDictMixin(BaseDict), BaseDict);
    }

    protected MixableDict(IDictionary<K, V> originalDictionary, bool wrap)
        : base(originalDictionary, wrap)
    {
        _mixinListHead = new DictMixinLinkNode(new TerminalDictMixin(BaseDict), BaseDict);
    }

    public MixableDict(IDictionary<K, V> originalDictionary, IEqualityComparer<K> comparer)
        : base(originalDictionary, comparer)
    {
        _mixinListHead = new DictMixinLinkNode(new TerminalDictMixin(BaseDict), BaseDict);
    }

    /// <summary>
    /// The head of the mixin list for extending collection behavior.
    /// </summary>
    private IDictMixinHolder _mixinListHead;

    /// <summary>
    /// Gets all mixins applied to this dictionary for extension.
    /// </summary>
    public IEnumerable<DictMixin<K, V>> Mixins {
        get
        {
            var mixinHolder = _mixinListHead;
            while (true)
            {
                yield return mixinHolder.Mixin;
                if (mixinHolder is DictMixinLinkNode) {
                    mixinHolder = ((DictMixinLinkNode)mixinHolder).NextMixin;
                }
            }
        }
        set
        {
            var mixins = new DictMixinLinkNode(new TerminalDictMixin(BaseDict), BaseDict);
            foreach (DictMixin<K, V> mixin in value)
            {
                IDictMixinHolder oldHeadMixin = mixins;
                mixins = new DictMixinLinkNode(mixin, BaseDict);
                mixins.NextMixin = oldHeadMixin;
            }

            _mixinListHead = mixins;
            _mixinListHead.SetDict(this);
        }
    }

    protected void _InitializeMixins()
    {
        _mixinListHead = new DictMixinLinkNode(new TerminalDictMixin(BaseDict), BaseDict);
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

    public class TerminalDictMixin : DictMixin<K, V>, IDictMixinHolder
    {
        private IDictionary<K, V> _baseDict;

        #region IDictMixinHolder Members
        public DictMixin<K, V> Mixin => this;
        public ICollection<K> Keys => _baseDict.Keys;
        public ICollection<V> Values => _baseDict.Values;
        public int Count => _baseDict.Count;

        public V this[K key] {
            get => _baseDict[key];
            set => _baseDict[key] = value;
        }

        public void SetDict(MixableDict<K, V> baseDict) {
            _baseDict = baseDict;
        }
        #endregion

        #region Constuctors
        internal protected TerminalDictMixin(IDictionary<K, V> baseDict) 
        {
            _baseDict = baseDict;
        }
        #endregion

        #region DictMixin Members
        public override bool Remove(IDictionary<K, V> thisDict, MixableDict<K, V>.IDictMixinHolder nextMixin, K key)
        {
            return _baseDict.Remove(key);
        }
        public override void Add(IDictionary<K, V> thisDict, MixableDict<K, V>.IDictMixinHolder nextMixin, K key, V value)
        {
            _baseDict.Add(key, value);
        }
        public override ICollection<K> GetKeys(IDictionary<K, V> thisDict, MixableDict<K, V>.IDictMixinHolder nextMixin)
        {
            return _baseDict.Keys;
        }
        public override ICollection<V> GetValues(IDictionary<K, V> thisDict, MixableDict<K, V>.IDictMixinHolder nextMixin)
        {
            return _baseDict.Values;
        }

        public override bool ContainsKey(IDictionary<K, V> thisDict, MixableDict<K, V>.IDictMixinHolder nextMixin, K key)
        {
            return _baseDict.ContainsKey(key);
        }

        public override bool Contains(IDictionary<K, V> thisDict, MixableDict<K, V>.IDictMixinHolder nextMixin, KeyValuePair<K, V> item)
        {
            return _baseDict.Contains(item);
        }

        public override bool Remove(IDictionary<K, V> thisDict, MixableDict<K, V>.IDictMixinHolder nextMixin, KeyValuePair<K, V> item)
        {
            return _baseDict.Remove(item);
        }

        public override void Clear(IDictionary<K, V> thisDict, MixableDict<K, V>.IDictMixinHolder nextMixin)
        {
            _baseDict.Clear();
        }

        public override int GetCount(IDictionary<K, V> thisDict, MixableDict<K, V>.IDictMixinHolder nextMixin)
        {
            return _baseDict.Count;
        }

        public override void CopyTo(IDictionary<K, V> thisDict, MixableDict<K, V>.IDictMixinHolder nextMixin, KeyValuePair<K, V>[] array, int arrayIndex)
        {
            _baseDict.CopyTo(array, arrayIndex);
        }

        public override IEnumerator GetObjectEnumerator(IDictionary<K, V> thisDict, MixableDict<K, V>.IDictMixinHolder nextMixin)
        {
            return (_baseDict as IEnumerable).GetEnumerator();
        }

        public override IEnumerator<KeyValuePair<K, V>> GetGenericEnumerator(IDictionary<K, V> thisDict, MixableDict<K, V>.IDictMixinHolder nextMixin)
        {
            return (_baseDict as IEnumerable<KeyValuePair<K, V>>).GetEnumerator();
        }

        public override bool TryGetValue(IDictionary<K, V> thisDict, MixableDict<K, V>.IDictMixinHolder nextMixin, K key, out V val)
        {
            return _baseDict.TryGetValue(key, out val);
        }

        public override V GetVal(IDictionary<K, V> thisDict, MixableDict<K, V>.IDictMixinHolder NextMixin, K key)
        {
            return _baseDict[key];
        }

        public override void SetVal(IDictionary<K, V> thisDict, MixableDict<K, V>.IDictMixinHolder NextMixin, K key, V value)
        {
            _baseDict[key] = value;
        }

        public void Add(K key, V value) {
            throw new NotImplementedException();
        }

        public void Clear() {
            throw new NotImplementedException();
        }

        public bool Contains(KeyValuePair<K, V> item) {
            throw new NotImplementedException();
        }

        public bool ContainsKey(K key) {
            throw new NotImplementedException();
        }

        public void CopyTo(KeyValuePair<K, V>[] array, int arrayIndex) {
            throw new NotImplementedException();
        }

        public IEnumerator<KeyValuePair<K, V>> GetGenericEnumerator() {
            throw new NotImplementedException();
        }

        public IEnumerator GetObjectEnumerator() {
            throw new NotImplementedException();
        }

        public bool Remove(K key) {
            throw new NotImplementedException();
        }

        public bool Remove(KeyValuePair<K, V> item) {
            throw new NotImplementedException();
        }

        public bool TryGetValue(K key, out V val) {
            throw new NotImplementedException();
        }

        #endregion
    }

    public interface IDictMixinHolder {
        DictMixin<K, V> Mixin { get; }

        V this[K key] { get; set; }

        ICollection<K> Keys { get; }
        ICollection<V> Values { get; }
        int Count { get; }

        void Add(K key, V value);
        void Clear();
        bool Contains(KeyValuePair<K, V> item);
        bool ContainsKey(K key);
        void CopyTo(KeyValuePair<K, V>[] array, int arrayIndex);
        IEnumerator<KeyValuePair<K, V>> GetGenericEnumerator();
        IEnumerator GetObjectEnumerator();
        bool Remove(K key);
        bool Remove(KeyValuePair<K, V> item);
        bool TryGetValue(K key, out V val);
        internal void SetDict(MixableDict<K, V> thisDict);
    }

    public class DictMixinLinkNode : IDictMixinHolder {
        private DictMixin<K, V> _mixin;
        public DictMixin<K, V> Mixin { get { return _mixin; } }
        internal DictMixinLinkNode(DictMixin<K, V> mixin, IDictionary<K, V> baseDict) {
            ArgumentGuard.ThrowIfNull(mixin, nameof(mixin));
            ArgumentGuard.ThrowIfNull(baseDict, nameof(baseDict));
            NextMixin = new TerminalDictMixin(baseDict);
            _mixin = mixin!;
        }

        protected internal IDictMixinHolder NextMixin { get; set; }

        private MixableDict<K, V>? _thisDict;
        private void ThrowIfThisDictNull() {
            throw new NullPropertyException($"Cannot complete operation. '{nameof(_thisDict)}' is null.  Use '{nameof(SetDict)}' to provide a value.", nameof(_thisDict));
        }
        public void SetDict(MixableDict<K, V> thisDict) {
            _thisDict = thisDict;
            NextMixin.SetDict(thisDict);
        }

        #region Dictionary Methods

        public virtual V this[K key] {
            get {
                ThrowIfThisDictNull();
                return Mixin.GetVal(_thisDict!, NextMixin, key);
            }
            set {
                ThrowIfThisDictNull();
                Mixin.SetVal(_thisDict!, NextMixin, key, value);
            }
        }

        public bool Remove(K key) {
            ThrowIfThisDictNull();
            return Mixin.Remove(_thisDict!, NextMixin, key);
        }
        public void Add(K key, V value) {
            ThrowIfThisDictNull();
            Mixin.Add(_thisDict!, NextMixin, key, value);
        }
        public ICollection<K> Keys {
            get { 
                ThrowIfThisDictNull();
                return Mixin.GetKeys(_thisDict!, NextMixin);
            }
        }
        public ICollection<V> Values {
            get { 
                ThrowIfThisDictNull();
                return Mixin.GetValues(_thisDict!, NextMixin);
            }
        }

        public bool ContainsKey(K key) {
            ThrowIfThisDictNull();
            return Mixin.ContainsKey(_thisDict!, NextMixin, key);
        }

        public bool Contains(KeyValuePair<K, V> item) {
            ThrowIfThisDictNull();
            return Mixin.Contains(_thisDict!, NextMixin, item);
        }

        public bool Remove(KeyValuePair<K, V> item) {
            ThrowIfThisDictNull();
            return Mixin.Remove(_thisDict!, NextMixin, item);
        }

        public void Clear() {
            ThrowIfThisDictNull();
            Mixin.Clear(_thisDict!, NextMixin);
        }

        public int Count {
            get {
                ThrowIfThisDictNull();
                return Mixin.GetCount(_thisDict!, NextMixin);
            }
        }

        public void CopyTo(KeyValuePair<K, V>[] array, int arrayIndex) {
            ThrowIfThisDictNull();
            Mixin.CopyTo(_thisDict!, NextMixin, array, arrayIndex);
        }

        public IEnumerator GetObjectEnumerator() {
            ThrowIfThisDictNull();
            return Mixin.GetObjectEnumerator(_thisDict!, NextMixin);
        }

        public IEnumerator<KeyValuePair<K, V>> GetGenericEnumerator() {
            ThrowIfThisDictNull();
            return Mixin.GetGenericEnumerator(_thisDict!, NextMixin);
        }

        public bool TryGetValue(K key, out V val) {
            ThrowIfThisDictNull();
            return Mixin.TryGetValue(_thisDict!, NextMixin, key, out val);
        }
        #endregion
    }
}