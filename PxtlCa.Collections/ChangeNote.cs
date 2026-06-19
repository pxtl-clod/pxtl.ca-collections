namespace PxtlCa.Collections;

public struct ChangeNote<V> {
    public readonly ChangeNoteType Type;
    public readonly V? Val;

    public ChangeNote(ChangeNoteType type, V? val)
        : this(type) {
        Val = val;
    }

    public ChangeNote(ChangeNoteType type) {
        Type = type;
        Val = default(V);
    }
}
