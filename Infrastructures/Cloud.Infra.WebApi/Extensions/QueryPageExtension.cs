using Cloud.Infra.WebApi.AppCode;
using Microsoft.EntityFrameworkCore;

namespace Cloud.Infra.WebApi.Extensions;

public static class QueryPageExtension
{
    /// <summary>
    ///  分页扩展
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="query">原数据</param>
    /// <param name="page">分页条件</param>
    /// <returns></returns>
    public static async Task<PagedList<T>> ToPageAsync<T>(this IQueryable<T> query, BasePage page)
    {
        //总数据
        var total = await query.CountAsync();
        var pageData =await query.Skip(page.pageSize * (page.pageIndex - 1)).Take(page.pageSize)
           .ToListAsync();
        return new PagedList<T>(pageData, total);
    }
}
