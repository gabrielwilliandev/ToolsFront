
using Tools.Api.Responses;
using Tools.Application.Common.Result;
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
            catch (DomainException ex)
            {
                if (!httpContext.Response.HasStarted)
                {
                    httpContext.Response.StatusCode = StatusCodes.Status422UnprocessableEntity;
                    httpContext.Response.ContentType = "application/json";

                    var errors = new[]
                    {
                        new Error(ex.Code, ex.Message)
                    };

                    await httpContext.Response.WriteAsJsonAsync(ApiResponse<object>.FailureResponse(errors));
                }
            }
            catch (Exception ex)
            {
                if (!httpContext.Response.HasStarted)
                {
                    httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                _logger.LogError(ex, "Erro inesperado");
                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                httpContext.Response.ContentType = "application/json";

                var errors = new[]
                {
                    new Error("server.error", "Ocorreu um erro inesperado. Por favor contate o administrador.")
                };

                await httpContext.Response.WriteAsJsonAsync(ApiResponse<object>.FailureResponse(errors));
                }
            }
        }
    }
}