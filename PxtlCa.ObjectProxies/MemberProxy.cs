namespace PxtlCa.ObjectProxies;

public abstract class MemberProxy<TParent, TMember> : IMemberProxy<TParent, TMember> {
    public abstract TMember Get(TParent parent);
    public abstract void Set(IObjectProxy<TParent> parent, TMember memberValue);
    public abstract ICollection<string> AsPathStrings { get; }
}
