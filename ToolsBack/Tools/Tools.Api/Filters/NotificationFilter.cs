using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Tools.Api.Responses;
using Tools.Application.Notifications;

namespace Tools.Api.Filters
{
    public class NotificationFilter : IActionFilter
    {
        private readonly NotificationContext _notificationContext;

        public NotificationFilter(NotificationContext notificationContext)
        {
            _notificationContext = notificationContext;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (_notificationContext.HasErrors)
            {
                context.Result = new ObjectResult(
                    ApiResponse<object>.FailureResponse(_notificationContext.Errors)
                )
                {
                    StatusCode = StatusCodes.Status422UnprocessableEntity
                };
                return;
            }
            if (context.Result is ObjectResult objectResult)
            {
                context.Result = new ObjectResult(
                    ApiResponse<object>.SuccessResponse(objectResult.Value))
                {
                    StatusCode = objectResult.StatusCode
                };
            }
        }
    }
}