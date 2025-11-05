## Linq Extensions

### Foreach

Perform an operation for each element in the collection.
Unlike select the function doesn't return.

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

Convert each element in the collection to the new type, and return all elements that can be converted.

```cs
var values = [new object(), new NumVal(0)];

var onlyNumVals = values.WhereAs<NumVal>();

// [NumVal(0)]
```

### AsArray

Convert an element to an array with that element.
Useful for working with linq.

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