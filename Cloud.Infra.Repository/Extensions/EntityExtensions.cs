using Cloud.Infra.Repository.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Cloud.Infra.Repository.Extensions;

/// <summary>
/// 实体扩展类
/// </summary>
public static class EntityExtensions
{
    /// <summary>
    /// 保存基础字段维护
    /// </summary>
    /// <param name="entityEntries"></param>
    /// <param name="loginUser">待扩展用户上下文更新实体用户操作信息</param>
    public static void LateStage(this IEnumerable<EntityEntry> entityEntries, ILoginUser loginUser)
    {
        foreach (var entity in entityEntries.Where
            (o =>
            o.State == EntityState.Added
            || o.State == EntityState.Modified
            || o.State == EntityState.Deleted))
        {

            var state = entity.State;
            var entity1 = entity.Entity;
            switch (state)
            {
                case EntityState.Added when entity1 is IIsCreate:
                    (entity1 as IIsCreate)!.CreateTime = DateTime.Now;
                    (entity1 as IIsCreate)!.CreateId = loginUser.Id;
                    break;
                case EntityState.Modified when entity1 is IIsEdit:
                    (entity1 as IIsEdit)!.EditTime = DateTime.Now;
                    (entity1 as IIsEdit)!.EditId = loginUser.Id;
                    break;
                case EntityState.Deleted when entity1 is IIsDelete:
                    (entity1 as IIsDelete)!.IsDelete = true;
                    (entity1 as IIsDelete)!.DeleteId = loginUser.Id;
                    (entity1 as IIsDelete)!.DeleteTime = DateTime.Now;
                    entity.State = EntityState.Modified;
                    break;
            }
        }
    }

    /// <summary>
    /// 过滤软删除
    /// </summary>
    /// <param name="entityData"></param>
    public static void DelQueryFileter(this IMutableEntityType entityData)
    {
        var methodToCall = typeof(EntityExtensions).GetMethod(nameof(GetDelFileter), BindingFlags.NonPublic | BindingFlags.Static)?.MakeGenericMethod(entityData.ClrType);
        var filter = methodToCall?.Invoke(null, new object[] { });
        entityData.SetQueryFilter((LambdaExpression)filter!);
    }

    /// <summary>
    /// 获取过滤条件
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    private static LambdaExpression GetDelFileter<T>() where T : IIsDelete
    {
        Expression<Func<T, bool>> filter = x => !x.IsDelete;
        return filter;
    }
}
