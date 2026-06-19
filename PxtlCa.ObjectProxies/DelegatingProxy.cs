namespace PxtlCa.ObjectProxies;

public class DelegatingProxy<T> : ObjectProxy<T> {
    public delegate T Getter();
    public delegate void Setter(T value);

    protected Getter GetHandler;
    protected Setter SetHandler;
    public DelegatingProxy(Getter getHandler, Setter setHandler) {
        this.GetHandler = getHandler;
        this.SetHandler = setHandler;
    }

    public override T Val {
        get { return GetHandler(); }
        set { SetHandler(value); }
    }
}
