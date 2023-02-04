using Cloud.Infra.WebApi.AppCode;
using Microsoft.EntityFrameworkCore;

namespace Cloud.Infra.WebApi.Extensions.Imp;

public static class QueryPageExtension
{
    /// <summary>
    ///  分页扩展
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="query">原数据</param>
    /// <param name="page">分页条件</param>
    /// <returns></returns>
    public static async Task<PagedList<OutDto>> ToPageAsync<T, OutDto>(this IQueryable<T> query, BasePage page, IObjectMapper mapper)
    {
        //总数据
        var total = await query.CountAsync();

        var pageData = await mapper.ToOutput<OutDto>(query
             .Skip(page.pageSize * (page.pageIndex - 1))
             .Take(page.pageSize)
            ).ToListAsync();

        return new PagedList<OutDto>(pageData, total);
    }
}
