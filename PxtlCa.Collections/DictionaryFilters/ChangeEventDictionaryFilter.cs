namespace PxtlCa.Collections.DictionaryFilters {
    public class ChangeEventDictionaryFilter<K, V> : DictionaryFilter<K, V> {
        public ChangeEventDictionaryFilter(ChangeHandler changed) {
            Changed = changed;
        }

        public delegate void ChangeHandler(K key, ChangeNote<V> change);
        public ChangeHandler Changed;

        protected void NoteChange(K key, ChangeNoteType changeType, V? value) {
            if (Changed != null) {
                Changed(key, new ChangeNote<V>(changeType, value));
            }
        }

        // This is the raison d'etre of this whole class - an indexer
        // which notes any changes
        public override void SetVal(IDictionary<K, V> thisDictionary, IDictionaryFilterNode<K, V> nextFilter, K key, V value) {
            nextFilter[key] = value;
            NoteChange(key, ChangeNoteType.Set, value);
        }

        // Delegating implementations of all other methods.
        public override bool Remove(IDictionary<K, V> thisDictionary, IDictionaryFilterNode<K, V> nextFilter, K key) {
            NoteChange(key, ChangeNoteType.Remove, default);
            return nextFilter.Remove(key);
        }
        public override void Add(IDictionary<K, V> thisDictionary, IDictionaryFilterNode<K, V> nextFilter, K key, V value) {
            nextFilter.Add(key, value);
            NoteChange(key, ChangeNoteType.Add, value);
        }
        public override bool Remove(IDictionary<K, V> thisDictionary, IDictionaryFilterNode<K, V> nextFilter, KeyValuePair<K, V> item) {
            NoteChange(item.Key, ChangeNoteType.Remove, default(V));
            return nextFilter.Remove(item);
        }

        public override void Clear(IDictionary<K, V> thisDictionary, IDictionaryFilterNode<K, V> nextFilter) {
            foreach (K key in thisDictionary.Keys) {
                NoteChange(key, ChangeNoteType.Remove, default(V));
            }
            nextFilter.Clear();
        }
    }
}
