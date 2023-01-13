namespace Cloud.Platform.Model
{
    public class PlatformDbContext : DefaultDbContext<PlatformDbContext>
    {

        public PlatformDbContext(DbContextOptions<PlatformDbContext> options, ILoginUser loginUser) : base(options, loginUser,Assembly.GetExecutingAssembly())
        {

        }
    }
}
