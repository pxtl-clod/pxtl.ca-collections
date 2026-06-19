namespace PxtlCa.Collections.DictionaryFilters {
    public class NullReferenceTypeDefaultingDictionaryFilter<K, V> : DefaultingDictionaryFilter<K, V>
    where V : class? {
        protected override bool TryGetDefaultValue(K key, out V val) {
            // can use "!" because we already declared that V is nullable in where.
            val = null!;
            return true;
        }
    }
}
