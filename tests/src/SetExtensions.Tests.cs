namespace CriusNyx.Util.Tests;

public class SetExtensionsTests
{
  [Test]
  public void SetSync_Added_Works()
  {
    var original = new HashSet<string>(["A"]);
    string[] newSet = ["A", "B"];
    var expected = original.Sync(newSet);
    Assert.That(expected.added, Is.EquivalentTo(new string[] { "B" }));
    Assert.That(expected.removed, Is.EquivalentTo(new string[] { }));
    Assert.That(expected.unchanged, Is.EquivalentTo(new string[] { "A" }));
  }

  [Test]
  public void SetSync_Removed_Works()
  {
    var original = new HashSet<string>(["A", "B"]);
    string[] newSet = ["A"];
    var expected = original.Sync(newSet);
    Assert.That(expected.added, Is.EquivalentTo(new string[] { }));
    Assert.That(expected.removed, Is.EquivalentTo(new string[] { "B" }));
    Assert.That(expected.unchanged, Is.EquivalentTo(new string[] { "A" }));
  }

  [Test]
  public void SetSync_Added_Removed_Works()
  {
    var original = new HashSet<string>(["A", "B"]);
    string[] newSet = ["A", "C"];
    var expected = original.Sync(newSet);
    Assert.That(expected.added, Is.EquivalentTo(new string[] { "C" }));
    Assert.That(expected.removed, Is.EquivalentTo(new string[] { "B" }));
    Assert.That(expected.unchanged, Is.EquivalentTo(new string[] { "A" }));
  }
}
