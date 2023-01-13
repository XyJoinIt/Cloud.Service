using Cloud.Platform.Repository.Dto.Sys.SysUser;

namespace Cloud.Platform.Service.Validators.Sys.SysUser
{
    public class AddSysUserValidator : AbstractValidator<AddSysUserDto>
    {
        private const string _emptyOrNullMesg = "{0}不能为空或Null！！";

        public AddSysUserValidator()
        {
            Validator();
        }

        private void Validator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage(_emptyOrNullMesg.FormatWith("用户名"));
            RuleFor(x => x.Account).NotEmpty().WithMessage(_emptyOrNullMesg.FormatWith("账户名"));
            RuleFor(x => x.Password).NotEmpty().WithMessage(_emptyOrNullMesg.FormatWith("密码"));
        }

    }
}
