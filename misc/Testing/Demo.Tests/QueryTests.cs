using System.Text;
using Microsoft.Extensions.DiagnosticAdapter.Internal;

namespace Demo.Tests;

public class QueryTests : QueryTestBase
{
    [Fact]
    public async Task FetchSophie()
    {
        var result = await ExecuteQueryAsync(
            @"
                {
                    puppy(id: ""Id_Sophie"") { 
                        name 
                        breed 
                    } 
                }
                ");

        result.MatchSnapshot();
    }
}