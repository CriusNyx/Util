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

You can implement the `PrintDebug` interface to customize how `Debug` works for
a particular type. It includes a single method `EnumerateFields` which expects
you to return a tuple of field names with their values. The `Debug` method will
recursively print every value which implements `PrintDebug` and every element in
a collection or dictionary.
