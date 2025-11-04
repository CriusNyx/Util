namespace CriusNyx.Util.Tests;

public class TestObject : DebugPrint
{
  public string fieldA = null!;
  public int fieldB;

  public IEnumerable<(string, object)> EnumerateFields()
  {
    return [nameof(fieldA).With(fieldA), nameof(fieldB).With(fieldB)];
  }
}

public class PrintDebugTests
{
  [Test]
  public void PrintDebug_Primitive_Works()
  {
    Assert.That(0.Debug(), Is.EqualTo("0"));
    Assert.That(1.Debug(), Is.EqualTo("1"));
    Assert.That("Hello".Debug(), Is.EqualTo("\"Hello\""));
    Assert.That(false.Debug(), Is.EqualTo("False"));
  }

  [Test]
  public void PrintDebug_Dictionary_Works()
  {
    Dictionary<string, string> source = new Dictionary<string, string>() { { "Hello", "World" } };
    var actual = source.Debug();
    var expected = "{\n  Hello: \"World\"\n}";
    Assert.That(actual, Is.EqualTo(expected));
  }

  const string NestedDictionaryResult =
    @"{
  Hello: {
    World: ""!""
  }
}";

  [Test]
  public void PrintDebug_Nested_Dictionary_Works()
  {
    Dictionary<string, object> source = new Dictionary<string, object>()
    {
      {
        "Hello",
        new Dictionary<string, object>() { { "World", "!" } }
      },
    };
    var actual = source.Debug();
    Assert.That(actual, Is.EqualTo(NestedDictionaryResult));
  }

  [Test]
  public void PrintDebug_Emimerable_Works()
  {
    var array = new object[] { "Hello", "World" };
    var enumerable = array.AsEnumerable();
    var expected = "[\n  \"Hello\",\n  \"World\"\n]";
    var arrayD = array.Debug();
    var enumerableD = enumerable.Debug();
    Assert.That(arrayD, Is.EqualTo(expected));
    Assert.That(enumerableD, Is.EqualTo(expected));
  }

  const string DebugObjectOutput =
    @"TestObject {
  fieldA: ""Hello"",
  fieldB: 1
}";

  [Test]
  public void PrintDebug_Object_Works()
  {
    var o = new TestObject { fieldA = "Hello", fieldB = 1 };
    var d = o.Debug();
    Assert.That(o.Debug, Is.EqualTo(DebugObjectOutput));
  }
}
