namespace CriusNyx.Util.Tests;

public class ExtensionTests
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
  public void As_NotNull_Returns_When_Correct()
  {
    var a = new ClassA();
    var o = a;
    var actual = o.AsNotNull<ClassA>();
    Assert.That(actual, Is.EqualTo(a));
  }

  [Test]
  public void As_NotNull_Throws_When_Not_Correct()
  {
    var o = new object();
    Assert.Throws<NullReferenceException>(() =>
    {
      o.AsNotNull<ClassA>();
    });
  }

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
  public void OrDefault_Works_When_Correct()
  {
    var expected = new object();
    var actual = expected.OrDefault(new object());
    Assert.That(actual, Is.EqualTo(expected));
  }

  [Test]
  public void OrDefault_Works_When_Null()
  {
    object? source = null!;
    var expected = new object();
    var actual = source.OrDefault(expected);
    Assert.That(actual, Is.EqualTo(expected));
  }

  [Test]
  public void OrDefaultWith_Works_When_Correct()
  {
    var expected = new object();
    var actual = expected.OrDefaultWith(() => new object());
    Assert.That(actual, Is.EqualTo(expected));
  }

  [Test]
  public void OrDefaultWith_Works_When_Null()
  {
    object? source = null!;
    var expected = new object();
    var actual = source.OrDefaultWith(() => expected);
    Assert.That(actual, Is.EqualTo(expected));
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
  public void Safe_Func_Works()
  {
    List<string> list = new List<string>() { "Hello" };
    Assert.That(list.Safe((x) => x[0]), Is.EqualTo("Hello"));
    Assert.That(list.Safe((x) => x[1]), Is.EqualTo(null));
  }

  [Test]
  public void Safe_Dictionary_Works()
  {
    Dictionary<string, string> dict = new Dictionary<string, string>() { { "key", "value" } };
    Assert.That(dict.Safe("key"), Is.EqualTo("value"));
    Assert.Null(dict.Safe("value"));
  }

  [Test]
  public void Transform_Works()
  {
    var source = "a";
    var result = source.Transform(x => x + "b");
    Assert.That(result, Is.EqualTo("ab"));
  }
}
