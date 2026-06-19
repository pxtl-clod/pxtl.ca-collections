namespace PxtlCa.Collections.Tests;

public class VirtualDictCollectionTests
{
    [Fact]
    public void CreateEmpty_Collection_Count_Is_Zero()
    {
        var dict = new VirtualDict<string, int>();
        
        Assert.Equal(0, dict.Count);
    }

    [Fact]
    public void AddItems_ContainsKey_Returns_True()
    {
        var dict = new VirtualDict<string, int>
        {
            ["key1"] = 5,
        };

        Assert.True(dict.ContainsKey("key1"));
    }

    [Fact]
    public void RemoveItem_ContainsKey_Is_False_After_Remove()
    {
        var dict = new VirtualDict<string, int> { ["key"] = 1 };

        Assert.True(dict.Remove("key"));
        Assert.False(dict.ContainsKey("key"));
    }

    [Fact]
    public void Count_Single_Key_Equals_One()
    {
        var dict = new VirtualDict<int, string> { [5] = "five" };

        Assert.Equal(1, dict.Count);
    }
}
