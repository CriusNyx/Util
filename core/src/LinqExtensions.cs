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
}
