namespace Recipe.BusinessRules
{
    public interface IValidatable<T> where T : IValidatable<T>
    {
        DomainResult Validate(IValidateEntities<T> validateModel);
    }
}
