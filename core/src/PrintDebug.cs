using System.Collections;

namespace CriusNyx.Util;

public interface DebugPrint
{
  IEnumerable<(string, object)> EnumerateFields();
}

public static class DebugPrintExtensions
{
  private static string PrintBody(IEnumerable<(string, object)> body)
  {
    return body.Select(PrintField).StringJoin(",\n");
  }

  public static string Debug(this object o)
  {
    if (o is string str)
    {
      return $"\"{str}\"";
    }
    else if (o is IDictionary dictionary)
    {
      List<(string, object)> values = new List<(string, object)>();
      foreach (var key in dictionary.Keys)
      {
        var stringKey = key?.ToString() ?? "null";
        var value = dictionary[key!];
        values.Add((stringKey, value)!);
      }
      return $"{{\n{PrintBody(values).Indent("  ")}\n}}";
    }
    else if (o is IEnumerable enumerable)
    {
      return $"[\n{enumerable.Cast<object>().Select(Debug).StringJoin(",\n").Indent("  ")}\n]";
    }
    else if (o is DebugPrint debug)
    {
      return $"{o.GetType().Name} {{\n{PrintBody(debug.EnumerateFields()).Indent("  ")}\n}}";
    }
    else if (o is null)
    {
      return "null";
    }
    return o?.ToString() ?? "";
  }

  private static string PrintField((string, object) field)
  {
    var (name, value) = field;
    return $"{name}: {Debug(value)}";
  }
}
