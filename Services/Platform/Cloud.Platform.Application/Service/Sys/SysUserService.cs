using Cloud.Platform.Repository.Dto.Sys.SysUserManage;
using Cloud.Platform.Repository.Service.Sys;

namespace Cloud.Platform.Service.Service.Sys
{
    public class SysUserService : ISysUserRepository
    {
        private readonly IValidator<AddSysUserDto> _addValidator;
        private readonly IRepository<SysUser> _repository;
        private readonly IObjectMapper _objectMapper;
        private readonly EncryptionService _encryptionService;
        public SysUserService(IValidator<AddSysUserDto> addValidator, IRepository<SysUser> repository, IObjectMapper objectMapper, EncryptionService encryptionService)
        {
            _addValidator = addValidator;
            _repository = repository;
            _objectMapper = objectMapper;
            _encryptionService = encryptionService;
        }

        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<AppResult> Add(AddSysUserDto input)
        {
            input.NotNull(nameof(input));
            var validator = await _addValidator.ValidateAsync(input);
            if (!validator.IsValid)
                return AppResult.Error(validator);
            var entity = _objectMapper.Map<SysUser>(input);
            entity!.userInfo!.SecurityStamp = Guid.NewGuid().ToString("N").ToUpper();
            entity!.userInfo!.Password = _encryptionService.GeneratePassword(entity!.userInfo.Password, entity!.userInfo.SecurityStamp);
            return AppResult.RetAppResult(await _repository.InsertAsync(entity));

        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<AppResult> Delete(long id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 编辑用户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<AppResult> Edit(EditSysUserDto input)
        {
            throw new NotImplementedException();
        }
    }
}
