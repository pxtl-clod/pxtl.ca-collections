namespace PxtlCa.ObjectProxies;

public class DictionaryProxyFactory<D, K, V> : IKeyedProxyFactory<D, K, V> where D : IDictionary<K, V> {
    private DictionaryProxyFactory() { }
    public static readonly DictionaryProxyFactory<D, K, V> Instance = new DictionaryProxyFactory<D, K, V>();
    public IObjectProxy<V> Create(IObjectProxy<D> dict, K key) {
        return Create(dict.Val, key);
    }

    public IObjectProxy<V> Create(D dict, K key) {
        return new DictionaryProxy<K, V>(dict, key);
    }
}
