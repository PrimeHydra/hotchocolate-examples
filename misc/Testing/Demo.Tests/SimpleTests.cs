using System.Text;
using Microsoft.Extensions.DiagnosticAdapter.Internal;

namespace Demo.Tests;

public class SimpleTests
{
    [Fact]
    public async Task SchemaChangeTest()
    {
        var schema = await TestServices.Executor.GetSchemaAsync(default);
        schema.ToString().MatchSnapshot();
    }

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