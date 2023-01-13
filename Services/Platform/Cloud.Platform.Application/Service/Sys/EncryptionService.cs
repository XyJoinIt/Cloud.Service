using Cloud.Platform.Repository.Service.Sys;
using NETCore.Encrypt;

namespace Cloud.Platform.Service.Service.Sys
{
    /// <summary>
    /// 加密实现
    /// </summary>
    public class EncryptionService : IEncryptionRepository
    {
        /// <summary>
        /// 用户密码是否匹配
        /// </summary>
        /// <param name="passwordHash">加密后的字符串</param>
        /// <param name="securityStamp">加密盐</param>
        /// <param name="password">原字符串</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool CheckPasswordAsync(string passwordHash, string securityStamp, string password)
        {
            return EncryptProvider.HMACSHA256(password, securityStamp).Equals(passwordHash, StringComparison.InvariantCultureIgnoreCase);
        }

        /// <summary>
        /// 用户密码加密
        /// </summary>
        /// <param name="password">字符串</param>
        /// <param name="securityStamp">加密盐</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public string GeneratePassword(string password, string securityStamp)
        {
            return EncryptProvider.HMACSHA256(password, securityStamp);
        }
    }
}
