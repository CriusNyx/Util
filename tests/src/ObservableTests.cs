public class ObservableTests
{
  [Test]
  public void Observable_OnChange_Works()
  {
    bool following = false;

    Observable<bool> observable = new Observable<bool>(false);
    observable.OnValueChange += (value) => following = value;

    Assert.That(observable.value, Is.EqualTo(false));
    Assert.That(following, Is.EqualTo(false));

    observable.SetValue(true);

    Assert.That(observable.value, Is.EqualTo(true));
    Assert.That(following, Is.EqualTo(true));

    observable.SetValue(false);

    Assert.That(observable.value, Is.EqualTo(false));
    Assert.That(following, Is.EqualTo(false));
  }

  [Test]
  public void Observable_CustomCompare_Works()
  {
    string following = "";

    Observable<KeyValuePair<int, string>> observable = new Observable<KeyValuePair<int, string>>(
      new KeyValuePair<int, string>(0, "none"),
      (self, other) => self.Key == other.Key
    );

    observable.OnValueChange += (value) =>
    {
      following = value.Value;
    };

    Assert.That(observable.value, Is.EqualTo(new KeyValuePair<int, string>(0, "none")));
    Assert.That(following, Is.EqualTo(""));

    observable.SetValue(new KeyValuePair<int, string>(0, "bar"));

    Assert.That(observable.value, Is.EqualTo(new KeyValuePair<int, string>(0, "bar")));
    Assert.That(following, Is.EqualTo(""));

    observable.SetValue(new KeyValuePair<int, string>(1, "baz"));

    Assert.That(observable.value, Is.EqualTo(new KeyValuePair<int, string>(1, "baz")));
    Assert.That(following, Is.EqualTo("baz"));
  }

  [Test]
  public void Observable_Selector_Works()
  {
    string following = "world";
    Observable<string> observable = new Observable<string>("hello");

    var free = observable.RegisterSelector(x => x.Substring(1), (value) => following = value);

    Assert.That(observable.value, Is.EqualTo("hello"));
    Assert.That(following, Is.EqualTo("world"));

    observable.SetValue("Bar");

    Assert.That(observable.value, Is.EqualTo("Bar"));
    Assert.That(following, Is.EqualTo("ar"));

    free();

    observable.SetValue("Baz");

    Assert.That(observable.value, Is.EqualTo("Baz"));
    Assert.That(following, Is.EqualTo("ar"));
  }

  [Test]
  public void Observable_Selector_CustomCompare_Works()
  {
    string following = "Bar";
    Observable<string> observable = new Observable<string>("Bar");

    var free = observable.RegisterSelector(
      x => x,
      (value) => following = value,
      (x, y) => x?[0] == y?[0]
    );

    Assert.That(observable.value, Is.EqualTo("Bar"));
    Assert.That(following, Is.EqualTo("Bar"));

    observable.SetValue("Baz");

    Assert.That(observable.value, Is.EqualTo("Baz"));
    Assert.That(following, Is.EqualTo("Bar"));

    observable.SetValue("Foo");

    Assert.That(observable.value, Is.EqualTo("Foo"));
    Assert.That(following, Is.EqualTo("Foo"));
  }
}
