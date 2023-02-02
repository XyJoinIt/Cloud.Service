using System.Linq.Expressions;

namespace Cloud.Infra.Mapper;

public interface IObjectMapper
{
    /// <summary>
    /// 映射
    /// </summary>
    /// <typeparam name="TDestination">目标</typeparam>
    /// <param name="source"></param>
    /// <returns></returns>
    TDestination? Map<TDestination>(object source);

    /// <summary>
    /// 映射
    /// </summary>
    /// <typeparam name="TSource">数据源</typeparam>
    /// <typeparam name="TDestination">目标</typeparam>
    /// <param name="source"></param>
    /// <returns></returns>
    TDestination? Map<TSource, TDestination>(TSource source);

    /// <summary>
    /// 映射
    /// </summary>
    /// <typeparam name="TSource">数据源</typeparam>
    /// <typeparam name="TDestination">目标</typeparam>
    /// <param name="source"></param>
    /// <param name="destination"></param>
    /// <returns></returns>
    TDestination? Map<TSource, TDestination>(TSource source, TDestination destination);

    /// <summary>
    /// 将数据源映射为指定<typeparamref name="TOutputDto"/>的集合
    /// </summary>
    /// <typeparam name="TOutputDto"></typeparam>
    /// <param name="source"></param>
    /// <param name="membersToExpand"></param>
    /// <returns></returns>
    IQueryable<TOutputDto> ToOutput<TOutputDto>(IQueryable source,
           params Expression<Func<TOutputDto, object>>[] membersToExpand);
}
