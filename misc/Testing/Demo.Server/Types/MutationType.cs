using Demo.Server.Data;
using Demo.Server.Models;

namespace Demo.Server.Types;

public class MutationType : ObjectType<Mutation>
{
    protected override void Configure(IObjectTypeDescriptor<Mutation> descriptor)
    {
        descriptor.Field("addPuppy")
            .Description("Adds a new puppy to the system and returns it.")
            .Argument(
                "input",
                a => a.Type<NonNullType<AddPuppyInputType>>().Description("Input describing the puppy to add."))
            .Type<PuppyType>()
            .Resolve(
                async context =>
                {
                    var input = context.ArgumentValue<AddPuppyInput>("input");
                    var repo = context.Services.GetRequiredService<IRepository<Puppy>>();
                    var idGenerator = context.Services.GetRequiredService<IIdGenerator>();

                    var puppy = new Puppy
                    {
                        Breed = input.Breed,
                        Name = input.Name,
                        Id = idGenerator.CreateId()
                    };
                    await repo.AddAsync(puppy);

                    return puppy;
                });

        descriptor.Field("removePuppy")
            .Description("Removes a puppy from the system and returns the ID of the puppy that was removed.")
            .Argument(
                "input",
                a => a.Type<NonNullType<RemovePuppyInputType>>().Description("Input specifying the puppy to remove."))
            .Type<StringType>()
            .Resolve(
                async context =>
                {
                    var input = context.ArgumentValue<RemovePuppyInput>("input");
                    var repo = context.Services.GetRequiredService<IRepository<Puppy>>();
                    await repo.DeleteAsync(input.Id);

                    return input.Id;
                });
    }
}