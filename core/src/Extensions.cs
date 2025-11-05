namespace CriusNyx.Util;

/// <summary>
/// Extensions for manipulating ordinary objects.
/// </summary>
public static class Extensions
{
  /// <summary>
  /// Cast the element to the specified type T or return the default T otherwise.
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="value"></param>
  /// <returns></returns>
  public static T? As<T>(this object value)
  {
    if (value is T t)
    {
      return t;
    }
    return default;
  }

  /// <summary>
  /// Convert value to T and assert that it's not null. Throws an exception otherwise.
  /// Lets you generate a null reference stack trace at the point of assignment rather then when dereferenced.
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="value"></param>
  /// <param name="fieldName"></param>
  /// <returns></returns>
  public static T AsNotNull<T>(this object value, string? fieldName = null)
  {
    return value.As<T>().NotNull(fieldName);
  }

  /// <summary>
  /// Assert that the object is not null, and throw an exception if it is.
  /// Lets you generate a null reference stack trace at the point of assignment rather then when dereferenced.
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="value"></param>
  /// <param name="fieldName"></param>
  /// <returns></returns>
  /// <exception cref="NullReferenceException"></exception>
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

  /// <summary>
  /// Create a tuple with the this element and the other element.
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <typeparam name="U"></typeparam>
  /// <param name="value"></param>
  /// <param name="other"></param>
  /// <returns></returns>
  public static (T, U) With<T, U>(this T value, U other)
  {
    return (value, other);
  }

  /// <summary>
  /// Extend a tuple with an additional element.
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <typeparam name="U"></typeparam>
  /// <typeparam name="V"></typeparam>
  /// <param name="value"></param>
  /// <param name="other"></param>
  /// <returns></returns>
  public static (T, U, V) AndWith<T, U, V>(this (T, U) value, V other)
  {
    return (value.Item1, value.Item2, other);
  }

  /// <summary>
  /// Extend a tuple with an additional element.
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <typeparam name="U"></typeparam>
  /// <typeparam name="V"></typeparam>
  /// <typeparam name="W"></typeparam>
  /// <param name="value"></param>
  /// <param name="other"></param>
  /// <returns></returns>
  public static (T, U, V, W) AndWith<T, U, V, W>(this (T, U, V) value, W other)
  {
    return (value.Item1, value.Item2, value.Item3, other);
  }

  /// <summary>
  /// Perform some action with the this object and then return that same object.
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="element"></param>
  /// <param name="action"></param>
  /// <returns></returns>
  public static T Touch<T>(this T element, Action<T> action)
  {
    action(element);
    return element;
  }

  /// <summary>
  /// Derefernce an element from the array or return the default value for that type if the index is outside the array.
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="arr"></param>
  /// <param name="index"></param>
  /// <returns></returns>
  public static T? Safe<T>(this T[] arr, int index)
  {
    if (index >= 0 && index < arr.Length)
    {
      return arr[index];
    }
    return default;
  }

  /// <summary>
  /// Dereference an element from the list or return the default value for the type if the index is outside the list.
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="list"></param>
  /// <param name="index"></param>
  /// <returns></returns>
  public static T? Safe<T>(this IList<T> list, int index)
  {
    if (index >= 0 && index < list.Count)
    {
      return list[index];
    }
    return default;
  }

  /// <summary>
  /// Dereference an element form the dictionary or return the default value for the type if the key is not in the dictionary.
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <typeparam name="U"></typeparam>
  /// <param name="dict"></param>
  /// <param name="key"></param>
  /// <returns></returns>
  public static U? Safe<T, U>(this IDictionary<T, U> dict, T key)
  {
    if (dict.TryGetValue(key, out var value))
    {
      return value;
    }
    return default;
  }

  /// <summary>
  /// Try to execution the function and return the value. Return the default value otherwise.
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <typeparam name="U"></typeparam>
  /// <param name="value"></param>
  /// <param name="getter"></param>
  /// <returns></returns>
  public static U? Safe<T, U>(this T value, Func<T, U> getter)
  {
    try
    {
      return getter(value);
    }
    catch
    {
      return default;
    }
  }

  /// <summary>
  /// Convert the element to a new element.
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <typeparam name="U"></typeparam>
  /// <param name="value"></param>
  /// <param name="transformation"></param>
  /// <returns></returns>
  public static U Transform<T, U>(this T value, Func<T, U> transformation)
  {
    return transformation(value);
  }
}
