namespace Recipe.Controller
{
    using Recipe.Models.Db;
    using System.Linq;
    using System.Threading.Tasks;

    public class CustomerHistoryController : BaseController, ICustomerHistoryController
    {
        public CustomerHistoryController(RecipeDbContext context) : base(context)
        {
        }

        public async Task<PagedResult<LatestCustomersView>> GetPage(int page, int pageSize, int maxrecords) =>
            await Task.Run(() =>
            Database.LatestCustomersViews.OrderByDescending(c => c.LatestRecipeCard).Take(maxrecords).GetPaged(page, pageSize));
    }
}
