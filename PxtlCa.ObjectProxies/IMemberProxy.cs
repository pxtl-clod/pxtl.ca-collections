namespace PxtlCa.ObjectProxies;

/// <summary>
/// This interface is intended to wrap an object member in a way that accessing
/// or modifying that member will require a reference to the parent object.
/// Analogous to the C++ pointer-to-member.
/// </summary>
/// <typeparam name="TParent"></typeparam>
/// <typeparam name="TMember"></typeparam>
public interface IMemberProxy<TParent, TMember> {
    TMember Get(TParent parent);
    void Set(IObjectProxy<TParent> parent, TMember memberValue);
    ICollection<string> AsPathStrings { get; }
}