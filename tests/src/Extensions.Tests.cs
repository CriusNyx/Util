using System.Data.Common;

namespace CriusNyx.Util.Tests;

public class ExtensionTests
{
  [Test]
  public void NotNull_Dereferences_Defined_Valued()
  {
    var value = new object();
    var result = value.NotNull();
    Assert.That(value, Is.EqualTo(result));
  }

  [Test]
  public void NotNull_Throws_On_Null()
  {
    object value = null!;
    Assert.Throws<NullReferenceException>(() => value.NotNull());
  }

  [Test]
  public void With_Works()
  {
    var a = new object();
    var b = new object();
    var c = new object();
    var d = new object();

    Assert.That(a.With(b), Is.EqualTo((a, b)));
    Assert.That(a.With(b).AndWith(c), Is.EqualTo((a, b, c)));
    Assert.That(a.With(b).AndWith(c).AndWith(d), Is.EqualTo((a, b, c, d)));
  }

  [Test]
  public void Touch_Works()
  {
    int element = 3;
    int idk = 0;
    int result = element.Touch(x => idk += x);
    Assert.That(element, Is.EqualTo(3));
    Assert.That(idk, Is.EqualTo(3));
    Assert.That(result, Is.EqualTo(3));
  }

  [Test]
  public void Safe_Arr_Works()
  {
    string[] list = ["Hello", "World"];
    Assert.That(list.Safe(0), Is.EqualTo("Hello"));
    Assert.That(list.Safe(1), Is.EqualTo("World"));
    Assert.Null(list.Safe(-1));
    Assert.Null(list.Safe(2));
  }

  [Test]
  public void Safe_List_Works()
  {
    List<string> list = new List<string>() { "Hello", "World" };
    Assert.That(list.Safe(0), Is.EqualTo("Hello"));
    Assert.That(list.Safe(1), Is.EqualTo("World"));
    Assert.Null(list.Safe(-1));
    Assert.Null(list.Safe(2));
  }

  [Test]
  public void Safe_Dictionary_Works()
  {
    Dictionary<string, string> dict = new Dictionary<string, string>() { { "key", "value" } };
    Assert.That(dict.Safe("key"), Is.EqualTo("value"));
    Assert.Null(dict.Safe("value"));
  }
}
