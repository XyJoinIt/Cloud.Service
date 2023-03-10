using Cloud.Infra.Auth.HttpContextUser;
using Cloud.Infra.WebApi.AppCode;
using Cloud.Infra.WebApi.Enum;
using Cloud.Infra.WebApi.Extensions.Imp;
using Cloud.Platform.Repository.Dto.Sys.SysUserManage;
using Cloud.Platform.Repository.Service;
using Cloud.Platform.Repository.Service.Sys;
namespace Cloud.Platform.Service.Service.Sys
{
    /// <summary>
    /// 用户服务
    /// </summary>
    public class SysUserService : BasePlatformRepository<SysUser, AddSysUserDto, EditSysUserDto, SysUserPageParam>, ISysUserRepository
    {
        private readonly IValidator<AddSysUserDto> _addValidator;
        private readonly IValidator<EditSysUserDto> _editValidator;
        private readonly IRepository<SysUser> _repository;
        private readonly IObjectMapper _objectMapper;
        private readonly IEncryptionRepository _encryptionService;
        private readonly ILoginUser _loginUser;

        public SysUserService(IValidator<AddSysUserDto> addValidator,
                              IRepository<SysUser> repository,
                              IObjectMapper objectMapper,
                              IEncryptionRepository encryptionService,
                              IValidator<EditSysUserDto> editValidator,
                              ILoginUser loginUser)
             : base(repository, addValidator, editValidator, objectMapper)
        {
            _addValidator = addValidator;
            _repository = repository;
            _objectMapper = objectMapper;
            _encryptionService = encryptionService;
            _editValidator = editValidator;
            _loginUser = loginUser;
        }

        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public override async Task<AppResult> Add(AddSysUserDto input)
        {
            input.NotNull(nameof(input));
            var validator = await _addValidator.ValidateAsync(input);
            if (!validator.IsValid)
                return AppResult.Error(validator);
            var entity = _objectMapper.Map<SysUser>(input);
            entity!.SecurityStamp = Guid.NewGuid().ToString("N").ToUpper();
            entity!.Password = _encryptionService.GeneratePassword(entity.Password, entity.SecurityStamp);
            var res = await _repository.InsertAsync(entity);
            return AppResult.RetAppResult(res);

        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public override async Task<AppResult> Delete(long id)
        {
            //删除用户
            var res = await _repository.DeleteAsync(id);
            return AppResult.RetAppResult(res);
        }

        /// <summary>
        /// 编辑用户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public override async Task<AppResult> Edit(EditSysUserDto input)
        {
            input.NotNull(nameof(input));
            var validationResult = await _editValidator.ValidateAsync(input);
            if (!validationResult.IsValid)
                return AppResult.Error(validationResult);
            var user = await _repository.FindAsync(input.Id);
            _objectMapper.Map(input, user!);
            user!.Password = _encryptionService.GeneratePassword(input.Pwd ?? "123456", user.SecurityStamp);
            var result = await _repository.UpdateAsync(user!);
            return AppResult.RetAppResult(result);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public override async Task<AppResult> Page(SysUserPageParam param)
        {
            var list = await _repository.QueryAsNoTracking()
                .WhereIf(!param.account.IsNullOrEmpty(), x => x!.Account == param.account)
                .WhereIf(!param.name.IsNullOrEmpty(), x => x!.Name == param.name)
                .WhereIf(!param.nikeName.IsNullOrEmpty(), x => x!.NickName == param.nikeName)
                .ToPageAsync<SysUser, OutSysUserPageDto>(param, _objectMapper);
            return AppResult.Success(list);
        }

        /// <summary>
        /// 获取用户详情信息
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<AppResult> GetUserInfo()
        {
            var user = await _repository.FindAsync(_loginUser.Id);
            return AppResult.Success(new
            {
                roles = new[] {
                    new {
                        roleName="admin",
                        value = "admin"
                    }
                },
                userId = _loginUser.Id,
                username = _loginUser.UserName,
                realName = user!.Name,
                avatar = user.Avatar,
                desc = ""
            });
        }

        /// <summary>
        /// 修改用户状态 (开关)
        /// </summary>
        /// <returns></returns>
        public async Task<AppResult> EditUserStart()
        {
            var user = await _repository.FindAsync(_loginUser.Id);
            user!.Status = user!.Status == CommonStatus.正常 ? CommonStatus.停用 : CommonStatus.正常;
            await _repository.UpdateAsync(user);
            return AppResult.Success();
        }
    }
}
