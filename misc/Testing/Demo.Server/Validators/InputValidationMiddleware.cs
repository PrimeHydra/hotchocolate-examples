using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;
using Demo.Server.Data;
using FluentValidation.Results;
using HotChocolate.Resolvers;

namespace Demo.Server.Validators
{
    /// <remarks>
    /// Borrowed from https://github.com/ChilliCream/hotchocolate/issues/1197#issuecomment-552108941
    /// </remarks>
    public class InputValidationMiddleware<TInput>
    {
        private readonly Type validatorType;
        private readonly FieldDelegate next;
        private readonly string inputArgumentName;

        public InputValidationMiddleware(
            Type validatorType,
            FieldDelegate next,
            string inputArgumentName)
        {
            this.validatorType = validatorType;
            this.next = next;
            this.inputArgumentName = inputArgumentName;
        }

        public async Task InvokeAsync(IMiddlewareContext context)
        {
            var input = context.ArgumentValue<TInput>(inputArgumentName);
            if (input != null)
            {
                IInputValidator<TInput> validator = CreateValidator(context);
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

        private IInputValidator<TInput> CreateValidator(IMiddlewareContext middlewareContext)
        {
            var services = new ServiceCollection();
            services.AddScoped(this.validatorType);
            services.AddScoped(_ => middlewareContext.Service<IDataContext>());
            ServiceProvider provider = services.BuildServiceProvider();

            return (IInputValidator<TInput>)provider.GetRequiredService(this.validatorType);
        }
    }
}