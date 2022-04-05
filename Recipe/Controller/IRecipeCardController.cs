namespace Recipe.Controller
{
    using Recipe.Models.Db;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IRecipeCardController
    {
        Task<int> Add(RecipeCard recipeCard);
        
        Task Delete(int id);
        
        Task Modify(RecipeCard recipeCard);
        
        Task<RecipeCard> Get(int id);
        
        Task<IList<RecipeCard>> GetByCustomer(int id);
    }
}
