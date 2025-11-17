/// <summary>
/// Extensions methods for the Equivalent interface.
/// </summary>
public static class EquivalentExtensions
{
  /// <summary>
  /// Returns true if the elements of the set are equivalent
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <typeparam name="U"></typeparam>
  /// <param name="source"></param>
  /// <param name="other"></param>
  /// <returns></returns>
  public static bool SetEquivalent<T, U>(this IEnumerable<T> source, IEnumerable<U> other)
    where T : Equivalent
  {
    if (source.Count() != other.Count())
    {
      return false;
    }
    return source.Zip(other).All((a) => a.First.Equivalent(a.Second!));
  }

  /// <summary>
  /// Returns true if two dictionaryies contain the same keys, and if each value is the same between the two.
  /// </summary>
  /// <typeparam name="Key"></typeparam>
  /// <typeparam name="Value"></typeparam>
  /// <typeparam name="OtherValue"></typeparam>
  /// <param name="source"></param>
  /// <param name="other"></param>
  /// <returns></returns>
  public static bool DictionaryEquivalent<Key, Value, OtherValue>(
    this IDictionary<Key, Value> source,
    IDictionary<Key, OtherValue> other
  )
    where Value : Equivalent
  {
    var keys = source.Keys.Concat(other.Keys).ToHashSet();
    foreach (var key in keys)
    {
      if (
        !source.TryGetValue(key, out var a)
        || !other.TryGetValue(key, out var b)
        || !a.Equivalent(b)
      )
      {
        return false;
      }
    }
    return true;
  }
}
