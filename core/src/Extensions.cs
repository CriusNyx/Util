namespace CriusNyx.Util;

public static class Extensions
{
  public static T NotNull<T>(this T? value, string? fieldName = null)
  {
    if (value == null)
    {
      if (fieldName != null)
      {
        throw new NullReferenceException($"{fieldName} is null");
      }
      else
      {
        throw new NullReferenceException();
      }
    }
    return value!;
  }

  public static (T, U) With<T, U>(this T value, U other)
  {
    return (value, other);
  }

  public static (T, U, V) AndWith<T, U, V>(this (T, U) value, V other)
  {
    return (value.Item1, value.Item2, other);
  }

  public static (T, U, V, W) AndWith<T, U, V, W>(this (T, U, V) value, W other)
  {
    return (value.Item1, value.Item2, value.Item3, other);
  }
}
