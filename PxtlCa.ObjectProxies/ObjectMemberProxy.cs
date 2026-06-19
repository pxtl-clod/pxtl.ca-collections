using PxtlCa.Collections.Polyfills;

namespace PxtlCa.ObjectProxies;

public class ObjectMemberProxy<TParent, TMember> : IObjectMemberProxy<TParent, TMember> {
    public IMemberProxy<TParent, TMember> MemberProxy { get; }
    public IObjectProxy<TParent> Parent { get; }

    public ObjectMemberProxy(IMemberProxy<TParent, TMember> memberProxy, IObjectProxy<TParent> parent) {
        ArgumentGuard.ThrowIfNull(memberProxy, nameof(memberProxy));
        ArgumentGuard.ThrowIfNull(parent, nameof(parent));

        MemberProxy = memberProxy;
        Parent = parent;
    }

    public TMember Val {
        get {
            return MemberProxy.Get(Parent!.Val!);
        }
        set {
            MemberProxy.Set(Parent, value);
        }
    }
}