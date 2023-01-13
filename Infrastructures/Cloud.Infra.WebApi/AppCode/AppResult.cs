using Cloud.Infra.WebApi.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloud.Infra.WebApi.AppCode
{
    [Serializable]
    public class AppResult : IResult<object>
    {
        public string? Message { get; set; }
        public bool Succeeded => Code == (int)HttpCode.成功;
        public int Code { get; set; }
        public object? Result { get; set; }

        public static AppResult Problem(HttpCode status, string? message = default, object? data = default)
        {
            return new AppResult() { Code = (int)status, Message = message, Result = data };
        }
        public static async Task<AppResult> ProblemAsync(HttpCode status, string? message = default, object? data = default)
        {
            return await Task.FromResult(Problem(status, message, data));
        }

        #region 失败返回

        /// <summary>
        /// 失败
        /// </summary>
        /// <returns></returns>
        public static AppResult Error(string msg = "操作失败")
        {
            return Problem(HttpCode.失败, msg);
        }

        /// <summary>
        /// 验证失败
        /// </summary>
        /// <param name="validationResult"></param>
        /// <returns></returns>
        public static AppResult Error(FluentValidation.Results.ValidationResult validationResult)
        {
            var errorBuilder = new StringBuilder();
            validationResult.Errors.ForEach(o => errorBuilder.AppendLine(o.ErrorMessage));
            return Error(errorBuilder.ToString());

        }

        #endregion

        #region 成功返回
        /// <summary>
        /// 成功
        /// </summary>
        /// <returns></returns>
        public static AppResult Success()
        {
            return Problem(HttpCode.成功, "操作成功");
        }
        public static AppResult Success(object? data = default)
        {
            return Problem(HttpCode.成功, "操作成功", data);
        }
        public static AppResult Success(string msg = "操作成功", object? data = default)
        {
            return Problem(HttpCode.成功, msg, data);
        }
        #endregion

        #region 判断返回
        public static AppResult RetAppResult(bool isBool = true, object? data = default)
        {
            return isBool ? Success(data) : Error(data!.ToString()!);
        }

        public static AppResult RetAppResult(int isNum = 0, object? data = default)
        {
            return isNum > 0 ? Success(data) : Error(data!.ToString()!);
        }
        #endregion

    }
}
