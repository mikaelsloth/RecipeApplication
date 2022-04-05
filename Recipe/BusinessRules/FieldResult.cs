namespace Recipe.BusinessRules
{
    public class FieldResult
    {
        public FieldResult(bool success, bool nullIsSuccess, string message)
        {
            Success = success;
            Message = message;
            NullIsSuccess = nullIsSuccess;
        }

        public bool Success { get; }

        public string Message { get; }

        public bool NullIsSuccess { get; }
    }
}
