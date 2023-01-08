using Cloud.Platform.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloud.Platform.Model
{
    public class PlatformDbContext : DefaultDbContext<PlatformDbContext>
    {

        public PlatformDbContext(DbContextOptions<PlatformDbContext> options, ILoginUser loginUser) : base(options, loginUser,Assembly.GetExecutingAssembly())
        {

        }
    }
}
