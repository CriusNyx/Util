## String Extensions

### Indent

Will apply the indentation to the source string.

```cs
var source = "Hello\n  World!"
var result = source.Indent("  ");
//   Hello
//     World!
```

### StringJoin

Will join an enumerable together into a single string.

```cs
var lines = ["Hello", "World"];
var result = lines.StringJoin("\n");
// Hello
// World
```

### Truncate

Will shorten a string to the desired lenght.

```cs
var original = "Hello World!"
var truncated = original.Truncate(10, "...");
// Hello W...
```

### Format Grid

Given a 2 dimensional string enumerable will create a grid with the input.

```cs
string[][] input = [
  ["A", "B"],
  ["C", "D"]
];

var output = input.FromatGrid(" ");
// A B
// C D
```

### Bisect

Given a string an a separator bisect the string into the parts before and after.

```cs
var (a, b) = "a, b".Bisect(", ");
// ("a", "b")
```
