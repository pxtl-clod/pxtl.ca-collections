namespace PxtlCa.Collections.DictMixins
{
    public abstract class DefaultingDictMixin<K, V> : DictMixin<K, V>
    {
        #region Constructors

        public DefaultingDictMixin() : base() {

        }
        #endregion

        protected abstract bool TryGetDefaultValue(K key, out V val);

        public override V GetVal(IDictionary<K,V> thisDict, MixableDict<K,V>.IDictMixinHolder nextMixin, K key)
        {
            if (thisDict.TryGetValue(key, out V val)) {
                return nextMixin[key];
            } else if (TryGetDefaultValue(key, out V constructedVal)) {
                thisDict[key] = constructedVal;
                return constructedVal;
            } else {
                return nextMixin[key];
            }
        }
    }
}
