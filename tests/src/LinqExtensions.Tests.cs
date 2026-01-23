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
  public void Expand_Works()
  {
    var a = new object();
    var b = new object();
    var c = new object();
    object[] original = [a, b];
    var actual = original.Expand([c]);
    Assert.That(actual, Is.EqualTo(new object[] { a, b, c }));
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

  [Test]
  public void Take_2_Empty_Works()
  {
    object[] arr = [];
    var (aActual, bActual) = arr.Take<object, object>();
    Assert.That(aActual, Is.Null);
    Assert.That(bActual, Is.Null);
  }

  [Test]
  public void Take_2_With_1_Works()
  {
    var a = new object();
    object[] arr = [a];
    var (aActual, bActual) = arr.Take<object, object>();
    Assert.That(aActual, Is.EqualTo(a));
    Assert.That(bActual, Is.Null);
  }

  [Test]
  public void Take_2_With_2_Works()
  {
    var a = new object();
    var b = new object();
    object[] arr = [a, b];
    var (aActual, bActual) = arr.Take<object, object>();
    Assert.That(aActual, Is.EqualTo(a));
    Assert.That(bActual, Is.EqualTo(b));
  }

  [Test]
  public void Take_2_With_Types_Works()
  {
    var a = 1;
    var b = "str";
    object[] arr = [a, b];
    var (aActual, bActual) = arr.Take<int, string>();
    Assert.That(aActual, Is.EqualTo(1));
    Assert.That(bActual, Is.EqualTo(b));
  }

  [Test]
  public void Take_2_With_Incorrect_Primitive_Type_Works()
  {
    var a = 1;
    var b = "str";
    object[] arr = [a, b];
    var (aActual, bActual) = arr.Take<int, int>();
    Assert.That(aActual, Is.EqualTo(1));
    Assert.That(bActual, Is.EqualTo(0));
  }

  [Test]
  public void Take_2_With_Null_Primitive_Type_Works()
  {
    var a = 1;
    var b = "str";
    object[] arr = [a, b];
    var (aActual, bActual) = arr.Take<int?, int?>();
    Assert.That(aActual, Is.EqualTo(1));
    Assert.That(bActual, Is.Null);
  }

  [Test]
  public void Take_2_With_Incorrect_Ref_Type_Works()
  {
    var a = 1;
    var b = "str";
    object[] arr = [a, b];
    var (aActual, bActual) = arr.Take<string, string>();
    Assert.That(aActual, Is.Null);
    Assert.That(bActual, Is.EqualTo("str"));
  }

  [Test]
  public void Take_3_Works()
  {
    object[] source = ["a", "b", "c", "d", "e", "f"];
    var (a, b, c) = source.Take<string, string, string>();
    Assert.That(a, Is.EqualTo("a"));
    Assert.That(b, Is.EqualTo("b"));
    Assert.That(c, Is.EqualTo("c"));
  }

  [Test]
  public void Take_4_Works()
  {
    object[] source = ["a", "b", "c", "d", "e", "f"];
    var (a, b, c, d) = source.Take<string, string, string, string>();
    Assert.That(a, Is.EqualTo("a"));
    Assert.That(b, Is.EqualTo("b"));
    Assert.That(c, Is.EqualTo("c"));
    Assert.That(d, Is.EqualTo("d"));
  }

  [Test]
  public void Take_5_Works()
  {
    object[] source = ["a", "b", "c", "d", "e", "f"];
    var (a, b, c, d, e) = source.Take<string, string, string, string, string>();
    Assert.That(a, Is.EqualTo("a"));
    Assert.That(b, Is.EqualTo("b"));
    Assert.That(c, Is.EqualTo("c"));
    Assert.That(d, Is.EqualTo("d"));
    Assert.That(e, Is.EqualTo("e"));
  }

  [Test]
  public void Take_6_Works()
  {
    object[] source = ["a", "b", "c", "d", "e", "f"];
    var (a, b, c, d, e, f) = source.Take<string, string, string, string, string, string>();
    Assert.That(a, Is.EqualTo("a"));
    Assert.That(b, Is.EqualTo("b"));
    Assert.That(c, Is.EqualTo("c"));
    Assert.That(d, Is.EqualTo("d"));
    Assert.That(e, Is.EqualTo("e"));
    Assert.That(f, Is.EqualTo("f"));
  }

  [Test]
  public void Consume_Empty_Works()
  {
    object[] arr = [];
    var actual = arr.GetEnumerator().Consume();
    Assert.That(actual, Is.Null);
  }

  [Test]
  public void Consume_2_Empty_Works()
  {
    object[] arr = [];
    var enumerator = arr.GetEnumerator();
    var a = enumerator.Consume();
    var b = enumerator.Consume();
    Assert.That(a, Is.Null);
    Assert.That(b, Is.Null);
  }

  [Test]
  public void Consume_1_Works()
  {
    var a = new object();
    object[] arr = [a];
    var aActual = arr.GetEnumerator().Consume();
    Assert.That(aActual, Is.EqualTo(a));
  }

  [Test]
  public void Consume_2_Works()
  {
    var a = new object();
    var b = new object();
    object[] arr = [a, b];
    var enumerator = arr.GetEnumerator();
    var aActual = enumerator.Consume();
    var bActual = enumerator.Consume();
    Assert.That(aActual, Is.EqualTo(a));
    Assert.That(bActual, Is.EqualTo(b));
  }

  [Test]
  public void Consume_2_Short_Works()
  {
    var a = new object();
    object[] arr = [a];
    var enumerator = arr.GetEnumerator();
    var aActual = enumerator.Consume();
    var bActual = enumerator.Consume();
    Assert.That(aActual, Is.EqualTo(a));
    Assert.That(bActual, Is.Null);
  }

  [Test]
  public void TryConsume_Empty_Works()
  {
    object[] arr = [];
    var success = arr.GetEnumerator().TryConsume(out var value);
    Assert.That(!success);
    Assert.That(value, Is.Null);
  }

  [Test]
  public void TryConsume_1_Works()
  {
    var a = new object();
    object[] arr = [a];
    var success = arr.GetEnumerator().TryConsume(out var value);
    Assert.That(success);
    Assert.That(value, Is.EqualTo(a));
  }

  [Test]
  public void Consume_WithoutDefault_WithValue_Works()
  {
    var a = "str";
    object[] arr = [a];
    var actual = arr.GetEnumerator().Consume<string>();
    Assert.That(actual, Is.EqualTo(a));
  }

  [Test]
  public void Consume_WithoutDefault_WithoutValue_Works()
  {
    object[] arr = [];
    var actual = arr.GetEnumerator().Consume<string>();
    Assert.That(actual, Is.Null);
  }

  [Test]
  public void Consume_WithDefault_WithValue_Works()
  {
    var a = "str";
    object[] arr = [a];
    var actual = arr.GetEnumerator().Consume<string>("empty");
    Assert.That(actual, Is.EqualTo(a));
  }

  [Test]
  public void Consume_WithDefault_WithoutValue_Works()
  {
    object[] arr = [];
    var actual = arr.GetEnumerator().Consume<string>("empty");
    Assert.That(actual, Is.EqualTo("empty"));
  }
}
