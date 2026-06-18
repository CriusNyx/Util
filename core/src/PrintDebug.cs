// RON.NET wasn't really meeting my needs. If a new RON serializer for .NET is created that is more dynamic I should switch to that.

using System.Collections;
using System.Reflection;

namespace CriusNyx.Util;

/// <summary>
/// Enable debug printing for fields that have the DebugPrint field attribute.
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public class DebugPrintAttribute : Attribute { }

/// <summary>
/// Enable debug printing for field.
/// </summary>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public class DebugFieldAttribute : Attribute { }

/// <summary>
/// Apply this interface to an object to added .Debug support to that object.
/// Otherwise .ToString will be used instead.
/// </summary>
public interface DebugPrint
{
  /// <summary>
  /// Should return a list of field names and their values so that they can be included in the .Debug output.
  /// </summary>
  /// <returns></returns>
  IEnumerable<(string, object)> EnumerateFields();

  internal delegate IEnumerable<(string, object)> CustomDebugPrint_Impl(object o);

  /// <summary>
  /// Custom debug print implementation
  /// </summary>
  /// <param name="o"></param>
  /// <returns></returns>
  public delegate IEnumerable<(string, object)> CustomDebugPrint<T>(T o);

  internal static Dictionary<Type, CustomDebugPrint_Impl> customPrinters =
    new Dictionary<Type, CustomDebugPrint_Impl>();

  /// <summary>
  /// Register custom enumeration method for Debug Print.
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="enumerateField"></param>
  public static void RegisterCustomType<T>(CustomDebugPrint<T> enumerateField)
  {
    customPrinters.Add(typeof(T), (object o) => enumerateField((T)o));
  }

  internal static bool TryGetCustomFormatter(Type type, out CustomDebugPrint_Impl impl)
  {
    if (customPrinters.TryGetValue(type, out impl!))
    {
      return true;
    }
    if (type.BaseType != null)
    {
      return TryGetCustomFormatter(type.BaseType, out impl);
    }

    return false;
  }

  /// <summary>
  /// Enumerate the fields of a type using reflection.
  /// </summary>
  /// <param name="source"></param>
  /// <returns></returns>
  public static IEnumerable<(string, object)> EnumerateWithReflection(object source)
  {
    return EnumerateWithReflection(source, source.GetType());
  }

  /// <summary>
  /// Enumerate the fields of a type using reflection.
  /// </summary>
  /// <param name="source"></param>
  /// <param name="type"></param>
  /// <returns></returns>
  public static IEnumerable<(string, object)> EnumerateWithReflection(object source, Type type)
  {
    return type.GetFields()
      .Select(x => x.Name.With(x.GetValue(source)))
      .Concat(type.GetProperties().Select(x => x.Name.With(x.GetValue(source))))!;
  }

  /// <summary>
  /// Enumerate fields that have the DebugFieldAttribute
  /// </summary>
  /// <param name="source"></param>
  /// <param name="type"></param>
  /// <returns></returns>
  public static IEnumerable<(string, object)> EnumerateWithAttributes(object source, Type type)
  {
    var fields = type.GetFields((BindingFlags)(-1))
      .Where(x => x.GetCustomAttribute<DebugFieldAttribute>(true) is not null)
      .Select(x => x.Name.With(x.GetValue(source)));
    var props = type.GetProperties((BindingFlags)(-1))
      .Where(x => x.GetCustomAttribute<DebugFieldAttribute>(true) is not null)
      .Select(x => x.Name.With(x.GetValue(source)));
    return fields.Concat(props)!;
  }

  /// <summary>
  /// Enumerate fields that have the DebugFieldAttribute
  /// </summary>
  /// <param name="source"></param>
  /// <returns></returns>
  public static IEnumerable<(string, object)> EnumerateWithAttributes(object source)
  {
    var type = source.GetType();
    var fields = type.GetFields()
      .Where(x => x.GetCustomAttribute<DebugFieldAttribute>(true) is not null)
      .Select(x => x.Name.With(x.GetValue(source)));
    var props = type.GetProperties()
      .Where(x => x.GetCustomAttribute<DebugFieldAttribute>(true) is not null)
      .Select(x => x.Name.With(x.GetValue(source)));
    return fields.Concat(props)!;
  }
}

/// <summary>
/// Serialize the object to a Debug object similar to rust object notation (RON).
/// </summary>
public static class DebugPrintExtensions
{
  /// <summary>
  /// Prints the body for the Enumerable
  /// </summary>
  /// <param name="body"></param>
  /// <returns></returns>
  private static string PrintBody(IEnumerable<(string, object)> body)
  {
    return body.Select(PrintField).StringJoin(",\n");
  }

  private static string PrintObject(string objectName, IEnumerable<(string, object)> fields)
  {
    return $"{objectName} {{\n{PrintBody(fields).Indent("  ")}\n}}";
  }

  /// <summary>
  /// Serialize the object in a RON like notation and return that string.
  /// </summary>
  /// <param name="o"></param>
  /// <param name="name"></param>
  /// <returns></returns>
  public static string Debug(this object o, string name)
  {
    return $"{name} = {o.Debug()}";
  }

  /// <summary>
  /// Serialize the object in a RON like notation and return that string.
  /// </summary>
  /// <param name="o"></param>
  /// <returns></returns>
  public static string Debug(this object o)
  {
    if (o != null && DebugPrint.TryGetCustomFormatter(o.GetType(), out var customPrinter))
    {
      return PrintObject(o.GetType().Name, customPrinter(o));
    }
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
      return $"[\n{enumerable.Cast<object>().Select((value) => Debug(value)).StringJoin(",\n").Indent("  ")}\n]";
    }
    else if (o is DebugPrint debug)
    {
      return PrintObject(o.GetType().Name, debug.EnumerateFields());
    }
    else if (o is null)
    {
      return "null";
    }
    else if (o.GetType() is Type t && t.GetCustomAttribute<DebugPrintAttribute>() is not null)
    {
      return PrintObject(t.Name, DebugPrint.EnumerateWithAttributes(o, t));
    }
    return o?.ToString() ?? "";
  }

  /// <summary>
  /// Print a RON field.
  /// </summary>
  /// <param name="field"></param>
  /// <returns></returns>
  private static string PrintField((string, object) field)
  {
    var (name, value) = field;
    return $"{name}: {Debug(value)}";
  }
}
