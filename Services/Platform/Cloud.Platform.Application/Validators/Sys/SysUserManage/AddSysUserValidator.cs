using Cloud.Platform.Repository.Dto.Sys.SysUserManage;
using Microsoft.EntityFrameworkCore;

namespace Cloud.Platform.Service.Validators.Sys.SysUserManage
{
    public class AddSysUserValidator : AbstractValidator<AddSysUserDto>
    {
        private const string Msg = "{0}不能为空或Null！！";
        private readonly IRepository<SysUser> _repository;
        public AddSysUserValidator(IRepository<SysUser> repository)
        {
            _repository = repository;
            Validator();
        }

        /// <summary>
        /// 检查
        /// </summary>
        private void Validator()
        {
            RuleFor(x => x.userInfo!.Name).NotEmpty().WithMessage(Msg.FormatWith("用户名"));
            RuleFor(x => x.userInfo!.Account).NotEmpty().WithMessage(Msg.FormatWith("账户名"));
            RuleFor(x => x.userInfo!.Password).NotEmpty().WithMessage(Msg.FormatWith("密码"));
            RuleFor(x => x.userInfo!.Account)
                .MustAsync(async (model, value, cox, token) => !await this.IsNameExistAsync(value, cox, token))
                .WithMessage(x => $"账户【{x.userInfo!.Account}】已存在");
        }

        /// <summary>
        /// 判断用户名是否存在
        /// </summary>
        /// <returns></returns>
        private async Task<bool> IsNameExistAsync(string value,ValidationContext<AddSysUserDto> context,CancellationToken token = default)
        {
            var exist = await _repository
                .QueryAsNoTracking(x => x.userInfo!.Account.Equals(value, StringComparison.OrdinalIgnoreCase))
                .FirstOrDefaultAsync(cancellationToken: token)!;
            return exist != null;
        }
    }
}
