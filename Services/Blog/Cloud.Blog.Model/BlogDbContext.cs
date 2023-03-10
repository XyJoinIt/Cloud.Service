using Cloud.Infra.Auth.HttpContextUser;

namespace Cloud.Blog.Model;

public class BlogDbContext : DefaultDbContext<BlogDbContext>
{

    public BlogDbContext(DbContextOptions<BlogDbContext> options, ILoginUser loginUser) : base(options, loginUser,Assembly.GetExecutingAssembly())
    {

    }
}
