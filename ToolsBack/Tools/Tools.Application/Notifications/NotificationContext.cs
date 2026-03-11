using Tools.Application.Common.Result;

namespace Tools.Application.Notifications
{
    public class NotificationContext
    {
        private readonly List<Error> _errors = new();

        public IReadOnlyCollection<Error> Errors => _errors;

        public bool HasErrors => _errors.Any();

        public void AddErrors(string code, string message)
        {
            _errors.Add(new Error(code, message));
        }
        public void AddError(IEnumerable<Error> errors)
        {
            _errors.AddRange(errors);
        }
    }
}
