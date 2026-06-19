namespace PxtlCa.Collections.DictMixins
{
    public class AutoConstructingDictMixin<K, V> : DefaultingDictMixin<K, V> where V : new()
    {
        protected override V _DefaultValueConstructorImplementation(K key)
        {
            return new V();
        }
    }
}
