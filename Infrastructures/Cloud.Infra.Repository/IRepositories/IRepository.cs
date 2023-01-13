using Cloud.Infra.Repository.Entities.Realize;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Cloud.Infra.Repository.IRepositories
{
    public interface IRepository<TEntity>
    where TEntity : FullEntity
    {
        /// <summary>
        /// 异步查找
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        ValueTask<TEntity> FindAsync(long key, CancellationToken cancellationToken = default);

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> ExistsAsync(long key, CancellationToken cancellationToken = default);

        /// <summary>
        /// 获取不跟踪数据更改（NoTracking）的查询数据源
        /// </summary>
        /// <param name="predicate">数据查询谓语表达式</param>
        /// <returns></returns>
        IQueryable<TEntity> QueryAsNoTracking(Expression<Func<TEntity, bool>> predicate = null!);

        /// <summary>
        /// 获取跟踪数据更改（Tracking）的查询数据源
        /// </summary>
        /// <param name="predicate">数据过滤表达式</param>
        /// <returns></returns>
        IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> predicate = null!);

        /// <summary>
        /// 获取跟踪数据更改（Tracking）的查询数据源，并可Include导航属性
        /// </summary>
        /// <param name="includePropertySelectors">要Include操作的属性表达式</param>
        /// <returns>符合条件的数据集</returns>
        IQueryable<TEntity> Include(params Expression<Func<TEntity, object>>[] includePropertySelectors);

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <returns></returns>
        IQueryable<TEntity> QueryList();

        /// <summary>
        /// 异步加载实体
        /// </summary>
        /// <param name="item"></param>
        /// <param name="property"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task LoadPropertyAsync(TEntity item, Expression<Func<TEntity, object>> property, CancellationToken cancellationToken = default);

        /// <summary>
        /// 异步添加
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        Task<int> InsertAsync(TEntity item, bool IsSava = true, CancellationToken cancellationToken = default);

        /// <summary>
        /// 批量插入
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<int> InsertBatchAsync(IEnumerable<TEntity> entities, bool IsSava = true, CancellationToken cancellationToken = default);

        /// <summary>
        /// 异步更新
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        Task<int> UpdateAsync(TEntity item, bool IsSava = true, CancellationToken cancellationToken = default);

        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<int> UpdateBatchAsync(IEnumerable<TEntity> entities, bool IsSava = true, CancellationToken cancellationToken = default);

        /// <summary>
        /// 异步删除
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<int> DeleteAsync(TEntity entity, bool IsSava = true, CancellationToken cancellationToken = default);

        /// <summary>
        /// 异步条件删除
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <returns></returns>
        Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> whereExpression, bool IsSava = true, CancellationToken cancellationToken = default);

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<int> DeleteBatchAsync(IEnumerable<TEntity> entities, bool IsSava = true, CancellationToken cancellationToken = default);

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<int> DeleteBatchAsync(Expression<Func<TEntity, bool>> whereExpression, CancellationToken cancellationToken = default);

        /// <summary>
        /// 查询第一个
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
    }
}
