namespace PxtlCa.Collections.DictionaryFilters {
    public abstract class DefaultingDictionaryFilter<K, V> : DictionaryFilter<K, V> {
        #region Constructors

        public DefaultingDictionaryFilter() : base() {

        }
        #endregion

        protected abstract bool TryGetDefaultValue(K key, out V val);

        public override V GetVal(IDictionary<K, V> thisDictionary, IDictionaryFilterNode<K, V> nextFilter, K key) {
            if (thisDictionary.TryGetValue(key, out V val)) {
                return nextFilter[key];
            } else if (TryGetDefaultValue(key, out V constructedVal)) {
                thisDictionary[key] = constructedVal;
                return constructedVal;
            } else {
                return nextFilter[key];
            }
        }
    }
}
