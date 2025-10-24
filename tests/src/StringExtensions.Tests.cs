using System.Diagnostics.CodeAnalysis;

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
