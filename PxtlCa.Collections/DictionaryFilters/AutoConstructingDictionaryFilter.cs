namespace PxtlCa.Collections.DictionaryFilters {
    public class AutoConstructingDictionaryFilter<K, V> : DefaultingDictionaryFilter<K, V>
    where V : new() {
        protected override bool TryGetDefaultValue(K key, out V val) {
            // can use "!" because we already declared that V is nullable in where.
            val = new V();
            return true;
        }
    }
}
