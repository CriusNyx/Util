namespace CriusNyx.Util;

/// <summary>
/// Apply this interface to a class to allow it to auto implement Traverse and other tree extensions.
/// </summary>
/// <typeparam name="T"></typeparam>
public interface ITree<T>
  where T : ITree<T>
{
  /// <summary>
  /// Get the children of this node in the tree.
  /// </summary>
  /// <returns></returns>
  IEnumerable<T> Getchildren();
}

/// <summary>
/// Extensions to work with trees.
/// </summary>
public static class TreeExtensions
{
  /// <summary>
  /// Visit each node in the tree in breadth first order.
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="node"></param>
  /// <param name="visitor"></param>
  public static void Traverse<T>(this T node, Action<T> visitor)
    where T : ITree<T> => node.Traverse(visitor, (n) => n.Getchildren());

  /// <summary>
  /// Visit each node, with it's depth, in the tree in a breadth first order.
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="node"></param>
  /// <param name="visitor"></param>
  public static void Traverse<T>(this T node, Action<T, int> visitor)
    where T : ITree<T> => node.Traverse(visitor, (n) => n.Getchildren());

  /// <summary>
  /// Visit each node in a tree using the getChildren function to find children.
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="node"></param>
  /// <param name="visitor"></param>
  /// <param name="getChildren"></param>
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

  /// <summary>
  /// Visit each node, with it's depth, in a tree using the getChildren function to find children.
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="node"></param>
  /// <param name="visitor"></param>
  /// <param name="getChildren"></param>
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

  /// <summary>
  /// Convert tree to a list of nodes in Bredth First Order.
  /// Returns a tuples of nodes with their depth.
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="node"></param>
  /// <returns></returns>
  public static IEnumerable<(T node, int depth)> TraverseFlat<T>(this T node)
    where T : ITree<T> => node.TraverseFlat((n) => n.Getchildren());

  /// <summary>
  /// Convert tree to a list of nodes in Breadth First Order using getChildren to find the nodes children.
  /// Returns a tuples of nodes with their depth.
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="node"></param>
  /// <param name="getChildren"></param>
  /// <param name="depth"></param>
  /// <returns></returns>
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
