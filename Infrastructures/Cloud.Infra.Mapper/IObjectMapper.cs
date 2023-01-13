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
}
