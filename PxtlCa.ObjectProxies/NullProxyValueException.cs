namespace PxtlCa.ObjectProxies;

public class NullProxyValueException : Exception {
    public NullProxyValueException() { }

    public NullProxyValueException(string? message)
    : base(message) { }

    public NullProxyValueException(string? message, Exception? innerException)
    : base(message, innerException) { }
}