using FluentValidation;
using System.Text.Json;
using Tools.Api.Responses;
using Tools.Domain.Exceptions;

namespace Tools.Api.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(
            RequestDelegate next,
            ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (ValidationException ex) 
            {

                    httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                    httpContext.Response.ContentType = "application/json";

                    var errors = ex.Errors.Select(x => x.ErrorMessage);

                    await httpContext.Response.WriteAsJsonAsync(ApiResponse<object>.FailureResponse(errors));
            }
            catch (DomainException ex)
            {
                    httpContext.Response.StatusCode = StatusCodes.Status422UnprocessableEntity;
                    httpContext.Response.ContentType = "application/json";

                await httpContext.Response.WriteAsJsonAsync(ApiResponse<object>.FailureResponse(ex.Message));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro inexperado");
                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                httpContext.Response.ContentType = "application/json";
                await httpContext.Response.WriteAsJsonAsync(
                    ApiResponse<object>.FailureResponse(
                        new[] { "Ocorreu um erro inesperado. Por favor, tente novamente mais tarde." }
                    )
                );
            }
        }
    }
}
