using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloud.Blog.Model
{
    public class BlogDbContextFactory : IDesignTimeDbContextFactory<BlogDbContext>
    {
        public BlogDbContext CreateDbContext(string[] args)
        {
            //暂时我也想不出怎么获取web里面的配置文件 暂时就写死吧
            //IConfigurationRoot configuration = new ConfigurationBuilder()
            //                .SetBasePath(Directory.GetCurrentDirectory())
            //                .AddJsonFile("appsettings.json")
            //                .Build();
            var dbcon =
                "server=43.143.112.229;userid=YdProject;pwd=5JQAv^#v@vH&ic@1d;database=CloudBlog;connectiontimeout=3000;port=31124;Pooling=true;Max Pool Size=300; Min Pool Size=5";
            var optionsBuilder = new DbContextOptionsBuilder<BlogDbContext>();
            optionsBuilder.UseMySql(dbcon, new MySqlServerVersion(new Version()));

            return new BlogDbContext(optionsBuilder.Options, null);

        }
    }
}
