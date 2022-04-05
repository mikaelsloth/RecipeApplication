namespace Recipe.Controller
{
    using Recipe.Models.Db;
    using System.Threading.Tasks;

    public interface ICustomerHistoryController
    {
        Task<PagedResult<LatestCustomersView>> GetPage(int page, int pageSize, int maxrecords);
    }
}
