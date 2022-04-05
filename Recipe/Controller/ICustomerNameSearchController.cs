namespace Recipe.Controller
{
    using Recipe.Models.Db;

    public interface ICustomerNameSearchController : IAutoCompleteList<CustomerAutoCompleteNameTextView>
    {
    }
}