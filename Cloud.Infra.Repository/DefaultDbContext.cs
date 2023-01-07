using Cloud.Infra.Auth;
using Cloud.Infra.Repository.Entities;
using Cloud.Infra.Repository.Entities.Contracts;
using Cloud.Infra.Repository.Extensions;
using System.Reflection;

namespace Cloud.Infra.Repository
{
    public class DefaultDbContext : DbContext
    {
        private readonly ILoginUser _loginUser;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        /// <param name="loginUser"></param>
        public DefaultDbContext(DbContextOptions<DefaultDbContext> options, ILoginUser loginUser) : base(options)
        {
            _loginUser = loginUser;
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
            //MapingEntityTypes(modelBuilder);
            //modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

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
        private void MapingEntityTypes(ModelBuilder modelBuilder)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var types = assembly?.GetTypes();
            //Eg:只要本身或者祖籍类继承了IEntity实体类接口都算数据库表
            var list = types?.Where(x => x.IsClass && !x.IsGenericType && !x.IsAbstract
            && x.GetInterfaces().Any(s => s.IsAssignableFrom(typeof(IEntity)))).ToList();

            if (list.Any())
            {
                list.ForEach(x =>
                {
                    if (modelBuilder.Model.FindEntityType(x) == null)
                        modelBuilder.Model.AddEntityType(x);
                });
            }
        }

        public void test()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var types = assembly?.GetTypes();
            //Eg:只要本身或者祖籍类继承了IEntity实体类接口都算数据库表
            var list = types?.Where(x => x.IsClass && !x.IsGenericType && !x.IsAbstract
            && x.GetInterfaces().Any(s => s.IsAssignableFrom(typeof(IEntity)))).ToList();

        }
    }
}
