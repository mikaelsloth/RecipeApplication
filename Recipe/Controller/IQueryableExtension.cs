namespace Recipe.Controller
{
    using System;
    using System.Linq;

    internal static class IQueryableExtension
    {
        public static PagedResult<T> GetPaged<T>(this IQueryable<T> query,
                                         int page, int pageSize) where T : class
        {
            PagedResult<T> result = new()
            {
                CurrentPage = page,
                PageSize = pageSize,
                RowCount = query.Count()
            };

            double pageCount = (double)result.RowCount / pageSize;
            result.PageCount = (int)Math.Ceiling(pageCount);

            int skip = (page - 1) * pageSize;
            result.Results = query.Skip(skip).Take(pageSize).ToList();

            return result;
        }
    }
}
