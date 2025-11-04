public interface ITree<T>
  where T : ITree<T>
{
  IEnumerable<T> Getchildren();
}

public static class TreeExtensions
{
  public static void Traverse<T>(this T node, Action<T> visitor)
    where T : ITree<T> => node.Traverse(visitor, (n) => n.Getchildren());

  public static void Traverse<T>(this T node, Action<T, int> visitor)
    where T : ITree<T> => node.Traverse(visitor, (n) => n.Getchildren());

  public static void Traverse<T>(
    this T node,
    Action<T> visitor,
    Func<T, IEnumerable<T>> getChildren
  )
  {
    foreach (var (child, _) in node.TraverseFlat(getChildren))
    {
      visitor(child);
    }
  }

  public static void Traverse<T>(
    this T node,
    Action<T, int> visitor,
    Func<T, IEnumerable<T>> getChildren
  )
  {
    foreach (var (child, depth) in node.TraverseFlat(getChildren))
    {
      visitor(child, depth);
    }
  }

  public static IEnumerable<(T node, int depth)> TraverseFlat<T>(this T node)
    where T : ITree<T> => node.TraverseFlat((n) => n.Getchildren());

  public static IEnumerable<(T node, int depth)> TraverseFlat<T>(
    this T node,
    Func<T, IEnumerable<T>> getChildren,
    int depth = 0
  )
  {
    yield return (node, depth);
    foreach (var child in getChildren(node))
    {
      foreach (var ancestor in child.TraverseFlat(getChildren, depth + 1))
      {
        yield return ancestor;
      }
    }
  }
}
