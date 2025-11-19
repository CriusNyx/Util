namespace CriusNyx.Util.Tests;

class ClassA { }

class ClassB { }

public class LinqExtensionsTests
{
  [Test]
  public void Foreach_Works()
  {
    string[] source = ["a", "b"];
    HashSet<string> actual = new HashSet<string>();
    source.Foreach((x) => actual.Add(x));
    var expected = new HashSet<string>(["a", "b"]);
    Assert.That(actual, Is.EquivalentTo(expected));
  }

  [Test]
  public void WhereAs_Filters_Array()
  {
    var a = new ClassA();
    var b = new ClassB();
    object[] arr = [a, b];
    var actual = arr.WhereAs<ClassA>();
    Assert.That(actual, Is.EquivalentTo(new ClassA[] { a }));
  }

  [Test]
  public void ThenConcat_Works()
  {
    var a = new object();
    var b = new object();
    var actual = a.ThenConcat([b]);
    Assert.That(actual, Is.EquivalentTo(new object[] { a, b }));
  }

  [Test]
  public void AsArray_Works()
  {
    var a = new object();
    var actual = a.AsArray();
    Assert.That(actual, Is.EquivalentTo(new object[] { a }));
  }

  [Test]
  public void WithIndex_Works()
  {
    string[] elements = ["A", "B"];
    var actual = elements.WithIndex();
    (string, int)[] expected = ["A".With(0), "B".With(1)];
    Assert.That(actual, Is.EquivalentTo(expected));
  }

  [Test]
  public void PadWith_Works()
  {
    string[] elements = ["A", "B"];
    var actual = elements.PadWith(3, "");
    Assert.That(actual, Is.EquivalentTo(new string[] { "A", "B", "" }));
  }

  [Test]
  public void GetOrSet_GetWorks()
  {
    var expected = new object();
    var dictionary = new Dictionary<string, object>();
    dictionary["A"] = expected;
    var actual = dictionary.GetOrSet("A", () => new object());
    Assert.That(actual, Is.EqualTo(expected));
  }

  [Test]
  public void GetOrSet_CreateWorks()
  {
    var expected = new object();
    var dictionary = new Dictionary<string, object>();
    var actual = dictionary.GetOrSet("A", () => expected);
    Assert.That(actual, Is.EqualTo(expected));
  }
}
