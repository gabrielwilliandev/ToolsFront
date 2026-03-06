namespace Tools.Application.Common.Result
{
    public class Result<T>
    {
        public bool IsSuccess { get; }
        public T? Value { get; }
        public List<Error> Errors { get; }

        protected Result(bool isSuccess, T? value, List<Error> errors)
        {
            IsSuccess = isSuccess;
            Value = value;
            Errors = errors;
        }

        public static Result<T> Success(T value)
        {
            return new Result<T>(true, value, new List<Error>());
        }

        public static Result<T> Failure(List<Error> errors)
        {
            return new Result<T>(false, default, errors);
        }
    }
}
