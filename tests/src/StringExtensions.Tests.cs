namespace CriusNyx.Util.Tests;

public class StringExtensionsTests
{
  [Test]
  public void Ident_Works_Correctly()
  {
    Assert.That("test1".Indent("  "), Is.EqualTo("  test1"));
    Assert.That("test2\ntest2".Indent("  "), Is.EqualTo("  test2\n  test2"));
  }

  [Test]
  public void StringJoinWorksCorrectly()
  {
    Assert.That(new string[] { "a", "b" }.StringJoin(), Is.EqualTo("ab"));
    Assert.That(new string[] { "a", "b" }.StringJoin("\n"), Is.EqualTo("a\nb"));
  }
}

public class CanElipsizeTests
{
  [DatapointSource]
  public object[] CanTruncate_WithElipsize_Data =
  [
    ("abc123", "abc123", -1),
    ("abc123", "", 0),
    ("abc123", ".", 1),
    ("abc123", "..", 2),
    ("abc123", "...", 3),
    ("abc123", "a...", 4),
    ("abc123", "ab...", 5),
    ("abc123", "abc123", 6),
    ("abc123", "abc123", 7),
  ];

  [Theory]
  public void CanTruncate_WithElipsize(object data)
  {
    var (source, expected, truncateLength) = ((string, string, int))data;
    string actual = source.Truncate(truncateLength, "...");
    Assert.That(actual, Is.EqualTo(expected));
  }
}

public class NoElipsizeTests
{
  [DatapointSource]
  public object[] CanTruncate_NoElipsize_Data =
  [
    ("abc123", "abc123", -1),
    ("abc123", "", 0),
    ("abc123", "a", 1),
    ("abc123", "ab", 2),
    ("abc123", "abc", 3),
    ("abc123", "abc1", 4),
    ("abc123", "abc12", 5),
    ("abc123", "abc123", 6),
    ("abc123", "abc123", 7),
  ];

  [Theory]
  public void CanTruncate_NoElipsize(object data)
  {
    var (source, expected, truncateLength) = ((string, string, int))data;
    string actual = source.Truncate(truncateLength, "");
    Assert.That(actual, Is.EqualTo(expected));
  }
}

public class PrintGridTest(string[][] source, string expected)
{
  public string[][] Source => source;
  public string Expected => expected;
}

public class PrintGridTests
{
  [DatapointSource]
  public PrintGridTest[] PrintGridTestData =
  [
    new PrintGridTest([], ""),
    new PrintGridTest(
      [
        ["a", "b"],
      ],
      "a b"
    ),
    new PrintGridTest(
      [
        ["a"],
        ["b"],
      ],
      "a\nb"
    ),
    new PrintGridTest(
      [
        ["a", "b"],
        ["c", "d"],
      ],
      "a b\nc d"
    ),
    new PrintGridTest(
      [
        ["a", "b"],
        ["c"],
      ],
      "a b\nc  "
    ),
    new PrintGridTest(
      [
        ["a"],
        ["c", "d"],
      ],
      "a  \nc d"
    ),
    new PrintGridTest(
      [
        ["", "b"],
        ["c", "d"],
      ],
      "  b\nc d"
    ),
    new PrintGridTest(
      [
        ["", "b"],
        ["", "d"],
      ],
      " b\n d"
    ),
    new PrintGridTest(
      [
        ["a", "b"],
        ["", "d"],
      ],
      "a b\n  d"
    ),
    new PrintGridTest(
      [
        ["", "b"],
        ["", ""],
      ],
      " b\n  "
    ),
  ];

  [Theory]
  public void CanFormatGrid(PrintGridTest data)
  {
    var actual = data.Source.FormatGrid(" ");
    Assert.That(actual, Is.EqualTo(data.Expected));
  }
}

public class PrintGridTestsElipsize
{
  [DatapointSource]
  public PrintGridTest[] PrintGridTestData =
  [
    new PrintGridTest([], ""),
    new PrintGridTest(
      [
        ["abc123", "abc1", "a"],
      ],
      "a... abc1 a"
    ),
  ];

  [Theory]
  public void CanFormatGrid(PrintGridTest data)
  {
    var actual = data.Source.FormatGrid(" ", 4, true);
    Assert.That(actual, Is.EqualTo(data.Expected));
  }
}

public class PrintGridTestsNoElipsize
{
  [DatapointSource]
  public PrintGridTest[] PrintGridTestData =
  [
    new PrintGridTest([], ""),
    new PrintGridTest(
      [
        ["abc123", "abc1", "a"],
      ],
      "abc1 abc1 a"
    ),
  ];

  [Theory]
  public void CanFormatGrid(PrintGridTest data)
  {
    var actual = data.Source.FormatGrid(" ", 4, false);
    Assert.That(actual, Is.EqualTo(data.Expected));
  }
}
