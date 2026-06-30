## Reflection Extensions

### ReflectValue

If the object contains a field with the specified name return the value of that
field.

```cs
var foo = new Foo("baz");
var val = foo.ReflectValue("bar");
// val will be "baz"

class Foo{
  public string bar;
}
```
