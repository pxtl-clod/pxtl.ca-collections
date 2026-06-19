namespace PxtlCa.ObjectProxies;

public class ConcatenatedMemberProxy<TRoot, TJoin, TMember> : MemberProxy<TRoot, TMember> {
    public readonly IMemberProxy<TRoot, TJoin> First;
    public readonly IMemberProxy<TJoin, TMember> Second;

    public ConcatenatedMemberProxy(IMemberProxy<TRoot, TJoin> first, IMemberProxy<TJoin, TMember> second) {
        First = first;
        Second = second;
    }

    public override TMember Get(TRoot parent) {
        return Second.Get(First.Get(parent));
    }

    public override void Set(IObjectProxy<TRoot> parent, TMember memberValue) {
        var firstObjectProxy = new ObjectMemberProxy<TRoot, TJoin>(First, parent);
        Second.Set(firstObjectProxy, memberValue);
    }

    public override ICollection<string> AsPathStrings {
        get {
            List<string> path = new List<string>(First.AsPathStrings);
            path.AddRange(Second.AsPathStrings);
            return path;
        }
    }
}