## Print Debug

A helper method for printing RON like serializations of object.

For example the following code will write the following to the console.

```cs
MyClass element = new MyClass{
  a = "Hello",
  b = 10
};

Console.WriteLine(element.Debug());

// MyClass{
//   a: "Hello",
//   b: 10
// }

public class MyClass: DebugPrint{
  public string a;
  public int b;
  
  IEnumerable<(string, object)> EnumerateFields(){
    return [nameof(a).With(a), nameof(b).With(b)];
  }
}
```

## Custom Printers

You can implement the `PrintDebug` interface to customize how `Debug` works for
a particular type. It includes a single method `EnumerateFields` which expects
you to return a list of tuples of field names with their values. The `Debug`
method will recursively print every value which implements `PrintDebug` and
every element in a collection or dictionary.

For a type you don't own you can register a custom `DebugPrinter` using
`DebugPrinter.RegisterCustomType`.

```cs
class ObjectForCustomPrinter
{
  public required string Key;
  public required string Value;
}

DebugPrint.RegisterCustomType<ObjectForCustomPrinter>(
  (value) => [nameof(value.Key).With(value.Key), nameof(value.Value).With(value.Value)]
);

// Will print this object using the custom printer.
var o = new ObjectForCustomPrinter { Key = "key", Value = "value" };
```

## Using annotations

You can use the `DebugPrint` and `DebugField` annotations to automatically
generate the debug print implementation using reflection.

```cs
MyClass element = new MyClass{
  a = "Hello",
  b = 10
};

Console.WriteLine(element.Debug());

// MyClass{
//   a: "Hello",
//   b: 10
// }

[DebugPrint]
public class MyClass{
  [DebugField]
  public string a;
  [DebugField]
  public int b;
}
```
