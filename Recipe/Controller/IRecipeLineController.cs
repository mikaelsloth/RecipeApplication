namespace Recipe.Controller
{
    using Recipe.Models.Db;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IRecipeLineController
    {
        Task<int> Add(RecipeLine recipeLine);
        
        Task Delete(int id);
        
        Task<RecipeLine?> Get(int id);
        
        Task<IList<RecipeLine>> GetByCard(int recipeCardId);
        
        Task<IList<RecipeLine>> GetByCardAndMedicin(int recipeCardId, int medicinTypeId);
        
        Task<RecipeLine?> GetByCardAndMedicinAndLine(int recipeCardId, int medicinTypeId, int lineNumber);
        
        Task Modify(RecipeLine recipeLine);
    }
}