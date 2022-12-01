using System.Security.Claims;
using System.Threading.Tasks;
using FluentValidation.Results;
using HotChocolate.Resolvers;

namespace Demo.Server.Validators
{
    /// <remarks>
    /// Borrowed from https://github.com/ChilliCream/hotchocolate/issues/1197#issuecomment-552108941
    /// </remarks>
    public class InputValidationMiddleware<TInput>
    {
        private readonly FieldDelegate next;
        private readonly IInputValidator<TInput> validator;
        private readonly string inputArgumentName;

        public InputValidationMiddleware(
            FieldDelegate next,
            IInputValidator<TInput> validator,
            string inputArgumentName)
        {
            this.next = next;
            this.validator = validator;
            this.inputArgumentName = inputArgumentName;
        }

        public async Task InvokeAsync(IMiddlewareContext context)
        {
            var input = context.ArgumentValue<TInput>(inputArgumentName);
            if (input != null)
            {
                ValidationResult result = await validator.ValidateAsync(input);
                if (!result.IsValid)
                {
                    // Include error in query result
                    context.ReportError(result.ToString());

                    // Input validation failed; don't execute field resolver
                    return;
                }

                // Validation succeeded; execute next middleware in the chain (e.g., field resolver)
                await next(context).ConfigureAwait(false);

                return;
            }

            await next(context).ConfigureAwait(false);
        }
    }
}