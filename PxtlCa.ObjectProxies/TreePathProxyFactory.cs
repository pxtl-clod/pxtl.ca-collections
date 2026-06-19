namespace PxtlCa.ObjectProxies;

public class TreePathProxyFactory<D, K> : IKeyedProxyFactory<D, IList<K>, D> where D : IDictionary<K, D> {
    private TreePathProxyFactory() { }
    public static readonly TreePathProxyFactory<D, K> Instance = new TreePathProxyFactory<D, K>();
    public IObjectProxy<D> Create(IObjectProxy<D> dict, IList<K> keyList) {
        return new TreePathProxy<D, K>(dict, keyList);
    }
}
