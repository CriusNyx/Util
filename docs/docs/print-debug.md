## Print Debug

A helper method for printing RON like serializations of object.

Does not fully conform to the RON standard.

I developed this rather then using RON.NET because RON.NET was less dynamic then I wanted.

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