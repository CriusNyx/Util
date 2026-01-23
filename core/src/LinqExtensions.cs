using System.Collections;
using Microsoft.VisualBasic;

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
  /// Create a new array with the other elements appended on the end.
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="source"></param>
  /// <param name="other"></param>
  /// <returns></returns>
  public static T[] Expand<T>(this T[] source, IEnumerable<T> other)
  {
    return source.Concat(other).ToArray();
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
  /// Zip the lists together, zipping only the elements that exist in both lists.
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <typeparam name="U"></typeparam>
  /// <param name="left"></param>
  /// <param name="right"></param>
  /// <returns></returns>
  public static IEnumerable<(T left, U right)> InnerZip<T, U>(
    this IEnumerable<T> left,
    IEnumerable<U> right
  )
  {
    int len = Math.Min(left.Count(), right.Count());
    return left.Take(len).Zip(right.Take(len));
  }

  /// <summary>
  /// Zips the lists together, padding with the provided defaults if one list is longer then the other.
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <typeparam name="U"></typeparam>
  /// <param name="left"></param>
  /// <param name="right"></param>
  /// <param name="defaultLeft"></param>
  /// <param name="defaultRight"></param>
  /// <returns></returns>
  public static IEnumerable<(T left, U right)> OuterZip<T, U>(
    this IEnumerable<T> left,
    IEnumerable<U> right,
    T defaultLeft = default!,
    U defaultRight = default!
  )
  {
    int len = Math.Max(left.Count(), right.Count());
    return left.PadWith(len, defaultLeft).Zip(right.PadWith(len, defaultRight));
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

  /// <summary>
  /// Take 2 elements from the array of type T and U.
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <typeparam name="U"></typeparam>
  /// <param name="source"></param>
  /// <returns></returns>
  public static (T?, U?) Take<T, U>(this IEnumerable source)
  {
    var enumerator = source.GetEnumerator();
    return (enumerator.Consume()!.As<T>(), enumerator.Consume()!.As<U>());
  }

  /// <summary>
  /// Take 3 elements form the array of type T, U and V.
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <typeparam name="U"></typeparam>
  /// <typeparam name="V"></typeparam>
  /// <param name="source"></param>
  /// <returns></returns>
  public static (T?, U?, V?) Take<T, U, V>(this IEnumerable source)
  {
    var enumerator = source.GetEnumerator();
    return (
      enumerator.Consume()!.As<T>(),
      enumerator.Consume()!.As<U>(),
      enumerator.Consume()!.As<V>()
    );
  }

  /// <summary>
  /// Take 3 elements form the array of type T, U, V and W.
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <typeparam name="U"></typeparam>
  /// <typeparam name="V"></typeparam>
  /// <typeparam name="W"></typeparam>
  /// <param name="source"></param>
  /// <returns></returns>
  public static (T?, U?, V?, W?) Take<T, U, V, W>(this IEnumerable source)
  {
    var enumerator = source.GetEnumerator();
    return (
      enumerator.Consume()!.As<T>(),
      enumerator.Consume()!.As<U>(),
      enumerator.Consume()!.As<V>(),
      enumerator.Consume()!.As<W>()
    );
  }

  /// <summary>
  /// Take 5 elements of type T, U, V, W and X
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <typeparam name="U"></typeparam>
  /// <typeparam name="V"></typeparam>
  /// <typeparam name="W"></typeparam>
  /// <typeparam name="X"></typeparam>
  /// <param name="source"></param>
  /// <returns></returns>
  public static (T?, U?, V?, W?, X?) Take<T, U, V, W, X>(this IEnumerable source)
  {
    var enumerator = source.GetEnumerator();
    return (
      enumerator.Consume()!.As<T>(),
      enumerator.Consume()!.As<U>(),
      enumerator.Consume()!.As<V>(),
      enumerator.Consume()!.As<W>(),
      enumerator.Consume()!.As<X>()
    );
  }

  /// <summary>
  /// Take 6 elements of type T, U, V, W, X and Y
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <typeparam name="U"></typeparam>
  /// <typeparam name="V"></typeparam>
  /// <typeparam name="W"></typeparam>
  /// <typeparam name="X"></typeparam>
  /// <typeparam name="Y"></typeparam>
  /// <param name="source"></param>
  /// <returns></returns>
  public static (T?, U?, V?, W?, X?, Y?) Take<T, U, V, W, X, Y>(this IEnumerable source)
  {
    var enumerator = source.GetEnumerator();
    return (
      enumerator.Consume()!.As<T>(),
      enumerator.Consume()!.As<U>(),
      enumerator.Consume()!.As<V>(),
      enumerator.Consume()!.As<W>(),
      enumerator.Consume()!.As<X>(),
      enumerator.Consume()!.As<Y>()
    );
  }

  /// <summary>
  /// Consume a single element from the enumerator.
  /// </summary>
  /// <param name="source"></param>
  /// <returns></returns>
  public static object? Consume(this IEnumerator source)
  {
    if (source.MoveNext())
    {
      return source.Current;
    }
    return default!;
  }

  /// <summary>
  /// Consume an element from the source or return the default value.
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="source"></param>
  /// <param name="defaultValue"></param>
  /// <returns></returns>
  public static T Consume<T>(this IEnumerator source, T defaultValue = default!)
  {
    if (source.MoveNext() && source.Current is T t)
    {
      return t;
    }
    return defaultValue!;
  }

  /// <summary>
  /// Try to consume the next element from the enumerator.
  /// </summary>
  /// <param name="source"></param>
  /// <param name="value"></param>
  /// <returns></returns>
  public static bool TryConsume(this IEnumerator source, out object value)
  {
    if (source.MoveNext())
    {
      value = source.Current;
      return true;
    }
    value = default!;
    return false;
  }
}
