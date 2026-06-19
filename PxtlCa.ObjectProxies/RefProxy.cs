namespace PxtlCa.ObjectProxies;

public class RefProxy<T> : ObjectProxy<T> {
    protected T _val;
    public RefProxy(T val) { _val = val; }
    public override T Val {
        get { return _val; }
        set { _val = value!; }
    }

    public delegate void UsingDelegate(RefProxy<T> proxy);
    //TODO: IDisposable object wrapper?
    public static void UsingVal(ref T value, UsingDelegate procedure) {
        RefProxy<T> proxy = new RefProxy<T>(value);
        procedure(proxy);
        value = proxy.Val;
    }
}
