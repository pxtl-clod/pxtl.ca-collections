namespace PxtLCa.Collections.Polyfills;

public static class ArgumentGuard
{
    public static void ThrowIfNull(object? argument, string paramName)
    {
        if (null == argument)
        {
            throw new ArgumentNullException(paramName);
        }
    }
}
