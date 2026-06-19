using FluentAssertions;

namespace PxtlCa.Collections.Tests;

public class VirtualDictTests {
    [Fact]
    public void CreateEmpty_CountIsZero() {
        var dict = new VirtualDictionary<string, int>();

        dict.Should().BeEmpty();
    }

    [Fact]
    public void AddItems_ContainsKeyReturnsTrue() {
        var dict = new VirtualDictionary<string, int> {
            ["key1"] = 5,
        };

        dict.Should().ContainKey("key1");
    }

    [Fact]
    public void AddItems_IndexerReturnsCorrectValue() {
        var expectedValue = 5;
        var dict = new VirtualDictionary<string, int> {
            ["key1"] = expectedValue,
        };

        dict["key1"].Should().Be(expectedValue);
    }

    [Fact]
    public void RemoveItem_ContainsKeyIsFalseAfterwards() {
        var expectedKey = "key";
        var dict = new VirtualDictionary<string, int> { [expectedKey] = 1 };

        dict.Remove(expectedKey).Should().BeTrue();
        dict.Should().NotContainKey(expectedKey);
    }

    [Fact]
    public void CrateSingleKeyDict_CountEqualsOne() {
        var dict = new VirtualDictionary<int, string> { [5] = "five" };

        dict.Count.Should().Be(1);
    }
}
