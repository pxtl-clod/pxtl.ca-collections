namespace PxtlCa.ObjectProxies;

/// <summary>
/// This class is intended to describe cases where an <see
/// cref="IMemberProxy{TParent, TMember}"/> is curried with a corresponding
/// "parent" proxy such that this can be used as an <see
/// cref="IObjectProxy{T}"/>.  Semantically, to convert a pointer-to-member into
/// a pointer.
/// </summary>
public interface IObjectMemberProxy<TParent, TMember> : IObjectProxy<TMember> {
    IMemberProxy<TParent, TMember> MemberProxy { get; }
    IObjectProxy<TParent> Parent { get; }
}