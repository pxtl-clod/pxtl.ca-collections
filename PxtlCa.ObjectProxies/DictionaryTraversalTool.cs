namespace PxtlCa.ObjectProxies;

public static class DictionaryTraversalTool {
    public delegate void NodeSynchronizeHandler<V>(V source, IObjectProxy<V> target);

    public static void SynchronizeDictionaries<K, V>(IEnumerable<KeyValuePair<K, V>> source, IDictionary<K, V> target, NodeSynchronizeHandler<V> handler) {
        foreach (KeyValuePair<K, V> pair in source) {
            handler(pair.Value, new DictionaryProxy<K, V>(target, pair.Key));
        }
    }
}