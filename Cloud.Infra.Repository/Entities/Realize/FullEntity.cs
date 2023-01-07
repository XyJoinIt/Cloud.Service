using Cloud.Infra.Repository.Entities.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloud.Infra.Repository.Entities.Realize
{
    /// <summary>
    /// 含删除基础字段
    /// </summary>
    public class FullEntity : Key, IEntity, IIsCreate, IIsEdit, IIsDelete
    {
        /// <summary>
        /// 修改人
        /// </summary>
        public long? EditId { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? EditTime { get; set; }

        /// <summary>
        /// 新增人
        /// </summary>
        public long CreateId { get; set; }

        /// <summary>
        /// 新增时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 删除人
        /// </summary>
        public long? DeleteId { get; set; }

        /// <summary>
        /// 删除时间
        /// </summary>
        public DateTime? DeleteTime { get; set; }

        /// <summary>
        /// 软删除
        /// </summary>
        public bool IsDelete { get; set; } = false;
    }
}
