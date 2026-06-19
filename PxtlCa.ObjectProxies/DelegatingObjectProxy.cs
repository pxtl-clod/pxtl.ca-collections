namespace PxtlCa.ObjectProxies;

public class DelegatingObjectProxy<T> : ObjectProxy<T> {
    public DelegatingObjectProxy(GetHandler getter, SetHandler setter) {
        Getter += getter;
        Setter += setter;
    }
    public delegate T GetHandler();
    public delegate void SetHandler(T value);
    public GetHandler Getter;
    public SetHandler Setter;

    public override T Val {
        get {
            return Getter();
        }
        set {
            Setter(value);
        }
    }
}