using Cloud.Infra.Auth;
using Cloud.Infra.EntityFrameworkCore.Entities;
using Cloud.Infra.EntityFrameworkCore.Entities.Contracts;
using Cloud.Infra.EntityFrameworkCore.Extensions;
using System.Reflection;

namespace Cloud.Infra.EntityFrameworkCore
{
    public class DefaultDbContext<TDbContext> : DbContext where TDbContext : DbContext
    {
        private readonly ILoginUser _loginUser;
        public Assembly _assembly;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="options"></param>
        /// <param name="loginUser"></param>
        public DefaultDbContext(DbContextOptions<TDbContext> options,
                                ILoginUser loginUser,
                                Assembly assembly) : base(options)
        {
            _loginUser = loginUser;
            _assembly = assembly;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        /// <summary>
        /// 重写OnModelCreating （过滤软删除）
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            MapingEntityTypes(modelBuilder);

            //设置软删除
            var _Aess = modelBuilder.Model.GetEntityTypes()
                .Where(predicate: o => typeof(IIsDelete).IsAssignableFrom(o.ClrType));
            foreach (var entityType in _Aess)
            {
                entityType.DelQueryFileter();
            }
            base.OnModelCreating(modelBuilder);
        }

        /// <summary>
        /// 重写保存
        /// </summary>
        /// <returns></returns>
        public override int SaveChanges()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 重写保存Sync
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            this.ChangeTracker.Entries().LateStage(_loginUser);
            try
            {
                int count = await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
                return count;
            }
            catch (Exception)
            {
                throw;
            }

        }

        /// <summary>
        /// 动态获取实体表
        /// </summary>
        /// <param name="modelBuilder"></param>
        public void MapingEntityTypes(ModelBuilder modelBuilder)
        {
            //var assembly = Assembly.GetExecutingAssembly();
            if (_assembly == null) throw new Exception("model assembly is null");
            var types = _assembly?.GetTypes();
            //Eg:只要本身或者祖籍类继承了IEntity实体类接口都算数据库表
            var list = types?.Where(x => x.IsClass && !x.IsGenericType && !x.IsAbstract
            && x.GetInterfaces().Any(s => s.IsAssignableFrom(typeof(IEntity)))).ToList();

            if (list == null) return;

            if (list.Any())
            {
                list.ForEach(x =>
                {
                    if (modelBuilder.Model.FindEntityType(x) == null)
                        modelBuilder.Model.AddEntityType(x);
                });
            }
        }
    }
}
