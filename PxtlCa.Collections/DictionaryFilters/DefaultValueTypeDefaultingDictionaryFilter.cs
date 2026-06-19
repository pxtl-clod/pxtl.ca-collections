namespace PxtlCa.Collections.DictionaryFilters;

public class DefaultValueTypeDefaultingDictionaryFilter<K, V> : DefaultingDictionaryFilter<K, V>
where V : struct {
    protected override bool TryGetDefaultValue(K key, out V val) {
        // can use "!" because we already declared that V is nullable in where.
        val = default!;
        return true;
    }
}
