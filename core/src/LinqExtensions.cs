using System.Collections;

namespace CriusNyx.Util;

/// <summary>
/// Extensions for sets and lists.
/// </summary>
public static class LinqExtensions
{
  /// <summary>
  /// Itterate over each element in the enumerable and preform the specified action on it.
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="enumerable"></param>
  /// <param name="action"></param>
  public static void Foreach<T>(this IEnumerable<T> enumerable, Action<T> action)
  {
    foreach (var element in enumerable)
    {
      action(element);
    }
  }

  /// <summary>
  /// Convert each element to the type T and return a new enumerable with elements that could be converted to T.
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="enumerable"></param>
  /// <returns></returns>
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

  /// <summary>
  /// Convert a single element into an array with that element.
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="element"></param>
  /// <returns></returns>
  public static T[] AsArray<T>(this T element)
  {
    return [element];
  }

  /// <summary>
  /// Convert the element to an IEnumerable of the same type and concatinate the specified enumerable on that element.
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="element"></param>
  /// <param name="then"></param>
  /// <returns></returns>
  public static IEnumerable<T> ThenConcat<T>(this T element, IEnumerable<T> then)
  {
    return new T[] { element }.Concat(then);
  }

  /// <summary>
  /// Append indexes to the enumerable and return a new enumerable with those elements.
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="values"></param>
  /// <returns></returns>
  public static IEnumerable<(T value, int index)> WithIndex<T>(this IEnumerable<T> values)
  {
    int index = 0;
    foreach (var value in values)
    {
      yield return (value, index++);
    }
  }

  /// <summary>
  /// Pad the enumerable to a specified length with the specified element, or the default(T).
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="values"></param>
  /// <param name="length"></param>
  /// <param name="element"></param>
  /// <returns></returns>
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

  /// <summary>
  /// Get or set the specified key in the dictionary.
  /// </summary>
  /// <typeparam name="Key"></typeparam>
  /// <typeparam name="Value"></typeparam>
  /// <param name="dictionary"></param>
  /// <param name="key"></param>
  /// <param name="create"></param>
  /// <returns></returns>
  public static Value GetOrSet<Key, Value>(
    this Dictionary<Key, Value> dictionary,
    Key key,
    Func<Value> create
  )
    where Key : notnull
  {
    if (dictionary.TryGetValue(key, out var value))
    {
      return value;
    }
    else
    {
      var output = create();
      dictionary[key] = output;
      return output;
    }
  }
}
