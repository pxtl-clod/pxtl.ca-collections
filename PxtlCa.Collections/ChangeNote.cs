namespace PxtlCa.Collections
{
    public struct ChangeNote<V>
    {
        public readonly ChangeType Type;
        public readonly V? Val;

        public ChangeNote(ChangeType type, V? val)
            : this(type)
        {
            Val = val;
        }

        public ChangeNote(ChangeType type)
        {
            Type = type;
            Val = default(V);
        }
    }

    public enum ChangeType
    {
        Set = 0,
        Add, //refers to the dictionary "add" operation, not necessarily a new entry.
        Remove
    };
}
