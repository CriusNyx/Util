using System.Text;

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
  IEnumerable<T> GetChildren();
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
    where T : ITree<T> => node.Traverse(visitor, (n) => n.GetChildren());

  /// <summary>
  /// Visit each node, with it's depth, in the tree in a breadth first order.
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="node"></param>
  /// <param name="visitor"></param>
  public static void Traverse<T>(this T node, Action<T, int> visitor)
    where T : ITree<T> => node.Traverse(visitor, (n) => n.GetChildren());

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
  /// Convert tree to a list of nodes in breadth first order.
  /// Returns a tuples of nodes with their depth.
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="node"></param>
  /// <returns></returns>
  public static IEnumerable<(T node, int depth)> TraverseFlat<T>(this T node)
    where T : ITree<T> => node.TraverseFlat((n) => n.GetChildren());

  /// <summary>
  /// Convert tree to a list of nodes in breadth first order using getChildren to find the nodes children.
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

  /// <summary>
  /// Print all the elements in a tree.
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="tree"></param>
  /// <param name="printer"></param>
  /// <param name="lineLength"></param>
  /// <returns></returns>
  public static string PrintTree<T>(
    this T tree,
    Func<T, string> printer = null!,
    int lineLength = -1
  )
    where T : ITree<T>
  {
    return tree.PrintTree<T>((x) => x.GetChildren(), printer, lineLength);
  }

  /// <summary>
  /// Print all the elements in a tree.
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="element"></param>
  /// <param name="getChildren"></param>
  /// <param name="printer"></param>
  /// <param name="lineLength"></param>
  /// <returns></returns>
  public static string PrintTree<T>(
    this T element,
    Func<T, IEnumerable<T>> getChildren,
    Func<T, string> printer = null!,
    int lineLength = -1
  )
  {
    printer = printer ?? ((x) => x?.ToString()!);
    StringBuilder builder = new StringBuilder();

    string IndentString(int value)
    {
      if (value == 0)
      {
        return "";
      }
      else if (value == 1)
      {
        return "|-";
      }
      else
      {
        return Enumerable.Range(0, value - 1).Select(x => "| ").StringJoin() + "|-";
      }
    }

    foreach (var (node, depth) in element.TraverseFlat(getChildren))
    {
      builder.AppendLine(IndentString(depth) + printer(node).Truncate(lineLength));
    }

    return builder.ToString().TrimEnd();
  }
}
