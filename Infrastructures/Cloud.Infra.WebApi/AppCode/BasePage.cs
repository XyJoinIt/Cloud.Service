using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloud.Infra.WebApi.AppCode
{
    public interface IBasePage
    {
        int pageIndex { get; set; }

        int pageSize { get; set; }

        DateTime? startTime { get; set; }

        DateTime? endTime { get; set; }
    }

    public class BasePage : IBasePage
    {
        public int pageIndex { get; set; } = 1;
        public int pageSize { get; set; } = 10;
        public DateTime? startTime { get; set; }
        public DateTime? endTime { get; set; }
    }

    /// <summary>
    /// 分页格式
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PagedList<T>
    {

        public PagedList(List<T> list, long total)
        {
            Items = list;
            Total = total;
        }

        /// <summary>
        /// 分页数据
        /// </summary>
        public IReadOnlyList<T> Items { get; set; } = Array.Empty<T>().ToList();

        /// <summary>
        /// 总数
        /// </summary>
        public long Total { get; set; }
    }
}
