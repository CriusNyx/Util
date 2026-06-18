using NUnit.Framework.Constraints;

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

public class ReflectionTestObject(string publicField, int privateField)
{
  public string publicField = publicField;
  int privateField = privateField;
}

[DebugPrint]
public class AttributeTestObject(string publicField, int privateField)
{
  [DebugField]
  public string publicField = publicField;

  [DebugField]
  int privateField = privateField;
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

  class ObjectForCustomPrinter
  {
    public required string Key;
    public required string Value;
  }

  [Test]
  public void PrintDebug_CustomPrinter_Works()
  {
    DebugPrint.RegisterCustomType<ObjectForCustomPrinter>(
      (value) => [nameof(value.Key).With(value.Key), nameof(value.Value).With(value.Value)]
    );
    string expected =
      @"ObjectForCustomPrinter {
  Key: ""key"",
  Value: ""value""
}";
    var o = new ObjectForCustomPrinter { Key = "key", Value = "value" };

    Assert.That(o.Debug(), Is.EqualTo(expected));
  }

  [Test]
  public void PrintDebug_EnumerateWithReflection_Works()
  {
    ReflectionTestObject obj = new ReflectionTestObject("test", 2);

    var expected = new (string, object)[] { ("publicField", "test") };
    var actual = DebugPrint.EnumerateWithReflection(obj, typeof(ReflectionTestObject));

    Assert.That(expected, Is.EquivalentTo(actual));
  }

  [Test]
  public void PrintDebug_EnumerateWithAttribute_Works()
  {
    AttributeTestObject obj = new AttributeTestObject("test", 2);

    var expected =
      @"AttributeTestObject {
  publicField: ""test"",
  privateField: 2
}";
    var actual = obj.Debug();

    Assert.That(expected, Is.EqualTo(actual));
  }
}
