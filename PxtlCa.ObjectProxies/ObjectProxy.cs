using PxtlCa.Collections.Polyfills;

namespace PxtlCa.ObjectProxies;

public abstract class ObjectProxy<T> : IObjectProxy<T> {
    abstract public T Val { get; set; }
    public override bool Equals(object obj) {
        return Equals(Val, obj);
    }
    public override int GetHashCode() {
        return Val?.GetHashCode() ?? -1;
    }
    public override string? ToString() {
        return Val?.ToString();
    }
}
