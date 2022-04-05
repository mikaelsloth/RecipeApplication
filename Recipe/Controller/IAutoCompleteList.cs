namespace Recipe.Controller
{
    using Recipe.Models.Db;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IAutoCompleteList<T> where T : IAutoCompleteTextView
    {
        Task<IList<T>> GetAll();
    }
}
