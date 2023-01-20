using Cloud.Platform.Repository.Dto.Auth;

namespace Cloud.Platform.Service.Validators.Auth;


public class AddLoginValidator : AbstractValidator<InputLoginDot>
{
    private const string Msg = "{0}不能为空或Null！！";
    public AddLoginValidator()
    {
        Validator();
    }

    /// <summary>
    /// 检查
    /// </summary>
    private void Validator()
    {
        RuleFor(x => x.Account).NotEmpty().WithMessage(Msg.FormatWith("用户名"));
        RuleFor(x => x.PassWord).NotEmpty().WithMessage(Msg.FormatWith("账户名"));
        //RuleFor(x => x.VerifText).NotEmpty().WithMessage(Msg.FormatWith("验证码"));
    }
}