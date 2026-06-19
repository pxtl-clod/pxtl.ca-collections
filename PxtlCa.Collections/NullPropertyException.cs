namespace PxtlCa.Collections;

public class NullPropertyException : Exception
{
    public NullPropertyException() { }

    public NullPropertyException(string? message, string propertyName) : base(ResolveMessage(message, propertyName)) {
        PropertyName = propertyName;
    }

    public NullPropertyException(string? message, string propertyName, Exception? innerException)
        : base(ResolveMessage(message, propertyName), innerException) {
        PropertyName = propertyName;
    }

    public string? PropertyName { get; set; }

    private static string ResolveMessage(string? message, string propertyName)
        => message
        ?? $"The object property '{propertyName}' is null";
}