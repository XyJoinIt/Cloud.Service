using System.Linq.Expressions;
namespace Cloud.Infra.EntityFrameworkCore.IRepositories
{
    /// <summary>
    /// 仓储
    /// </summary>
    public sealed class Repository<TEntity> : IRepository<TEntity>
    where TEntity : FullEntity
    {
        /// <summary>
        /// 
        /// </summary>
        private DbSet<TEntity> DbSet { get; }

        /// <summary>
        /// 上下文
        /// </summary>
        private DbContext Context { get; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="unitOfWork"></param>
        public Repository(IUnitOfWork unitOfWork)
        {
            Context = unitOfWork.GetContext();
            DbSet = Context.Set<TEntity>();
        }

        #region 查询
        /// <summary>
        /// 查询列表 慎用
        /// </summary>
        /// <returns></returns>
        public IQueryable<TEntity> QueryList()
        {
            return DbSet.AsNoTracking();
        }

        /// <summary>
        /// 获取不跟踪数据更改（NoTracking）的查询数据源
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IQueryable<TEntity> QueryAsNoTracking(Expression<Func<TEntity, bool>>? predicate = null!)
        {
            return Query(predicate).AsNoTracking();
        }

        /// <summary>
        /// 获取跟踪数据更改（Tracking）的查询数据源
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IQueryable<TEntity> Query(Expression<Func<TEntity, bool>>? predicate)
        {
            var query = DbSet.AsQueryable();
            return predicate == null ? query : query.Where(predicate);
        }

        /// <summary>
        /// 获取跟踪数据更改（Tracking）的查询数据源，并可Include导航属性
        /// </summary>
        /// <param name="includePropertySelectors">要Include操作的属性表达式</param>
        /// <returns>符合条件的数据集</returns>
        public IQueryable<TEntity> Include(params Expression<Func<TEntity, object>>[]? includePropertySelectors)
        {
            var query = DbSet.AsQueryable();
            if (includePropertySelectors == null || includePropertySelectors.Length == 0)
            {
                return query;
            }
            return includePropertySelectors.Aggregate(query, (current, selector) => current.Include(selector));
        }

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="key"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<bool> ExistsAsync(long key, CancellationToken cancellationToken = default)
        {
            var item = await FindAsync(key, cancellationToken);
            return item != null;
        }

        //https://github.com/dotnet/efcore/issues/12012
        /// <summary>
        /// 异步查询
        /// </summary>
        /// <param name="key"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async ValueTask<TEntity?> FindAsync(long key, CancellationToken cancellationToken = default)
        {
            key.NotNull(nameof(key));
            return await DbSet.FindAsync(new object?[] { key, cancellationToken }, cancellationToken: cancellationToken)!;
        }

        /// <summary>
        /// FirstOrDefaultAsync
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            if (predicate == null)
                throw new Exception("表达式错误");
            return DbSet.FirstOrDefaultAsync(predicate, cancellationToken);
        }

        /// <summary>
        /// 异步加载实体
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="property"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task LoadPropertyAsync(TEntity entity, Expression<Func<TEntity, object>> property, CancellationToken cancellationToken = default)
        {
            entity.NotNull(nameof(entity));
            return Context.Entry(entity).Reference(property!).LoadAsync(cancellationToken);
        }
        #endregion

        #region 操作

        /// <summary>
        /// 异步新增
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="isSava"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<int> InsertAsync(TEntity entity, bool isSava = true, CancellationToken cancellationToken = default)
        {
            entity.NotNull(nameof(entity));
            await DbSet.AddAsync(entity, cancellationToken); //微软写代码的，解析一下为什么只有Add有异步方法？吊毛，哈哈
            return isSava ? (await Context.SaveChangesAsync(cancellationToken)) : 0;
        }

        /// <summary>
        /// 批量插入
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="isSava"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<int> InsertBatchAsync(IEnumerable<TEntity> entities, bool isSava = true, CancellationToken cancellationToken = default)
        {
            var fullEntities = entities as TEntity[] ?? entities.ToArray();
            fullEntities.NotNull(nameof(entities));
            await Context.AddRangeAsync(fullEntities, cancellationToken);
            return isSava ? (await Context.SaveChangesAsync(cancellationToken)) : 0;
        }

        /// <summary>
        /// 异步更新
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="isSava"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<int> UpdateAsync(TEntity entity, bool isSava = true, CancellationToken cancellationToken = default)
        {
            entity.NotNull(nameof(entity));
            Context.Update(entity);
            return isSava ? (await Context.SaveChangesAsync(cancellationToken)) : 0;
        }

        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="isSava"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<int> UpdateBatchAsync(IEnumerable<TEntity> entities, bool isSava = true, CancellationToken cancellationToken = default)
        {
            entities.NotNull(nameof(entities));
            Context.UpdateRange(entities);
            return isSava ? (await Context.SaveChangesAsync(cancellationToken)) : 0;
        }

        /// <summary>
        /// 异步删除
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="isSava"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<int> DeleteAsync(TEntity entity, bool isSava = true, CancellationToken cancellationToken = default)
        {
            entity.NotNull(nameof(entity));
            Context.Remove(entity);
            return isSava ? (await Context.SaveChangesAsync(cancellationToken)) : 0;
        }

        /// <summary>
        /// 异步条件删除
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <param name="isSava"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> whereExpression, bool isSava = true, CancellationToken cancellationToken = default)
        {
            whereExpression.NotNull(nameof(whereExpression));
            object[] list = await DbSet.Where(whereExpression).ToArrayAsync(cancellationToken: cancellationToken);
            Context.RemoveRange(list);
            return !isSava || (await Context.SaveChangesAsync(cancellationToken) > 0);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isSava"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<bool> DeleteAsync(long id, bool isSava = true, CancellationToken cancellationToken = default)
        {
            var entity = await FindAsync(id, cancellationToken);
            Context.Remove(entity);
            return isSava && (await Context.SaveChangesAsync(cancellationToken)>0);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="isSava"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<int> DeleteBatchAsync(IEnumerable<TEntity> entities, bool isSava = true, CancellationToken cancellationToken = default)
        {
            entities.NotNull(nameof(entities));
            Context.RemoveRange(entities);
            return isSava ? (await Context.SaveChangesAsync(cancellationToken)) : 0;
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<int> DeleteBatchAsync(Expression<Func<TEntity, bool>> whereExpression, CancellationToken cancellationToken = default)
        {
            whereExpression.NotNull(nameof(whereExpression));
            return  await DbSet.Where(whereExpression).ExecuteDeleteAsync(cancellationToken: cancellationToken);
        }

        #endregion
    }
}
