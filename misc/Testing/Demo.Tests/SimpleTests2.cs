namespace Demo.Tests;

public class SimpleTests2
{
    [Fact]
    public async Task FetchAuthor()
    {
        var result = await TestServices.ExecuteRequestAsync(
            b => b.SetQuery("{ book { author { name } } }"));

        result.MatchSnapshot();
    }

    [Fact]
    public async Task FetchBookAndAuthor()
    {
        var result = await TestServices.ExecuteRequestAsync(
            b => b.SetQuery("{ book { title author { name } } }"));

        result.MatchSnapshot();
    }

    [Fact]
    public async Task FetchAuthor1()
    {
        var result = await TestServices.ExecuteRequestAsync(
            b => b.SetQuery("{ author { name } }"));

        result.MatchSnapshot();
    }
}