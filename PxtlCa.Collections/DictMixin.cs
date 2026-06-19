using System.Collections;

namespace PxtlCa.Collections;

public class DictMixin<K, V>
{
    #region Dictionary Methods
    public virtual bool Remove(IDictionary<K, V> thisDict, MixableDict<K, V>.IDictMixinHolder nextMixin, K key)
    {
        return nextMixin.Remove(key);
    }

    public virtual void Add(IDictionary<K, V> thisDict, MixableDict<K, V>.IDictMixinHolder nextMixin, K key, V value)
    {
        nextMixin.Add(key, value);
    }

    public virtual ICollection<K> GetKeys(IDictionary<K, V> thisDict, MixableDict<K, V>.IDictMixinHolder nextMixin)
    {
        return nextMixin.Keys;
    }

    public virtual ICollection<V> GetValues(IDictionary<K, V> thisDict, MixableDict<K, V>.IDictMixinHolder nextMixin)
    {
        return nextMixin.Values;
    }

    public virtual bool ContainsKey(IDictionary<K, V> thisDict, MixableDict<K, V>.IDictMixinHolder nextMixin, K key)
    {
        return nextMixin.ContainsKey(key);
    }

    public virtual bool Contains(IDictionary<K, V> thisDict, MixableDict<K, V>.IDictMixinHolder nextMixin, KeyValuePair<K, V> item)
    {
        return nextMixin.Contains(item);
    }

    public virtual bool Remove(IDictionary<K, V> thisDict, MixableDict<K, V>.IDictMixinHolder nextMixin, KeyValuePair<K, V> item)
    {
        return nextMixin.Remove(item);
    }

    public virtual void Clear(IDictionary<K, V> thisDict, MixableDict<K, V>.IDictMixinHolder nextMixin)
    {
        nextMixin.Clear();
    }

    public virtual int GetCount(IDictionary<K, V> thisDict, MixableDict<K, V>.IDictMixinHolder nextMixin)
    {
        return nextMixin.Count;
    }

    public virtual void CopyTo(IDictionary<K, V> thisDict, MixableDict<K, V>.IDictMixinHolder nextMixin, KeyValuePair<K, V>[] array, int arrayIndex)
    {
        nextMixin.CopyTo(array, arrayIndex);
    }

    public virtual IEnumerator GetObjectEnumerator(IDictionary<K, V> thisDict, MixableDict<K, V>.IDictMixinHolder nextMixin)
    {
        return nextMixin.GetObjectEnumerator();
    }

    public virtual IEnumerator<KeyValuePair<K, V>> GetGenericEnumerator(IDictionary<K, V> thisDict, MixableDict<K, V>.IDictMixinHolder nextMixin)
    {
        return nextMixin.GetGenericEnumerator();
    }

    public virtual bool TryGetValue(IDictionary<K, V> thisDict, MixableDict<K, V>.IDictMixinHolder nextMixin, K key, out V val)
    {
        return nextMixin.TryGetValue(key, out val);
    }

    public virtual V GetVal(IDictionary<K, V> thisDict, MixableDict<K, V>.IDictMixinHolder NextMixin, K key)
    {
        return NextMixin[key];
    }

    public virtual void SetVal(IDictionary<K, V> thisDict, MixableDict<K, V>.IDictMixinHolder NextMixin, K key, V value)
    {
        NextMixin[key] = value;
    }
    #endregion
}