using HotChocolate.Resolvers;
using HotChocolate.Types;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demo.Server.Validators
{
    public static class ObjectFieldDescriptorExtensions
    {
        /// <summary>
        /// Uses an <see cref="IInputValidator{TInput}"/> to validate the input for this field.
        /// </summary>
        /// <typeparam name="TValidator">The type of validator to use.</typeparam>
        /// <typeparam name="TInput">The input type that the validator validates. Typically the input model POCO type that's wrapped by the GraphQL input type.</typeparam>
        /// <param name="descriptor">The object field descriptor.</param>
        /// <param name="inputArgumentName">The name of the input argument (e.g., "input"). Needed to retrieve and validate the input.</param>
        /// <returns>The object field descriptor.</returns>
        public static IObjectFieldDescriptor UseValidator<TValidator, TInput>(
            this IObjectFieldDescriptor descriptor,
            string inputArgumentName = "input")
            where TValidator : IInputValidator<TInput>
        {
            return descriptor.Use(
                (services, next) =>
                {
                    var inputValidator = services.GetService<TValidator>();

                    return new InputValidationMiddleware<TInput>(
                        next,
                        inputValidator,
                        inputArgumentName);
                });
        }
    }
}