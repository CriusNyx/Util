/// <summary>
/// An observable value.
/// </summary>
/// <typeparam name="Value"></typeparam>
public class Observable<Value>
{
  /// <summary>
  /// The comparison function.
  /// </summary>
  private Func<Value?, Value?, bool> compare = null!;

  /// <summary>
  /// The value of the observable.
  /// </summary>
  public Value value { get; private set; }

  /// <summary>
  /// Action to invoke when the value changes.
  /// </summary>
  public event Action<Value> OnValueChange = null!;

  /// <summary>
  /// Create an observable with the initial value and comparison function.
  /// </summary>
  /// <param name="initialValue"></param>
  /// <param name="compare"></param>
  public Observable(Value initialValue = default!, Func<Value?, Value?, bool> compare = null!)
  {
    value = initialValue;
    this.compare = compare ?? ((x, y) => Equals(x, y));
  }

  private bool CompareValues(Value self, Value other)
  {
    return compare?.Invoke(self, other) ?? Equals(self, other);
  }

  /// <summary>
  /// Sets the value.
  /// </summary>
  /// <param name="value"></param>
  public void SetValue(Value value)
  {
    if (!CompareValues(this.value, value))
    {
      this.value = value;
      OnValueChange?.Invoke(value);
    }
    else
    {
      this.value = value;
    }
  }

  /// <summary>
  /// Anytime the result of the selector changes invoke the onChange action.
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="selector"></param>
  /// <param name="onChange"></param>
  /// <param name="compare"></param>
  /// <returns></returns>
  public Action RegisterSelector<T>(
    Func<Value, T> selector,
    Action<T> onChange,
    Func<T?, T?, bool> compare = null!
  )
  {
    // Store the transformer value in the closure.
    var current = selector(value);

    // Create an action to observe the observer value.
    Action<Value> observerAction = (value) =>
    {
      var newCurrent = selector(value);
      if (!(compare?.Invoke(newCurrent, current) ?? Equals(newCurrent, current)))
      {
        current = newCurrent;
        onChange(current);
      }
    };

    // Register observer value.
    OnValueChange += observerAction;
    return () =>
    {
      OnValueChange -= observerAction;
    };
  }
}
