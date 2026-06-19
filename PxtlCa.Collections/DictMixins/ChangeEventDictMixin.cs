namespace PxtlCa.Collections.DictMixins
{
    public class ChangeEventDictMixin<K, V> : DictMixin<K, V>
    {
        public ChangeEventDictMixin(ChangeHandler changed)
        {
            Changed = changed;
        }

        public delegate void ChangeHandler(K key, ChangeNote<V> change);
        public ChangeHandler Changed;

        protected void NoteChange(K key, ChangeType changeType, V? value)
        {
            if (Changed != null) {
                Changed(key, new ChangeNote<V>(changeType, value));
            }
        }

        // This is the raison d'etre of this whole class - an indexer
        // which notes any changes
        public override void SetVal(IDictionary<K,V> thisDict, MixableDict<K,V>.IDictMixinHolder nextMixin, K key, V value)
        {
            nextMixin[key] = value;
            NoteChange(key, ChangeType.Set, value);
        }

        // Delegating implementations of all other methods.
        public override bool Remove(IDictionary<K,V> thisDict, MixableDict<K,V>.IDictMixinHolder nextMixin, K key)
        {
            NoteChange(key, ChangeType.Remove, default);
            return nextMixin.Remove(key);
        }
        public override void Add(IDictionary<K, V> thisDict, MixableDict<K, V>.IDictMixinHolder nextMixin, K key, V value)
        {
            nextMixin.Add(key, value);
            NoteChange(key, ChangeType.Add, value);
        }
        public override bool Remove(IDictionary<K, V> thisDict, MixableDict<K, V>.IDictMixinHolder nextMixin, KeyValuePair<K, V> item)
        {
            NoteChange(item.Key, ChangeType.Remove, default(V));
            return nextMixin.Remove(item);
        }

        public override void Clear(IDictionary<K, V> thisDict, MixableDict<K, V>.IDictMixinHolder nextMixin)
        {
            foreach (K key in thisDict.Keys)
            {
                NoteChange(key, ChangeType.Remove, default(V));
            }
            nextMixin.Clear();
        }
    }
}
