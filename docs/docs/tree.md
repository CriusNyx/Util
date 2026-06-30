## Tree Extensions

### Traverse

Visits each node in a try and applies the function.

```cs
// Will visit each node in the tree recursively and print it
var tree = new Tree("A", new Tree("B"), new Tree("C"));
tree.Traverse(node => Console.WriteLine(node.Name));

// A
// B
// C
```

### TraverseFlat

Will create an enumerable of each node in the tree by flattening it.

```cs
var tree = new Tree("A", new Tree("B"), new Tree("C"));

foreach(var (node, depth) in tree.TraverseFlat((node) => node.GetChildren())){
  Console.WriteLine($"{Indent(depth)}{node.Name}");
}

// A
// |-B
// |-C
```

### PrintTree

Print the object as a tree.

```cs
var tree = new Tree("A", new Tree("B"), new Tree("C");
Console.WriteLine(tree.PrintTree(node => node.GetChildren(), (node) => node.Value));
// Will print
// A
// |-B
// |-C
```
