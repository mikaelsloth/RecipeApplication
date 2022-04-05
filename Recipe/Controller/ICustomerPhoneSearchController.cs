namespace Recipe.Controller
{
    using Recipe.Models.Db;

    public interface ICustomerPhoneSearchController : IAutoCompleteList<CustomerAutoCompletePhoneTextView>
    {
    }
}