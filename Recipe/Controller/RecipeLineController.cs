namespace Recipe.Controller
{
    using Recipe.Models.Db;
    using System;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;
    using System.Threading.Tasks;

    public class RecipeLineController : BaseController, IRecipeLineController
    {
        public RecipeLineController(RecipeDbContext context) : base(context)
        {
        }

        public async Task<RecipeLine?> Get(int id) => await Database.RecipeLines.Include(rl => rl.Units).
                FirstOrDefaultAsync(i => i.Id == id);

        public async Task<IList<RecipeLine>> GetByCard(int recipeCardId) => await Database.RecipeLines.Include(rl => rl.Units)
                .Where(r => r.RecipeCardId == recipeCardId ).ToListAsync();

        public async Task<IList<RecipeLine>> GetByCardAndMedicin(int recipeCardId, int medicinTypeId) => await Database.RecipeLines.Include(rl => rl.Units)
                .Where(r => r.RecipeCardId == recipeCardId && r.MedicinTypeId == medicinTypeId).ToListAsync();

        public async Task<RecipeLine?> GetByCardAndMedicinAndLine(int recipeCardId, int medicinTypeId, int lineNumber) => await Database.RecipeLines.Include(rl => rl.Units)
                .FirstOrDefaultAsync(r => r.RecipeCardId == recipeCardId && r.MedicinTypeId == medicinTypeId && r.Position == lineNumber);

        public async Task Modify(RecipeLine recipeLine)
        {
            try
            {
                RecipeLine? original = await Database.RecipeLines.FindAsync(recipeLine.Id);
                if (original != null)
                {
                    original.MergeWith(recipeLine);
                    _ = await Database.SaveChangesAsync();
                    return;
                }

                throw new InvalidOperationException("Cannot modify a non-existing or deleted RecipeLine");
            }
            catch { throw; }
        }

        public async Task<int> Add(RecipeLine recipeLine)
        {
            try
            {
                _ = await Database.RecipeLines.AddAsync(recipeLine);
                _ = await Database.SaveChangesAsync();
                return recipeLine.Id;
            }
            catch { throw; }
        }

        public async Task Delete(int id)
        {
            try
            {
                RecipeLine? original = await Database.RecipeLines.FindAsync(id);
                if (original != null)
                {
                    _ = Database.RecipeLines.Remove(original);
                    _ = await Database.SaveChangesAsync();
                        return;
                }

                throw new InvalidOperationException("Cannot delete a non-existing RecipeLine");
            }
            catch { throw; }
        }
    }
}
