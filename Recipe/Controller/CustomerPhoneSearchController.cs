namespace Recipe.Controller
{
    using Microsoft.EntityFrameworkCore;
    using Recipe.Models.Db;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class CustomerPhoneSearchController : BaseController, ICustomerPhoneSearchController
    {
        public CustomerPhoneSearchController(RecipeDbContext context) : base(context)
        {
        }

        public async Task<IList<CustomerAutoCompletePhoneTextView>> GetAll() => await Task.Run(() => Database.CustomerAutoCompletePhoneTextViews.ToListAsync());
    }
}
