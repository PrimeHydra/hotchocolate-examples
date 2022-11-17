namespace Demo.Tests;

public class MutationTests : QueryTestBase
{
    [Fact]
    public async Task AddSparkyAndReadHimBack()
    {
        await this.ExecuteQueryAsync(
            @"
mutation {
  addPuppy(input: { name: ""Sparky"", breed: ""Beagle"" }) {
    id
  }
}
                ");

        var result = await this.ExecuteQueryAsync(
            @"
{
  puppy(id: ""Id_1"") {
    name
    breed
  }
}

                   
                ");

        result.MatchSnapshot();
    }

    [Fact]
    public async Task AddDaisyAndReadHerBack()
    {
        await this.ExecuteQueryAsync(
            @"
mutation {
  addPuppy(input: { name: ""Daisy"", breed: ""Golden Doodle"" }) {
    id
  }
}
                ");

        var result = await this.ExecuteQueryAsync(
            @"
{
  puppy(id: ""Id_1"") {
    name
    breed
  }
}

                   
                ");

        result.MatchSnapshot();
    }

}