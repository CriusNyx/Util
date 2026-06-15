namespace CriusNyx.Util;

/// <summary>
/// Result of a set sync operation
/// </summary>
/// <typeparam name="Element"></typeparam>
/// <param name="added"></param>
/// <param name="removed"></param>
/// <param name="unchanged"></param>
public class SyncResult<Element>(
  IEnumerable<Element> added,
  IEnumerable<Element> removed,
  IEnumerable<Element> unchanged
)
{
  /// <summary>
  /// Elements added during sync
  /// </summary>
  public readonly IEnumerable<Element> added = added;

  /// <summary>
  /// Elements removed during sync
  /// </summary>
  public readonly IEnumerable<Element> removed = removed;

  /// <summary>
  /// Elements unchanged during sync
  /// </summary>
  public readonly IEnumerable<Element> unchanged = unchanged;
}

/// <summary>
/// Extensions for ISet
/// </summary>
public static class SetExtensions
{
  /// <summary>
  /// Synchronize the hash set with the other set.
  /// Return a list of changes.
  /// This will mutate the source set.
  /// </summary>
  /// <typeparam name="Set"></typeparam>
  /// <typeparam name="Element"></typeparam>
  /// <param name="set"></param>
  /// <param name="other"></param>
  /// <returns></returns>
  public static SyncResult<Element> Sync<Element>(
    this ISet<Element> set,
    IEnumerable<Element> other
  )
  {
    List<Element> added = new List<Element>();
    List<Element> unchanged = new List<Element>();
    List<Element> removed = new List<Element>();

    var otherSet = new HashSet<Element>(other);
    var combinedSet = new HashSet<Element>(set.Concat(other));

    foreach (var element in combinedSet)
    {
      var prev = set.Contains(element);
      var curr = otherSet.Contains(element);
      if (prev && curr)
      {
        unchanged.Add(element);
      }
      if (!prev && curr)
      {
        added.Add(element);
      }
      if (prev && !curr)
      {
        removed.Add(element);
      }
    }

    foreach (var add in added)
    {
      set.Add(add);
    }
    foreach (var rem in removed)
    {
      set.Remove(rem);
    }

    return new(added, removed, unchanged);
  }
}
