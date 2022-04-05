namespace Recipe.Controller
{
    using Microsoft.EntityFrameworkCore;
    using Recipe.Models.Db;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class CommonEntitiesController : BaseController, ICommonEntitiesController
    {
        public CommonEntitiesController(RecipeDbContext context) : base(context)
        {
        }

        public async Task<CommonEntity?> Get() => await Database.CommonEntities.Take(1).SingleOrDefaultAsync();

        public async Task Modify(CommonEntity commonEntity)
        {
            try
            {
                CommonEntity? original = await Get();
                if (original != null)
                {
                    original.MergeWith(commonEntity);
                    _ = await Database.SaveChangesAsync();
                    return;
                }

                throw new InvalidOperationException("Cannot modify a non-existing common entity");
            }
            catch { throw; }
        }

        public async Task Add(CommonEntity commonEntity)
        {
            try
            {
                _ = await Database.CommonEntities.AddAsync(commonEntity);
                _ = await Database.SaveChangesAsync();
            }
            catch { throw; }
        }

        public async Task Delete()
        {
            try
            {
                CommonEntity? original = await Get();
                if (original != null)
                {
                    _ = Database.CommonEntities.Remove(original);
                    _ = await Database.SaveChangesAsync();
                    return;
                }

                throw new InvalidOperationException("Cannot modify a non-existing common entity");
            }
            catch { throw; }
        }
    }
}
