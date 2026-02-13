using CriusNyx.Util;

/// <summary>
/// Extensions for arrays
/// </summary>
public static class ArrayExtensions
{
  /// <summary>
  /// Fill all elements in arr with the provided value.
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="arr"></param>
  /// <param name="value"></param>
  /// <returns></returns>
  public static T[] Fill<T>(this T[] arr, T value)
  {
    arr.NotNull("arr");
    for (int i = 0; i < arr.Length; i++)
    {
      arr[i] = value;
    }
    return arr;
  }

  /// <summary>
  /// Replace all elements in arr with the output of the generator function.
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="arr"></param>
  /// <param name="generator"></param>
  /// <returns></returns>
  public static T[] FillFunc<T>(this T[] arr, Func<T> generator)
  {
    arr.NotNull("arr");
    for (int i = 0; i < arr.Length; i++)
    {
      arr[i] = generator();
    }
    return arr;
  }

  /// <summary>
  /// Fill all elements in the array that are equal to the default value for T.
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="arr"></param>
  /// <param name="value"></param>
  /// <returns></returns>
  public static T[] FillEmpty<T>(this T[] arr, T value)
  {
    arr.NotNull("arr");
    for (int i = 0; i < arr.Length; i++)
    {
      if (Equals(arr[i], default(T)))
      {
        arr[i] = value;
      }
    }
    return arr;
  }
}
