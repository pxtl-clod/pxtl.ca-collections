using System;
using System.Collections.Generic;
using System.Text;

namespace PxtlCa.Collections;

public static class DictionaryExtensions {
    public static T Clone<T>(T obj) where T : ICloneable {
        return (T)obj;
    }

    public static void AddRange<K, V>(IDictionary<K, V> aDictionary, IEnumerable<KeyValuePair<K, V>> range) {
        foreach (KeyValuePair<K, V> pair in range) {
            aDictionary.Add(pair);
        }
    }

    public static void SetRange<K, V>(IDictionary<K, V> aDictionary, IEnumerable<KeyValuePair<K, V>> range) {
        foreach (KeyValuePair<K, V> pair in range) {
            aDictionary[pair.Key] = pair.Value;
        }
    }

    public static void AddRangeCloning<K, V>(IDictionary<K, V> aDictionary, IEnumerable<KeyValuePair<K, V>> range) where V : ICloneable {
        foreach (KeyValuePair<K, V> pair in range) {
            aDictionary.Add(pair.Key, Clone(pair.Value));
        }
    }

    public static void SetRangeCloning<K, V>(IDictionary<K, V> aDictionary, IEnumerable<KeyValuePair<K, V>> range) where V : ICloneable {
        foreach (KeyValuePair<K, V> pair in range) {
            aDictionary[pair.Key] = Clone(pair.Value);
        }
    }
}
