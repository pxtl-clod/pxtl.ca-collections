namespace PxtlCa.ObjectProxies;

/// <summary>
/// This interface is intended for wrapping a single object with no secondary
/// parameters needed to access or modify it. Analogous to a C++ pointer.
/// </summary>
public interface IObjectProxy<T> {
    T Val { get; set; }
}
