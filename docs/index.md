---
_layout: landing
---

# CriusNyxUtil

This is a set of utility functions that I find myself using frequently.

They provide a fluent api for doing a lot of common things.

### See [Introduction](./docs/introduction) or [API](./api/CriusNyx.Util.html)

## Examples

For example, say you want to assign a field to an object before passing it into 
a method.

```cs
Tree.AddChild(
  new Node().Touch(child => { child.name = "Child"; })
);
```

Or you want to validate that the result of a function is not null before 
assigning it to a field.

```cs
var a = MyClass.From(source).NotNull("a");
```

These methods are useful when creating method chains or defining objects.

Consider the source code for this language parser. The child parsers shouldn't 
return a value if they don't succeed in parsing. Therefore, left, equal, or 
right returning a null value would indicate that a bug has slipped. Using the 
NotNull method will give the developer a crash with a useful error method if
the application this bug is introduced during development, rather then letting
the construction of an invalid object happen and having it crash later.

```cs
var AssignParser =
  from left in LeftHandExpressionParser
  from equal in EqualSignParser
  from right in RightHandExpressionParser
  select new AssignExpression(
    left.NotNull("left"), 
    equal.NotNull("equal"), 
    right.NotNull("right"));
```

Or this case where you want to validate a class and throw an exception before
returning control flow to the caller.

```cs
// Null check a field on initialization.
public class MyClass(string value){
  string Value = value.NotNull("value");
}
```