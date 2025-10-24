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
}
