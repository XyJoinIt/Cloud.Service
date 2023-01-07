using Cloud.Platform.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloud.Platform.Model
{
    public class PlatformDbContext : DefaultDbContext
    {
        public PlatformDbContext(DbContextOptions<DefaultDbContext> options, ILoginUser loginUser) : base(options, loginUser)
        {
        }

        public DbSet<SysUser> SysUsers { get; set; }
    }
}
