using Cloud.Platform.Repository.Dto.Sys.SysUserManage;
using Microsoft.EntityFrameworkCore;

namespace Cloud.Platform.Service.Validators.Sys.SysUserManage
{
    public class EditSysUserValidator : AbstractValidator<EditSysUserDto>
    {
        private const string Msg = "{0}不能为空或Null！！";
        private readonly IRepository<SysUser> _repository;
        public EditSysUserValidator(IRepository<SysUser> repository)
        {
            _repository = repository;
            Validator();
        }

        /// <summary>
        /// 检查
        /// </summary>
        private void Validator()
        {
            RuleFor(x => x!.Name).NotEmpty().WithMessage(Msg.FormatWith("用户名"));
            RuleFor(x => x!.Account).NotEmpty().WithMessage(Msg.FormatWith("账户名"));
            RuleFor(x => x!.Account)
                .MustAsync(async (model, value, cox, token) => !await this.IsNameExistAsync(value, cox, token))
                .WithMessage(x => $"账户【{x!.Account}】已存在");
        }

        /// <summary>
        /// 判断用户名是否存在
        /// </summary>
        /// <returns></returns>
        private async Task<bool> IsNameExistAsync(string value, ValidationContext<EditSysUserDto> context, CancellationToken token = default)
        {
            var exist = await _repository
                .QueryAsNoTracking(x => x!.Account.Equals(value, StringComparison.OrdinalIgnoreCase))
                .CountAsync(cancellationToken: token)!;
            return exist > 1;
        }
    }
}
