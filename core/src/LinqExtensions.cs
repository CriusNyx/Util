using System.Collections;

namespace CriusNyx.Util;

public static class LinqExtensions
{
  public static IEnumerable<T> WhereAs<T>(this IEnumerable enumerable)
  {
    foreach (var element in enumerable)
    {
      if (element is T t)
      {
        yield return t;
      }
    }
  }

  public static T[] AsArray<T>(this T element)
  {
    return [element];
  }

  public static IEnumerable<T> ThenConcat<T>(this T element, IEnumerable<T> then)
  {
    return new T[] { element }.Concat(then);
  }

  public static IEnumerable<(T value, int index)> WithIndex<T>(this IEnumerable<T> values)
  {
    int index = 0;
    foreach (var value in values)
    {
      yield return (value, index++);
    }
  }

  public static IEnumerable<T> PadWith<T>(
    this IEnumerable<T> values,
    int length,
    T element = default!
  )
  {
    int count = values.Count();
    if (count < length)
    {
      return values.Concat(Enumerable.Repeat(element, length - count));
    }
    return values;
  }
}
