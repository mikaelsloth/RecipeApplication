namespace Recipe.Controller
{
    using Recipe.Models.Db;
    using System.Threading.Tasks;

    public interface ICustomerController
    {
        Task<int> Add(Customer customer);

        Task Delete(int id);

        Task<Customer?> Get(int id);

        Task<Customer?> GetFromNameSearch(string autocompletetext);

        Task<Customer?> GetFromPhoneSearch(string autocompletetext);

        Task Modify(Customer customer);

        Task PermanentDelete(int id);
    }
}