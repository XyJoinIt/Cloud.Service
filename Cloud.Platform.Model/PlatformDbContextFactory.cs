using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloud.Platform.Model
{
    public class PlatformDbContextFactory : IDesignTimeDbContextFactory<PlatformDbContext>
    {
        public PlatformDbContext CreateDbContext(string[] args)
        {
            // IConfigurationRoot configuration = new ConfigurationBuilder()
            //                 .SetBasePath(Directory.GetCurrentDirectory())
            //                 .AddJsonFile("appsettings.json")
            //                 .Build();
            // var db = configuration.GetSection("ConnectionStrings:PlatformDb").Value;
            var dbcon =
                "server=43.143.112.229;userid=YdProject;pwd=5JQAv^#v@vH&ic@1d;database=XyPlatform;connectiontimeout=3000;port=31124;Pooling=true;Max Pool Size=300; Min Pool Size=5";
            var optionsBuilder = new DbContextOptionsBuilder<PlatformDbContext>();
            optionsBuilder.UseMySql(dbcon, new MySqlServerVersion(new Version()));

            return new PlatformDbContext(optionsBuilder.Options, null);

        }
    }
}
