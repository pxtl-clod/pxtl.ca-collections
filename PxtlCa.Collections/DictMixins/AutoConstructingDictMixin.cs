namespace PxtlCa.Collections.DictMixins
{
    public class AutoConstructingDictMixin<K, V> : DefaultingDictMixin<K, V> 
    where V : new() {
        protected override bool TryGetDefaultValue(K key, out V val) {
            // can use "!" because we already declared that V is nullable in where.
            val = new V();
            return true;
        }
    }
}
