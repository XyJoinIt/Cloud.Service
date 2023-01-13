using Cloud.Platform.Repository.Dto.Sys.SysUser;
using Cloud.Platform.Repository.Service.Sys;

namespace Cloud.Platform.Service.Service.Sys
{
    public class SysUserService : ISysUserRepository
    {
        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task Add(AddSysUserDto input)
        {
            input.NotNull(nameof(input));
            //var validator = await _addValidator.ValidateAsync(dto);
            //if (!validator.IsValid)
            //    return AppResult.Error(validator);

            //var entity = ObjectMap.MapTo<SysUser>(dto);
            //entity.SecurityStamp = Guid.NewGuid().ToString("N").ToUpper();
            //entity.Password = _encryption.GeneratePassword(entity.Password, entity.SecurityStamp);
            //var result = await _repository.InsertAsync(entity);
            //return result > 0 ? AppResult.Success() : AppResult.Error();
            throw new NotImplementedException();
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task Delete(long id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 编辑用户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task Edit(EditSysUserDto input)
        {
            throw new NotImplementedException();
        }
    }
}
