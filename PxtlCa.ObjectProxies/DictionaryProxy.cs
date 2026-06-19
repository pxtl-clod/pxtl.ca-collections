namespace PxtlCa.ObjectProxies;

public class DictionaryProxy<TKey, TValue> : ObjectMemberProxy<IDictionary<TKey, TValue>, TValue> {
    public DictionaryProxy(IDictionary<TKey, TValue> dict, TKey key)
        : base(new DictionaryMemberProxy<TKey, TValue>(key), new RefProxy<IDictionary<TKey, TValue>>(dict)) { }
}