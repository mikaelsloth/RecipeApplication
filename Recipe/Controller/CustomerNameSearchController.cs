namespace Recipe.Controller
{
    using Microsoft.EntityFrameworkCore;
    using Recipe.Models.Db;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class CustomerNameSearchController : BaseController, ICustomerNameSearchController
    {
        public CustomerNameSearchController(RecipeDbContext context) : base(context)
        {
        }

        public async Task<IList<CustomerAutoCompleteNameTextView>> GetAll() => await Database.CustomerAutoCompleteNameTextViews.ToListAsync();

    }
}
