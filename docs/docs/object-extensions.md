## Object Extensions

### AS

Same as the As keyword. Just convenient for places where the As keyword isn't
easy to insert.

```cs
var a = new Expected();
var b = new object();

var aAs = a.As<Expected>();
// Expected()
var bAs = b.As<Expected>();
// null
```

### AsNotNull

Same as As but will throw on fail.

```cs
var a = new Expected();
var b = new object();

var aAs = a.AsNotNull<Expected>();
// Expected()
var bAs = b.AsNotNull<Expected>();
// throws
```

### NotNull

Throw an exception if the value is null. Return that value otherwise.

```cs
public class MyClass(string source){
  // Will throw if developer passes a null value to constructor.
  public string Source { get; } = source.NotNull("source");
}
```

### With

Create a tuple with the element.

```cs
var result = "Hello".With("World");
// ("Hello", "World")
```

### AndWith

Extend a tuple with additional elements

```cs
var result = "A".With("B").AndWith("C");
// ("A", "B", "C")
```

### Expand

Expand a tuple by applying the expander function to the last element.

```cs
var (first, second, third) = "Foo: Bar, Baz".Bisect(": ").Expand(rest => rest.Bisect(", "));
// ("Foo", "Bar", "Baz")
```

### Touch

Perform the action on the object and then return the same object.

```cs
var val = new Node().Touch(node => node.name = "Val");
// Node { name = "Val" };
```

### Safe

Dereference an array, list or dictionary or return the default value if it can't
be dereferenced.

```cs
string[] elements = ["A", "B"];

var a = elements.Safe(0);
// "A"
var b = elements.Safe(1);
// "B"
var c = elements.Safe(2);
// null
```

```cs
Dictionary<string, object> dict = new Dictionary<string, object>();
dict.Add("Hello", new object());

var hello = dict.Safe("Hello");
// object
var world = dict.Safe("World");
// null
```

You can also use safe with a function. If the function throws an exception the
default value will be returned.

```cs
string[] arr = ["Hello"];
var a = arr.Safe((x) => x[0]);
// "Hello"
var b = arr.Safe((x) => x[1]);
// null
```

### SafePeek

Peek the next element of a queue or stack. If there is no element return the
default instead.

```cs
var queue = new Queue<string>(["Hello"]);
var a = queue.SafePeek();
// "Hello"
queue.Dequeue();
var b = queue.SafePeek();
// null
```

### Transform

Transform a value to a different form.

```cs
Dictionary<string, object> dict = new Dictionary<string, object>();
dict.Add("Hello", "Foo");

var hello = dict.Safe("Hello")?.Transform(x => x.Safe(0));
// F
var world = dict.Safe("World")?.Transform(x => x.Safe(0));
// null
```
