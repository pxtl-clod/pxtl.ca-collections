namespace PxtlCa.ObjectProxies;

public interface IKeyedProxyFactory<D, K, V> {
    IObjectProxy<V> Create(IObjectProxy<D> dict, K key);
}
