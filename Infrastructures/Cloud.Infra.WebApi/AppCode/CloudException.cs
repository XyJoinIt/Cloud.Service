namespace Cloud.Infra.WebApi.AppCode;

/// <summary>
/// 安联错误信息集
/// </summary>
public class CloudException : Exception
{
    /// <summary>
    /// 错误信息Dto
    /// </summary>
    public ErrorDto Error { get; set; }
    /// <summary>
    /// ALException
    /// </summary>
    /// <param name="msg"></param>
    /// <param name="code"></param>
    public CloudException(string msg, object? code = null) : base(msg)
    {
        Error = new ErrorDto(msg, code);
        base.Data.Add("error", Error);
    }

    /// <summary>
    /// 错误信息Dto
    /// </summary>
    public class ErrorDto
    {
        /// <summary>
        /// ErrorDto
        /// </summary>
        /// <param name="message"></param>
        /// <param name="code"></param>
        public ErrorDto(string message, object? code)
        {
            Code = code;
            Message = message;
        }
        /// <summary>
        /// 错误代码
        /// </summary>
        [JsonProperty(PropertyName = "code")]
        public object? Code { get; set; }
        /// <summary>
        /// 错误信息
        /// </summary>
        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }
    }
}