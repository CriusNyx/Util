using System.Text;

namespace CriusNyx.Util;

public static class StringExtensions
{
  public static string Indent(this string source, string indentation)
  {
    return source.Split("\n").Select(x => $"{indentation}{x}").StringJoin("\n");
  }

  public static string StringJoin(this IEnumerable<string> source, string separator = "")
  {
    return string.Join(separator, source);
  }

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
}
