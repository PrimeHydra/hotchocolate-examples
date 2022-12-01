using FluentValidation;

namespace Demo.Server.Validators
{
    public interface IInputValidator<TInput> : IValidator<TInput>
    {
    }
}
