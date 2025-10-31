namespace CriusNyx.Util.Tests;

class ClassA { }

class ClassB { }

public class LinqExtensionsTests
{
  [Test]
  public void As_Returns_When_Correct()
  {
    var a = new ClassA();
    object o = a;
    var actual = o.As<ClassA>();

    Assert.That(actual, Is.EqualTo(a));
  }

  [Test]
  public void As_Returns_Null_When_Not_Correct()
  {
    var a = new ClassB();
    object o = a;
    var actual = o.As<ClassA>();

    Assert.Null(actual);
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
  public void Then_Concat_Works()
  {
    var a = new object();
    var b = new object();
    var actual = a.ThenConcat([b]);
    Assert.That(actual, Is.EquivalentTo(new object[] { a, b }));
  }
}
