using Cloud.Infra.WebApi.Enum;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Cloud.Platform.Repository.Dto.Sys.SysUser;

public class BaseSysUserDto
{
    /// <summary>
    /// �˺�
    /// </summary>
    public string Account { get; set; } = default!;

    /// <summary>
    /// ���루Ĭ��HMACSHA256���ܣ�
    /// </summary>
    public string Password { get; set; } = default!;

    /// <summary>
    /// �ǳ�
    /// </summary>
    public string? NickName { get; set; }

    /// <summary>
    /// ����
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// ͷ��
    /// </summary>
    public string? Avatar { get; set; }

    /// <summary>
    /// ����
    /// </summary>
    public DateTime? Birthday { get; set; }

    /// <summary>
    /// �Ա�-��_1��Ů_2
    /// </summary>
    public Gender Sex { get; set; } = Gender.����;

    /// <summary>
    /// ����
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// �ֻ�
    /// </summary>
    public string? Phone { get; set; }

    /// <summary>
    /// �绰
    /// </summary>
    public string? Tel { get; set; }

    /// <summary>
    /// ����Ա����-��������Ա_1������Ա_2����ͨ�˺�_3
    /// </summary>
    public AccessType AdminType { get; set; } = AccessType.��ͨ�˺�;

    /// <summary>
    /// ״̬-����_0��ͣ��_1��ɾ��_2
    /// </summary>
    public CommonStatus Status { get; set; } = CommonStatus.����;
}