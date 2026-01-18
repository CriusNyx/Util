using System.Text;

namespace CriusNyx.Util;

/// <summary>
/// Extensions for strings.
/// </summary>
public static class StringExtensions
{
  /// <summary>
  /// Indent each line in a string by the specified indentation.
  /// </summary>
  /// <param name="source"></param>
  /// <param name="indentation"></param>
  /// <returns></returns>
  public static string Indent(this string source, string indentation)
  {
    return source.Split("\n").Select(x => $"{indentation}{x}").StringJoin("\n");
  }

  /// <summary>
  /// Join an enumerable of strings into a single string.
  /// </summary>
  /// <param name="source"></param>
  /// <param name="separator"></param>
  /// <returns></returns>
  public static string StringJoin(this IEnumerable<string> source, string separator = "")
  {
    return string.Join(separator, source);
  }

  /// <summary>
  /// Truncate the string to the specified length. Elipsize the string with the specified elipsize if provided.
  /// </summary>
  /// <param name="source"></param>
  /// <param name="Length"></param>
  /// <param name="elipsize"></param>
  /// <returns></returns>
  public static string Truncate(this string source, int Length, string elipsize = "")
  {
    elipsize = elipsize ?? "";
    if (Length < 0)
    {
      return source;
    }
    if (Length < elipsize.Length)
    {
      if (source.Length > Length)
      {
        return elipsize.Substring(0, Length);
      }

      return source;
    }
    if (source.Length > Length)
    {
      return source.Substring(0, Length - elipsize.Length) + elipsize;
    }
    return source;
  }

  /// <summary>
  /// Helper methods for printing grid.
  /// </summary>
  /// <param name="lines"></param>
  /// <param name="maxWidth"></param>
  /// <returns></returns>
  private static int[] ComputeColumnWidths(IEnumerable<IEnumerable<string>> lines, int maxWidth)
  {
    if (lines.Count() == 0)
    {
      return new int[] { };
    }
    var columnCount = lines.Max(x => x.Count());
    var columnWidths = new int[columnCount];

    void SetColumnWidth(int column, int width)
    {
      var current = columnWidths[column];
      current = Math.Max(current, width);
      if (maxWidth >= 0)
      {
        current = Math.Min(current, maxWidth);
      }
      columnWidths[column] = current;
    }

    foreach (var (line, y) in lines.WithIndex())
    {
      foreach (var (element, x) in line.WithIndex())
      {
        SetColumnWidth(x, element.Length);
      }
    }

    return columnWidths;
  }

  /// <summary>
  /// Format a 2 dimensional list of strings into a grid.
  /// The outer dimension should be lines, and the inner dimension should be columns in those lines.
  /// </summary>
  /// <param name="lines"></param>
  /// <param name="separator"></param>
  /// <param name="maxWidth"></param>
  /// <param name="elipsize"></param>
  /// <returns></returns>
  public static string FormatGrid(
    this IEnumerable<IEnumerable<string>> lines,
    string separator = "",
    int maxWidth = -1,
    bool elipsize = true
  )
  {
    var columnWidths = ComputeColumnWidths(lines, maxWidth);

    int lineCount = lines.Count();

    StringBuilder builder = new();

    foreach (var (line, lineNum) in lines.WithIndex())
    {
      foreach (var (element, column) in line.PadWith(columnWidths.Length, "").WithIndex())
      {
        var columnWidth = columnWidths[column];
        builder.Append(element.Truncate(columnWidth, elipsize ? "..." : "").PadRight(columnWidth));
        if (column < columnWidths.Length - 1)
        {
          builder.Append(separator);
        }
      }
      if (lineNum != lineCount - 1)
      {
        builder.Append("\n");
      }
    }
    return builder.ToString();
  }

  /// <summary>
  /// Return the string split up before and after the first occurrence of search.
  /// If search does not exist in the string then orDefault will be returned as the first element of result.
  /// </summary>
  /// <param name="source"></param>
  /// <param name="search"></param>
  /// <param name="orDefault">Default value if search is not found. Default to ""</param>
  /// <returns></returns>
  public static (string before, string after) Bisect(
    this string source,
    string search,
    string orDefault = ""
  )
  {
    var index = source.IndexOf(search);
    if (index >= 0)
    {
      return source.Substring(0, index).With(source.Substring(index + search.Length));
    }
    return (orDefault, source);
  }
}
