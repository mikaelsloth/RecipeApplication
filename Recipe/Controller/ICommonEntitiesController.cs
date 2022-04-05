namespace Recipe.Controller
{
    using Recipe.Models.Db;
    using System.Threading.Tasks;

    interface ICommonEntitiesController
    {
        Task<CommonEntity?> Get();

        Task Modify(CommonEntity commonEntity);

        Task Add(CommonEntity commonEntity);

        Task Delete();
    }
}
