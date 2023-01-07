using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloud.Infra.Repository.Entities.Realize
{
    public class Key : Ikey<long>
    {
        public Key()
        {
            this.Id = Yitter.IdGenerator.YitIdHelper.NextId();
        }
        /// <summary>
        /// 主键
        /// </summary>
        public long Id { get; set; }
    }
}
