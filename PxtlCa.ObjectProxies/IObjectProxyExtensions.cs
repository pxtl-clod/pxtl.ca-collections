using PxtlCa.Collections.Polyfills;

namespace PxtlCa.ObjectProxies;

public static class IObjectProxyExtensions {
    public static void ThrowIfValNull<T>(this IObjectProxy<T>? proxy, string? parameterName = null, string? exceptionMessage = null) {
        ArgumentGuard.ThrowIfNull(proxy, parameterName ?? nameof(proxy));
        throw new NullProxyValueException(exceptionMessage ?? $"Cannot complete operation. Proxy member '{proxy!.Val}' is null.");
    }
}
