## Array Extensions

### Fill

Fills an array with the provided element, and then returns the same array.

```cs
var arr = new int[3].Fill(-1);
// arr will be [-1, -1, -1];
```

### FillFunc

Fills an array with the output of the generator function.

```cs
var original = new User();
var arr = new User[3].Fill(() => original.Clone());
// Arr will be filled with 3 clones of user.
```

### FillEmpty

Replaces all empty elements in the array with the provided element.

```cs
var source = new User[]{new User("Brad"), null, new User("Emily")};
source.FillEmpty(new User("Unknown User"));
// Output will be Brad, Unknown User, Emily.
```
