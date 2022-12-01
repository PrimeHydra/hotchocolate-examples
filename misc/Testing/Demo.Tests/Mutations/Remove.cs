namespace Demo.Tests.Mutations;

public class Remove : QueryTestBase
{
    [Fact]
    public async Task RemoveSophie()
    {
        await this.ExecuteQueryAsync(
            @"
mutation {
  removePuppy(input: { id: ""Id_Sophie""}) 
}
                ");

        // Should fail validation
        var result = await this.ExecuteQueryAsync(
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