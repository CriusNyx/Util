namespace CriusNyx.Util.Tests;

class ClassA { }

class ClassB { }

public class LinqExtensionsTests
{
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
  public void AsArray_works()
  {
    var a = new object();
    var actual = a.AsArray();
    Assert.That(actual, Is.EquivalentTo(new object[] { a }));
  }
}
