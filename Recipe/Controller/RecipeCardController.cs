namespace Recipe.Controller
{
    using Microsoft.EntityFrameworkCore;
    using Recipe.Models.Db;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class RecipeCardController : BaseController, IRecipeCardController
    {
        public RecipeCardController(RecipeDbContext context) : base(context)
        {
        }

        public async Task<int> Add(RecipeCard recipeCard)
        {
            try
            {
                _ = await Database.RecipeCards.AddAsync(recipeCard);
                _ = await Database.SaveChangesAsync();
                return recipeCard.Id;
            }
            catch { throw; }
        }

        public async Task Delete(int id)
        {
            try
            {
                RecipeCard? original = await Database.RecipeCards.FindAsync(id);
                if (original != null)
                {
                    _ = Database.RecipeCards.Remove(original);
                    _ = await Database.SaveChangesAsync();
                    return;
                }

                throw new InvalidOperationException("Cannot delete a non-existing recipe card");
            }
            catch { throw; }
        }

        public async Task<RecipeCard> Get(int id) => await Database.RecipeCards
            .FindAsync(id);

        public async Task<IList<RecipeCard>> GetByCustomer(int id) => await Database.RecipeCards
            .Where(r => r.CustomerId == id).ToListAsync();

        public async Task Modify(RecipeCard recipeCard)
        {
            try
            {
                RecipeCard? original = await Database.RecipeCards.FindAsync(recipeCard.Id);
                if (original != null)
                {
                    original.MergeWith(recipeCard);
                    _ = await Database.SaveChangesAsync();
                    return;
                }

                throw new InvalidOperationException("Cannot modify a non-existing or deleted recipe card");
            }
            catch { throw; }
        }
    }
}
