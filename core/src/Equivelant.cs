/// <summary>
/// Implemented the Equivalent method that determines that two elements are equivalent but not nessessarily equal.
/// </summary>
public interface Equivalent
{
  /// <summary>
  /// Returns true if the two objects are equivalent.
  /// </summary>
  /// <param name="other"></param>
  /// <returns></returns>
  public bool Equivalent(object other);
}
