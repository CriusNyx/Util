using CriusNyx.Util.Reflection;

namespace CriusNyx.Util.Tests;

class Foo
{
  public string field;
  public string property { get; set; }
}

public class ReflectionTests
{
  [Test]
  public void ReflectFieldWorks()
  {
    var instance = new Foo { field = "field", property = "property" };
    var actual = instance.ReflectValue("field");
    Assert.That(actual, Is.EqualTo("field"));
  }

  [Test]
  public void ReflectPropertyWorks()
  {
    var instance = new Foo { field = "field", property = "property" };
    var actual = instance.ReflectValue("property");
    Assert.That(actual, Is.EqualTo("property"));
  }
}
