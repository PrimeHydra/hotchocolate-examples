using Demo.Server.Data;
using FluentValidation;

namespace Demo.Server.Validators
{
    /// <summary>
    /// Base class for our input validators.
    /// </summary>
    /// <typeparam name="TInput">The type of input this validator validates.</typeparam>
    public abstract class InputValidator<TInput> : AbstractValidator<TInput>, IInputValidator<TInput>
    {
        protected InputValidator(IDataContext dataContext)
        {

            // Short-circuit validation on first validation error.
            this.CascadeMode = CascadeMode.Stop;
        }
    }
}