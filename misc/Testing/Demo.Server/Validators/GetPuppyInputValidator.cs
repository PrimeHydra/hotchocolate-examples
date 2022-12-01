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
        public GetPuppyInputValidator(IDataContext dataContext) 
            : base(dataContext)
        {
            this.RuleFor(
                puppyId =>
                    puppyId).MustAsync(
                async (puppyId, _) =>
                {
                    Puppy? puppy = await dataContext.PuppyRepository.GetAsync(puppyId);

                    return puppy != null;
                }).WithMessage(s => $"The puppy with ID ${s} does not exist.");
        }
    }
}