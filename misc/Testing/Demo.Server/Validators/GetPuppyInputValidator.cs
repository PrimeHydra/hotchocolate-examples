using Demo.Server.Data;
using Demo.Server.Models;
using FluentValidation;

namespace Demo.Server.Validators
{
    /// <summary>
    /// Contrived input validator just to have one in the mix.
    /// </summary>
    public class GetPuppyInputValidator : InputValidator<string>
    {
        public GetPuppyInputValidator(IRepository<Puppy> puppyRepo)
        {
            this.RuleFor(
                puppyId =>
                    puppyId).MustAsync(
                async (puppyId, _) =>
                {
                    Puppy? puppy = await puppyRepo.GetAsync(puppyId);

                    return puppy != null;
                }).WithMessage(s => $"The puppy with ID ${s} does not exist.");
        }
    }
}