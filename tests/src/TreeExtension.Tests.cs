namespace CriusNyx.Util.Tests;

public class TreeHelperTests
{
  class Tree(string name, params Tree[] children) : ITree<Tree>
  {
    public string Name => name;
    public IEnumerable<Tree> Children => children;

    public Tree(string name)
      : this(name, []) { }

    public IEnumerable<Tree> GetChildren()
    {
      return Children;
    }
  }

  [Test]
  public void Traverse_ITree_Works_Visitor1()
  {
    var tree = new Tree("parent", new("childA"), new("childB"));

    List<string> actual = new List<string>();
    tree.Traverse((node) => actual.Add(node.Name));
    Assert.That(actual, Is.EquivalentTo(new string[] { "parent", "childA", "childB" }));
  }

  [Test]
  public void Traverse_ITree_Works_Visitor2()
  {
    var tree = new Tree("parent", new("childA"), new("childB"));

    List<(string, int)> actual = new List<(string, int)>();
    tree.Traverse((node, index) => actual.Add((node.Name, index)));
    Assert.That(
      actual,
      Is.EquivalentTo(new (string, int)[] { "parent".With(0), "childA".With(1), "childB".With(1) })
    );
  }

  [Test]
  public void Traverse_Works_Visitor1()
  {
    var tree = new Tree("parent", new("childA"), new("childB"));

    List<string> actual = new List<string>();
    tree.Traverse((node) => actual.Add(node.Name), node => node.Children);
    Assert.That(actual, Is.EquivalentTo(new string[] { "parent", "childA", "childB" }));
  }

  [Test]
  public void Traverse_Works_Visitor2()
  {
    var tree = new Tree("parent", new("childA"), new("childB"));

    List<(string, int)> actual = new List<(string, int)>();
    tree.Traverse((node, index) => actual.Add((node.Name, index)), (node) => node.Children);
    Assert.That(
      actual,
      Is.EquivalentTo(new (string, int)[] { "parent".With(0), "childA".With(1), "childB".With(1) })
    );
  }

  [Test]
  public void TraverseFlat_ITree_Works()
  {
    var childA = new Tree("childA");
    var childB = new Tree("childB");
    var parent = new Tree("parent", childA, childB);

    var actual = parent.TraverseFlat();
    Assert.That(
      actual,
      Is.EquivalentTo(new (Tree, int)[] { parent.With(0), childA.With(1), childB.With(1) })
    );
  }

  [Test]
  public void TraverseFlat_Works()
  {
    var childA = new Tree("childA");
    var childB = new Tree("childB");
    var parent = new Tree("parent", childA, childB);

    var actual = parent.TraverseFlat(x => x.Children);
    Assert.That(
      actual,
      Is.EquivalentTo(new (Tree, int)[] { parent.With(0), childA.With(1), childB.With(1) })
    );
  }

  [Test]
  public void PrintTree_Works()
  {
    var childA = new Tree("childA");
    var childB = new Tree("childB", new Tree("childC"));
    var parent = new Tree("parent", childA, childB);

    var actual = parent.PrintTree(x => x.Children, x => x.Name);
    Assert.That(actual, Is.EquivalentTo("parent\n|-childA\n|-childB\n| |-childC"));
  }
}
