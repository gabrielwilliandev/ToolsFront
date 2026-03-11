using Tools.Application.Common.Result;

namespace Tools.Api.Responses
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public T? Data { get; set; }
        public IEnumerable<Error>? Errors { get; set; }

        public static ApiResponse<T> SuccessResponse(T data)
        {
            return new ApiResponse<T>
            {
                Success = true,
                Data = data,
            };
        }
        public static ApiResponse<T> FailureResponse(IEnumerable<Error> errors)
        {
            return new ApiResponse<T>
            {
                Success = false,
                Errors = errors,
            };
        }
    }
}
