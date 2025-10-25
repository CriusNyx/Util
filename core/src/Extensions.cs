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

  public static T Touch<T>(this T element, Action<T> action)
  {
    action(element);
    return element;
  }

  public static T? Safe<T>(this T[] arr, int index)
  {
    if (index >= 0 && index < arr.Length)
    {
      return arr[index];
    }
    return default;
  }

  public static T? Safe<T>(this IList<T> list, int index)
  {
    if (index >= 0 && index < list.Count)
    {
      return list[index];
    }
    return default;
  }

  public static U? Safe<T, U>(this IDictionary<T, U> dict, T key)
  {
    if (dict.TryGetValue(key, out var value))
    {
      return value;
    }
    return default;
  }

  public static U Transform<T, U>(this T value, Func<T, U> transformation)
  {
    return transformation(value);
  }
}
