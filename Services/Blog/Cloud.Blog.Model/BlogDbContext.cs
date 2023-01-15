using Cloud.Infra.Auth.Auth;

namespace Cloud.Blog.Model;

public class BlogDbContext : DefaultDbContext<BlogDbContext>
{

    public BlogDbContext(DbContextOptions<BlogDbContext> options, ILoginUser loginUser) : base(options, loginUser,Assembly.GetExecutingAssembly())
    {

    }
}
