using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cloud.Infra.WebApi.Configurations;

namespace Cloud.Platform.Model
{
    public class PlatformDbContextFactory : IDesignTimeDbContextFactory<PlatformDbContext>
    {
        public PlatformDbContext CreateDbContext(string[] args)
        {
            //暂时我也想不出怎么获取web里面的配置文件 暂时就写死吧
            //IConfigurationRoot configuration = new ConfigurationBuilder()
            //                .SetBasePath(Directory.GetCurrentDirectory())
            //                .AddJsonFile("appsettings.json")
            //                .Build();
            //var dbcon = "server=43.143.112.229;userid=YdProject;pwd=5JQAv^#v@vH&ic@1d;database=CloudPlatform;connectiontimeout=3000;port=31124;Pooling=true;Max Pool Size=300; Min Pool Size=5";
            var dbcon = "server=localhost;userid=root;pwd=123456;database=CloudPlatform;connectiontimeout=3000;port=3306;Pooling=true;Max Pool Size=300; Min Pool Size=5";
            var optionsBuilder = new DbContextOptionsBuilder<PlatformDbContext>();
            optionsBuilder.UseMySql(dbcon, new MySqlServerVersion(new Version()));

            return new PlatformDbContext(optionsBuilder.Options, null);

        }
    }
}
