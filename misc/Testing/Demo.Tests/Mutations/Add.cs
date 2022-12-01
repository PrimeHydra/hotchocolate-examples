namespace Demo.Tests.Mutations;

public class Add : QueryTestBase
{
    [Fact]
    public async Task AddSparkyAndReadHimBack()
    {
        await ExecuteQueryAsync(
            @"
mutation {
  addPuppy(input: { name: ""Sparky"", breed: ""Beagle"" }) {
    id
  }
}
                ");

        var result = await ExecuteQueryAsync(
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
        await ExecuteQueryAsync(
            @"
mutation {
  addPuppy(input: { name: ""Daisy"", breed: ""Golden Doodle"" }) {
    id
  }
}
                ");

        var result = await ExecuteQueryAsync(
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