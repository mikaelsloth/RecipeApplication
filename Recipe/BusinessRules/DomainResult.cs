namespace Recipe.BusinessRules
{
    using System;
    using System.Collections.Generic;

    public class DomainResult
    {
        public DomainResult(bool success, IList<ArgumentException>? errormessage)
        {
            Success = success;
            Message = errormessage;
        }

        public bool Success { get; }

        public IList<ArgumentException>? Message { get; }
    }
}
