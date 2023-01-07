using Microsoft.EntityFrameworkCore.Storage;

namespace Cloud.Infra.Repository.IRepositories;

public class UnitOfWork<TDbContext> : IUnitOfWork, IDisposable where TDbContext : DbContext
{
    private readonly TDbContext _dbContext;
    private bool _hasCommit;  //是否已提交
    private volatile bool disposedValue;

    public UnitOfWork(TDbContext dbContext)
    {
        _dbContext = dbContext;
        _hasCommit = false;
    }

    private IDbContextTransaction _dbContextTransaction = default!;

    /// <summary>
    /// 本地事务
    /// </summary>
    /// <param name="action"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task ExecuteWithTransactionAsync(Func<Task> action)
    {
        //https://learn.microsoft.com/en-us/dotnet/architecture/microservices/implement-resilient-applications/implement-resilient-entity-framework-core-sql-connections
        var strategy = _dbContext.Database.CreateExecutionStrategy();
        await strategy.ExecuteAsync(async () =>
        {
            using (_dbContextTransaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    await action();
                    await _dbContextTransaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await _dbContextTransaction.RollbackAsync();
                    throw new Exception(ex.Message);
                }
            }
        });
    }


    /// <summary>
    /// 开启事务
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_hasCommit || _dbContext == null || _dbContextTransaction != null)
        {
            return;
        }

        _dbContextTransaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
        _hasCommit = false;
    }

    /// <summary>
    /// 提交事务
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
    {

        if (_hasCommit || _dbContext == null || _dbContextTransaction == null)
        {
            return;
        }
        await _dbContextTransaction.CommitAsync(cancellationToken);
        _hasCommit = true;
    }

    /// <summary>
    /// 获取上下文
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public DbContext GetContext()
    {
        return _dbContext;
    }

    /// <summary>
    /// 回滚事务
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task RollbackAsync(CancellationToken cancellationToken = default)
    {
        if (_dbContext == null || _dbContextTransaction == null)
        {
            return;
        }

        await _dbContextTransaction.RollbackAsync(cancellationToken);

        _hasCommit = true;
    }

    /// <summary>
    /// 保存
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return _dbContext.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// 释放
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                _dbContext?.Dispose();
                _dbContextTransaction?.Dispose();
                _hasCommit = false;
            }

            disposedValue = true;
        }
    }
}
