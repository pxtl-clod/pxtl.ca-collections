using PxtlCa.Collections.Polyfills;

namespace PxtlCa.ObjectProxies;

public class SelfProxy<T> : MemberProxy<T, T> {
    public static readonly SelfProxy<T> Instance = new SelfProxy<T>();

    public override T Get(T parent) {
        return parent;
    }

    public override void Set(IObjectProxy<T> parent, T memberValue) {
        parent.Val = memberValue;
    }

    public override ICollection<string> AsPathStrings {
        get {
            return new List<string>();
        }
    }
}