namespace PxtlCa.Collections;

/// <summary>
/// Filters are managed by the <see cref="MixableDictionary{K, V}"/> as a linked list.
/// This interface represents a node in that list.
/// </summary>
public interface IDictionaryFilterNode<K, V> : IDictionary<K, V> {
    internal void SetDictionary(MixableDictionary<K, V> thisDictionary);
}