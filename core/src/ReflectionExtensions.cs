using System.Reflection;

namespace CriusNyx.Util.Reflection;

/// <summary>
/// Extension methods for reflection.
/// </summary>
public static class ReflectionUtil
{
  /// <summary>
  /// Get the value of the named field on source.
  /// </summary>
  public static object ReflectValue(this object source, string fieldName)
  {
    var members = source.GetType().GetMember(fieldName);
    foreach (var member in members)
    {
      if (member is FieldInfo field)
      {
        return field.GetValue(source)!;
      }
      if (member is PropertyInfo property)
      {
        return property.GetValue(source)!;
      }
    }
    return default!;
  }
}
