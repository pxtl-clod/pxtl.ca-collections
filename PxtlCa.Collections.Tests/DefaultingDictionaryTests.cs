using FluentAssertions;

namespace PxtlCa.Collections.Tests;

public class DefaultingDictTests {
    [Fact]
    public void ProvidedFoundKey_ReturnsValue() {
        var dict = new DefaultingDictionary<string, int> {
            ["one"] = 1,
            ["two"] = 2,
            ["three"] = 3,
            ValueConstructionHandler = (key) => -99
        };

        dict["two"].Should().Be(2);
    }

    [Fact]
    public void ProvidedMissingKey_ReturnsDefault() {
        var dict = new DefaultingDictionary<string, int> {
            ["one"] = 1,
            ["two"] = 2,
            ["three"] = 3,
            ValueConstructionHandler = (key) => -99
        };
        dict["four"].Should().Be(-99);
    }
}