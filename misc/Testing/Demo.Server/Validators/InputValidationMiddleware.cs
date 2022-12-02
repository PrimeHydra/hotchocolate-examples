using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;
using FluentValidation.Results;
using HotChocolate.Resolvers;

namespace Demo.Server.Validators
{
    /// <remarks>
    /// Borrowed from https://github.com/ChilliCream/hotchocolate/issues/1197#issuecomment-552108941
    /// </remarks>
    public class InputValidationMiddleware<TInput, TValidator> where TValidator : IInputValidator<TInput>
    {
        private readonly FieldDelegate next;
        private readonly string inputArgumentName;

        public InputValidationMiddleware(
            FieldDelegate next,
            string inputArgumentName)
        {
            this.next = next;
            this.inputArgumentName = inputArgumentName;
        }

        public async Task InvokeAsync(IMiddlewareContext context)
        {
            var input = context.ArgumentValue<TInput>(this.inputArgumentName);
            if (input != null)
            {
                var validator = context.Service<TValidator>();
                ValidationResult result = await validator.ValidateAsync(input);
                if (!result.IsValid)
                {
                    // Include error in query result
                    context.ReportError(result.ToString());

                    // Input validation failed; don't execute field resolver
                    return;
                }

                // Validation succeeded; execute next middleware in the chain (e.g., field resolver)
                await this.next(context).ConfigureAwait(false);

                return;
            }

            await this.next(context).ConfigureAwait(false);
        }
    }
}