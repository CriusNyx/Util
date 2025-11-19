namespace CriusNyx.Util.Tests;

public class EquivalentTestClass(string value) : Equivalent
{
  public string Value => value;

  public bool Equivalent(object other)
  {
    return other is EquivalentTestClass equivalent && value.Equals(equivalent.Value);
  }
}

public class EquivalentTests
{
  [Test]
  public void TestClass_Equivalent_Matches()
  {
    var a = new EquivalentTestClass("a");
    var b = new EquivalentTestClass("a");
    Assert.That(a.Equivalent(b));
  }

  [Test]
  public void TestClass_NotEquivalent_DoesntMatch()
  {
    var a = new EquivalentTestClass("a");
    var b = new EquivalentTestClass("b");
    Assert.That(!a.Equivalent(b));
  }

  static IEnumerable<EquivalentTestClass> TestClassArr(params EquivalentTestClass[] args)
  {
    return args;
  }

  static IEnumerable<object> ObjectArr(params object[] args)
  {
    return args;
  }

  public static object[] TestArr(IEnumerable<EquivalentTestClass> left, IEnumerable<object> right)
  {
    return [left, right];
  }

  public static object[] SetEquivelant_ShouldMatch_Data()
  {
    return
    [
      TestArr(TestClassArr(), ObjectArr()),
      TestArr(TestClassArr(new EquivalentTestClass("a")), ObjectArr(new EquivalentTestClass("a"))),
      TestArr(
        TestClassArr(new EquivalentTestClass("a"), new EquivalentTestClass("b")),
        ObjectArr(new EquivalentTestClass("a"), new EquivalentTestClass("b"))
      ),
    ];
  }

  [TestCaseSource(nameof(SetEquivelant_ShouldMatch_Data))]
  public void SetEquivelant_ShouldMatch(
    IEnumerable<EquivalentTestClass> left,
    IEnumerable<object> right
  )
  {
    Assert.That(left.SetEquivalent(right));
  }

  public static object[] SetEquivelant_ShouldntMatch_Data()
  {
    return
    [
      TestArr(TestClassArr(new EquivalentTestClass("a")), ObjectArr()),
      TestArr(TestClassArr(), ObjectArr(new EquivalentTestClass("a"))),
      TestArr(TestClassArr(new EquivalentTestClass("a")), ObjectArr(new EquivalentTestClass("b"))),
      TestArr(TestClassArr(new EquivalentTestClass("a")), ObjectArr(new object())),
    ];
  }

  [TestCaseSource(nameof(SetEquivelant_ShouldntMatch_Data))]
  public void SetEquivelant_ShouldntMatch(
    IEnumerable<EquivalentTestClass> left,
    IEnumerable<object> right
  )
  {
    Assert.That(!left.SetEquivalent(right));
  }

  public static IEnumerable<object[]> DictionaryEquivelent_ShouldMatch_Data()
  {
    yield return
    [
      new Dictionary<string, EquivalentTestClass>() { },
      new Dictionary<string, object>(),
    ];

    yield return
    [
      new Dictionary<string, EquivalentTestClass>() { { "Foo", new EquivalentTestClass("Bar") } },
      new Dictionary<string, object>() { { "Foo", new EquivalentTestClass("Bar") } },
    ];
  }

  [TestCaseSource(nameof(DictionaryEquivelent_ShouldMatch_Data))]
  public void DictionaryEquivelent_ShouldMatch(
    IDictionary<string, EquivalentTestClass> left,
    IDictionary<string, object> right
  )
  {
    Assert.That(left.DictionaryEquivalent(right));
  }

  public static IEnumerable<object[]> DictionaryEquivelent_ShouldntMatch_Data()
  {
    yield return
    [
      new Dictionary<string, EquivalentTestClass>() { { "Foo", new EquivalentTestClass("Bar") } },
      new Dictionary<string, object>(),
    ];

    yield return
    [
      new Dictionary<string, EquivalentTestClass>() { },
      new Dictionary<string, object>() { { "Foo", new EquivalentTestClass("Bar") } },
    ];

    yield return
    [
      new Dictionary<string, EquivalentTestClass>() { { "Foo", new EquivalentTestClass("Bar") } },
      new Dictionary<string, object>() { { "Foo", new EquivalentTestClass("Baz") } },
    ];

    yield return
    [
      new Dictionary<string, EquivalentTestClass>() { { "Foo", new EquivalentTestClass("Bar") } },
      new Dictionary<string, object>() { { "Foo", new object() } },
    ];

    yield return
    [
      new Dictionary<string, EquivalentTestClass>() { { "Foo", new EquivalentTestClass("Bar") } },
      new Dictionary<string, object>() { { "Baz", new EquivalentTestClass("Bar") } },
    ];

    yield return
    [
      new Dictionary<string, EquivalentTestClass>()
      {
        { "Foo", new EquivalentTestClass("Bar") },
        { "Baz", new EquivalentTestClass("Bar") },
      },
      new Dictionary<string, object>() { { "Foo", new EquivalentTestClass("Bar") } },
    ];
    yield return
    [
      new Dictionary<string, EquivalentTestClass>() { { "Foo", new EquivalentTestClass("Bar") } },
      new Dictionary<string, object>()
      {
        { "Foo", new EquivalentTestClass("Bar") },
        { "Baz", new EquivalentTestClass("Bar") },
      },
    ];
  }

  [TestCaseSource(nameof(DictionaryEquivelent_ShouldntMatch_Data))]
  public void DictionaryEquivelent_ShouldntMatch(
    IDictionary<string, EquivalentTestClass> left,
    IDictionary<string, object> right
  )
  {
    Assert.That(!left.DictionaryEquivalent(right));
  }
}
