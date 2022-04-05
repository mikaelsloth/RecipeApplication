namespace Recipe.Models.Db
{
    using Recipe.BusinessRules;
    using System;

    public partial class Customer : IValidatable<Customer>
    {
        public virtual DomainResult Validate(IValidateEntities<Customer>? validateModel) => 
            validateModel == null ? throw new ArgumentNullException(nameof(validateModel)) : validateModel.Validate(this);
    }
}
