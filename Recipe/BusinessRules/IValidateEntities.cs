namespace Recipe.BusinessRules
{
    public interface IValidateEntities<T> where T : IValidatable<T>
    {
        DomainResult Validate(IValidatable<T> entity);
    }
}
