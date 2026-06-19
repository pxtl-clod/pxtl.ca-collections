namespace PxtlCa.Collections.DictMixins
{
    public class NullReferenceTypeDefaultingDictMixin<K, V> : DefaultingDictMixin<K, V>
    where V : class? {
        protected override bool TryGetDefaultValue(K key, out V val) {
            // can use "!" because we already declared that V is nullable in where.
            val = null!;
            return true;
        }
    }
}
