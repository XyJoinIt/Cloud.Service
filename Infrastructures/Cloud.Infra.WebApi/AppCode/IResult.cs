using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloud.Infra.WebApi.AppCode
{
    /// <summary>
    /// 统一返回
    /// </summary>
    public interface IResult
    {
        string? Message { get; set; }
        bool Succeeded { get; }
        int Code { get; set; }
    }

    public interface IResult<out T> : IResult
    {
        T? Result { get; }
    }
}
