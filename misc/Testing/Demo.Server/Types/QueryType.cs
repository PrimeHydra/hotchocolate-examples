using Demo.Server.Data;
using Demo.Server.Models;
using Demo.Server.Validators;

namespace Demo.Server.Types
{
    public class QueryType : ObjectType<Query>
    {
        protected override void Configure(IObjectTypeDescriptor<Query> descriptor)
        {
            descriptor.Field("puppy")
                .Description("Gets the puppy with the given id")
                .Argument("id", a => a.Type<NonNullType<StringType>>().Description("ID of the puppy to retrieve."))
                .Type<PuppyType>()
                .UseValidator<GetPuppyInputValidator, string>(inputArgumentName: "id")
                .Resolve(
                    async context =>
                    {
                        var id = context.ArgumentValue<string>("id");
                        var repo = context.Services.GetRequiredService<IRepository<Puppy>>();

                        return await repo.GetAsync(id);
                    });
        }
    }
}