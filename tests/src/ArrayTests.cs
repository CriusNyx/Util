namespace CriusNyx.Util.Tests;

struct Point
{
  public int x;
  public int y;

  public Point(int x, int y)
  {
    this.x = x;
    this.y = y;
  }
}

public class ArrayTests
{
  [Test]
  public void Array_Fill_Empty_Works()
  {
    var expected = new int[] { };
    var actual = new int[0].Fill(4);
    Assert.That(actual, Is.EqualTo(expected));
  }

  [Test]
  public void Array_Fill_Works()
  {
    var expected = new int[] { 4, 4, 4 };
    var actual = new int[3].Fill(4);
    Assert.That(actual, Is.EqualTo(expected));
  }

  [Test]
  public void Array_Fill_Null_Throws()
  {
    Assert.Throws<NullReferenceException>(() => (null as int[])!.Fill(-1));
  }

  [Test]
  public void Array_FillFunc_Empty_Works()
  {
    var actual = new object[0].Fill(new object());
    var expected = new object[0];
    Assert.That(actual, Is.EqualTo(expected));
  }

  [Test]
  public void Array_FillFunc_Works()
  {
    object a = new object();
    object b = new object();
    object c = new object();
    var expected = new object[] { a, b, c };
    var enumerator = expected.GetEnumerator();
    Func<object> generator = () => enumerator.Consume()!;
    var actual = new object[3].FillFunc(generator);

    Assert.That(actual, Is.EqualTo(expected));
  }

  [Test]
  public void Array_FillFunc_Null_Throws()
  {
    Assert.Throws<NullReferenceException>(() => (null as object[])!.FillFunc(() => new object()));
  }

  [Test]
  public void Array_FillEmpty_Primitive_Works()
  {
    var source = new int[] { 0, 1, 2 };
    var actual = source.FillEmpty(4);
    var expected = new int[] { 4, 1, 2 };
    Assert.That(actual, Is.EqualTo(expected));
  }

  [Test]
  public void Array_FillEmpty_Struct_Works()
  {
    var source = new Point[] { new Point(), new Point(1, 2), new Point(3, 4) };
    var actual = source.FillEmpty(new Point(5, 6));
    var expected = new Point[] { new Point(5, 6), new Point(1, 2), new Point(3, 4) };
    Assert.That(actual, Is.EqualTo(expected));
  }

  [Test]
  public void Array_FillEmpty_Object_Works()
  {
    var a = new object();
    var b = new object();
    var c = new object();
    var source = new object[] { null!, b, c };
    var actual = source.FillEmpty(a);
    var expected = new object[] { a, b, c };
    Assert.That(actual, Is.EqualTo(expected));
  }

  [Test]
  public void Array_FillEmpty_Empty_Works()
  {
    var actual = new object[0].FillEmpty(new object());
    var expected = new object[0];
    Assert.That(actual, Is.EqualTo(expected));
  }

  [Test]
  public void Array_FillEmpty_Null_Throws()
  {
    Assert.Throws<NullReferenceException>(() =>
    {
      (null as object[])!.FillEmpty(new object());
    });
  }
}
