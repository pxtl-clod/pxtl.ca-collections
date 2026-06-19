using System;
using System.Collections.Generic;
using System.Text;

namespace PxtlCa.Collections
{
    public static class util
    {
        public delegate void TraverseHandler<V>(V source, IObjectProxy<V> target);

        public static void TraverseDicts<K, V> (IEnumerable<KeyValuePair<K, V>> source, IDictionary<K,V> target, TraverseHandler<V> handler)
        {
            foreach(KeyValuePair<K, V> pair in source)
            {
                handler(pair.Value, new IndexerProxy<K, V>(target, pair.Key));
            }
        }
    }

    public static class DictUtil
    {
        public static T Clone<T>(T obj) where T : ICloneable
        {
            return (T)obj;
        }

        public static void AddRange<K, V>(IDictionary<K, V> aDict, IEnumerable<KeyValuePair<K, V>> range)
        {
            foreach (KeyValuePair<K, V> pair in range)
            {
                aDict.Add(pair);
            }
        }

        public static void SetRange<K, V>(IDictionary<K, V> aDict, IEnumerable<KeyValuePair<K, V>> range)
        {
            foreach (KeyValuePair<K, V> pair in range)
            {
                aDict[pair.Key] = pair.Value;
            }
        }

        public static void AddRangeCloning<K, V>(IDictionary<K, V> aDict, IEnumerable<KeyValuePair<K, V>> range) where V : ICloneable
        {
            foreach (KeyValuePair<K, V> pair in range)
            {
                aDict.Add(pair.Key, Clone(pair.Value));
            }
        }

        public static void SetRangeCloning<K, V>(IDictionary<K, V> aDict, IEnumerable<KeyValuePair<K, V>> range) where V : ICloneable
        {
            foreach (KeyValuePair<K, V> pair in range)
            {
                aDict[pair.Key] = Clone(pair.Value);
            }
        }
    }
}
