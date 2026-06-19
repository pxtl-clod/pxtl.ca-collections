namespace PxtlCa.Collections.DictMixins;

public class DefaultValueTypeDefaultingDictMixin<K, V> : DefaultingDictMixin<K, V>
where V : struct {
    protected override bool TryGetDefaultValue(K key, out V val) {
        // can use "!" because we already declared that V is nullable in where.
        val = default(V)!;
        return true;
    }
}
