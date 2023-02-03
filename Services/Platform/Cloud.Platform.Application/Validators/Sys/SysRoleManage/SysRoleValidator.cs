using Cloud.Platform.Repository.Dto.Sys.SysRoleManage;
using Microsoft.EntityFrameworkCore;

namespace Cloud.Platform.Service.Validators.Sys.SysRoleManage
{
    public class AddSysRoleValidator : AbstractValidator<AddSysRoleDto>
    {
        private const string Msg = "{0}不能为空或Null！！";
        private readonly IRepository<SysRole> _repository;
        public AddSysRoleValidator(IRepository<SysRole> repository)
        {
            _repository = repository;
            Validator();
        }

        /// <summary>
        /// 检查
        /// </summary>
        private void Validator()
        {
            RuleFor(x => x!.Code).NotEmpty().WithMessage(Msg.FormatWith("编码"));
            RuleFor(x => x!.Code)
                .MustAsync(async (model, value, cox, token) => !await this.IsNameExistAsync(value, cox, token))
                .WithMessage(x => $"账户【{x!.Code}】已存在");
        }

        /// <summary>
        /// 判断用户名是否存在
        /// </summary>
        /// <returns></returns>
        private async Task<bool> IsNameExistAsync(string value, ValidationContext<AddSysRoleDto> context, CancellationToken token = default)
        {
            var exist = await _repository
                .QueryAsNoTracking(x => x!.Code.Equals(value, StringComparison.OrdinalIgnoreCase))
                .FirstOrDefaultAsync(cancellationToken: token)!;
            return exist != null;
        }
    }

    public class EditSysRoleValidator : AbstractValidator<EditSysRoleDto>
    {
        private const string Msg = "{0}不能为空或Null！！";
        private readonly IRepository<SysRole> _repository;
        public EditSysRoleValidator(IRepository<SysRole> repository)
        {
            _repository = repository;
            Validator();
        }

        /// <summary>
        /// 检查
        /// </summary>
        private void Validator()
        {
            RuleFor(x => x!.Code).NotEmpty().WithMessage(Msg.FormatWith("编码"));
            RuleFor(x => x!.Code)
                .MustAsync(async (model, value, cox, token) => !await this.IsNameExistAsync(value, cox, token))
                .WithMessage(x => $"账户【{x!.Code}】已存在");
        }

        /// <summary>
        /// 判断用户名是否存在
        /// </summary>
        /// <returns></returns>
        private async Task<bool> IsNameExistAsync(string value, ValidationContext<EditSysRoleDto> context, CancellationToken token = default)
        {
            var exist = await _repository
              .QueryAsNoTracking(x => x!.Code.Equals(value, StringComparison.OrdinalIgnoreCase))
              .CountAsync(cancellationToken: token)!;
            return exist > 1;
        }
    }
}
