using FluentValidation;
using MediatR;
using Microsoft.IdentityModel.Tokens;

namespace Seph.Principal.Application.Common.Behaviors
{
    /*Esta clase no se puede heredar, utiliza un pipedLine de MediaTr y recive un request general
     *donde en esta peticion request generica puede ser un command/ query, y un response generica
     *que puede ser un response de un command o un response de un query
     */
    public sealed class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validatos):IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull 
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (!validatos.Any())
            {
                return await next();
            }
            ///recibimos todo el contexto en general , y lo validamos, si no es valido lanzamos una 
            ///excepcion de validacion, si es valido continuamos con el siguiente comportamiento
            var context = new ValidationContext<TRequest>(request);
            var failures = validatos
                .Select(validator => validator.Validate(context))
                .SelectMany(result => result.Errors)
                .Where(error => error is not null)
                .ToList();
            //si hay errores de validacion, lanzamos una excepcion de validacion con los errores encontrados
            if (failures.Count != 0)
            {
                throw new ValidationException(failures);
            }
            return await next();
        }
    }
}
