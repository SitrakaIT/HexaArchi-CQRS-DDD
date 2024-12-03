namespace SIT.Shared.Extensions;

public static class GenericTypeExtensions
{
    public static bool IsDefault<T>(this T value) => Equals(value, default(T));
}