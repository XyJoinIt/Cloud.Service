using System.Linq.Expressions;

namespace Cloud.Infra.Mapper.AutoMapper;

public class AutoMapperObject : IObjectMapper
{
    private readonly IMapper _mapper;

    public AutoMapperObject(IMapper mapper) => _mapper = mapper;

    public TDestination? Map<TDestination>(object source)
    {
        if (source is null)
            return default;
        return _mapper.Map<TDestination>(source);
    }

    public TDestination? Map<TSource, TDestination>(TSource source)
    {
        if (source is null)
            return default;
        return _mapper.Map<TSource, TDestination>(source);
    }

    public TDestination? Map<TSource, TDestination>(TSource source, TDestination destination)
    {
        if (source is null)
            return default;
        return _mapper.Map(source, destination);
    }

    /// <summary>
    /// 将数据源映射为指定<typeparamref name="TOutputDto"/>的集合
    /// </summary>
    /// <param name="source">数据源</param>
    /// <param name="membersToExpand">成员展开</param>
    public IQueryable<TOutputDto> ToOutput<TOutputDto>(IQueryable source,
        params Expression<Func<TOutputDto, object>>[] membersToExpand)
    {
        return _mapper.ProjectTo<TOutputDto>(source, membersToExpand);
    }
}
