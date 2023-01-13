using Cloud.Platform.Repository.Dto.Sys.SysUserManage;

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
            RuleFor(x => x.userInfo.Name).NotEmpty().WithMessage(_emptyOrNullMesg.FormatWith("用户名"));
            RuleFor(x => x.userInfo.Account).NotEmpty().WithMessage(_emptyOrNullMesg.FormatWith("账户名"));
            RuleFor(x => x.userInfo.Password).NotEmpty().WithMessage(_emptyOrNullMesg.FormatWith("密码"));
        }

    }
}
