using Cloud.Platform.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Cloud.Platform.Repository.Dto.Sys.SysUserManage;

public class BaseSysUserDto
{
    /// <summary>
    /// �˺�
    /// </summary>
    [Comment("�˺�")]
    [MaxLength(50)]
    public string Account { get; set; } = default!;

    /// <summary>
    /// ���루Ĭ��HMACSHA256���ܣ�
    /// </summary>
    [Comment("����")]
    [MaxLength(100)]
    [JsonIgnore]
    public string Password { get; set; } = default!;

    [Comment("��������")]
    public string? Pwd { get; set; }

    /// <summary>
    /// �ǳ�
    /// </summary>
    [Comment("�ǳ�")]
    [MaxLength(20)]
    public string? NickName { get; set; }

    /// <summary>
    /// ����
    /// </summary>
    [Comment("����")]
    [MaxLength(20)]
    public string? Name { get; set; }

    /// <summary>
    /// ͷ��
    /// </summary>
    [Comment("ͷ��")]
    public string? Avatar { get; set; }

    /// <summary>
    /// ����
    /// </summary>
    [Comment("����")]
    public DateTime? Birthday { get; set; }

    /// <summary>
    /// �Ա�-��_1��Ů_2
    /// </summary>
    [Comment("�Ա�-��_1��Ů_2")]
    public Gender Sex { get; set; } = Gender.����;

    /// <summary>
    /// ����
    /// </summary>
    [Comment("����")]
    [MaxLength(50)]
    public string? Email { get; set; }

    /// <summary>
    /// �ֻ�
    /// </summary>
    [Comment("�ֻ�")]
    [MaxLength(20)]
    public string? Phone { get; set; }

    /// <summary>
    /// �绰
    /// </summary>
    [Comment("�绰")]
    [MaxLength(20)]
    public string? Tel { get; set; }

    /// <summary>
    /// ����¼IP
    /// </summary>
    [Comment("����¼IP")]
    [MaxLength(20)]
    public string? LastLoginIp { get; set; }

    /// <summary>
    /// ����¼ʱ��
    /// </summary>
    [Comment("����¼ʱ��")]
    public DateTime? LastLoginTime { get; set; }

    /// <summary>
    /// ����Ա����-��������Ա_1������Ա_2����ͨ�˺�_3
    /// </summary>
    [Comment("����Ա���� -��������Ա_1������Ա_2����ͨ�˺�_3")]
    public AccessType AdminType { get; set; } = AccessType.��ͨ�˺�;

    /// <summary>
    /// ״̬-����_0��ͣ��_1��ɾ��_2
    /// </summary>
    [Comment("״̬-����_0��ͣ��_1��ɾ��_2")]
    public CommonStatus Status { get; set; } = CommonStatus.����;
}