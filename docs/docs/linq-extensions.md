## Linq Extensions

### Foreach

Perform an operation for each element in the collection. Unlike select the
function doesn't return.

```cs
var elements = [new Value("Hello"), new Value(null)];

elements.WithIndex().Foreach((pair) => {
    if(pair.value.inner == null){ 
      Console.WriteError($"Value is null on element {pair.index}"); 
    }
  }
)

// Value is null on element 1
```

### WhereAs

Convert each element in the collection to the new type, and return all elements
that can be converted.

```cs
var values = [new object(), new NumVal(0)];

var onlyNumVals = values.WhereAs<NumVal>();

// [NumVal(0)]
```

### AsArray

Convert an element to an array with that element. Useful for working with linq.

```cs
var element = "Hello World!";
var result = element.AsArray();

// ["Hello World!"]
```

### ThenConcat

Create an enumerable with the element, and then concatinate more.

```cs
var element = "Hello";
var rest = ["World", "And"];

var result = element.ThenConcat(rest);

// IEnumerable{ "Hello", "World", "And" }
```

### Expand

Creates a new array expanded with the new contents.

```cs
var arr = ["Foo", "Bar"];
var result = arr.Expand(["Baz"]);
// ["Foo", "Bar", "Baz"];
```

### WithIndex

Return a new enumerable with the elements and their index

```cs
var source = ["Hello", "World"];
var dict = new Dictionary<string, object>();

foreach(var (element, index) in source.WithIndex()){
  dict.Add(index, element);
}

// {
//   0: "Hello",
//   1: "World"
// }
```

### PadWith

Pad the array to a certain size by appending the specified element.

```cs
var source = [1, 2, 3];
var result = source.PadWith(5, -1).ToArray();
// result will be [1, 2, 3, -1, -1];
```

### InnerZip

Zips the elements matching the length of the shorter element.

```cs
string[] names = ["Sam", "Heartman"];
int[] values = [3];
var zipped = names.InnerZip(values);
// Will be [("Sam", 3)];
```

### OuterZip

Zips the elements matching the length of the outer element.

```cs
string[] names = ["Sam", "Heartman"];
int[] values = [3];
var zipped = names.InnerZip(values);
// Will be [("Sam", 3), ("Heartman", 0)];
```

### OuterZip

Zips the elements matching the length of the longer element.

### Take

Take up to 6 elements from a enumerable.

```cs
object source = [1, "hello", 3];
var (integer, str) = source.Take<int, string>();
// Will be 1 and "hello"
```

### Consume

Given an IEnumerator consume the next element and advance the enumerator

```cs
object[] source = [1, "hello", 3];
var enumerator = source.GetEnumerator();
var a = enumerator.Consume();
var b = enumerator.Consume();
var c = enumerator.Consume();
// Will be 1, "hello" and 3 respectively.
```

You can also provide a default value

```cs
object[] source = [1];
var enumerator = source.GetEnumerator();
var a = enumerator.Consume<string>("empty");
// a will be "empty"
```

### TryConsume

Same as consume but following C#'s try style.

```cs
object[] source = [1, "hello", 3];
if(source.TryConsume(out var a)){
  // a will be 1.
}
```
