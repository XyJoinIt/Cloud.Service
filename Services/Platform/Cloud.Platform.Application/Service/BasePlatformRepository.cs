using Cloud.Infra.EntityFrameworkCore.Entities;
using Cloud.Infra.EntityFrameworkCore.Entities.Realize;
using Cloud.Platform.Repository.Service;

namespace Cloud.Platform.Service.Service
{
    public class BasePlatformRepository<TEntity, TAddDto, TEditDto, TPageParam> : IBasePlatformRepository<TAddDto, TEditDto, TPageParam>
        where TEntity : FullEntity
        where TEditDto : IDtoId
    {
        private readonly IRepository<TEntity> _repository;
        private readonly IValidator<TAddDto> _addValidator;
        private readonly IValidator<TEditDto> _editValidator;
        private readonly IObjectMapper _objectMapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        public BasePlatformRepository(IRepository<TEntity> repository, IObjectMapper objectMapper)
        {
            _repository = repository;
            _objectMapper = objectMapper;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="addValidator"></param>

        public BasePlatformRepository(IRepository<TEntity> repository, IValidator<TAddDto> addValidator, IObjectMapper objectMapper)
        {
            _objectMapper = objectMapper;
            _repository = repository;
            _addValidator = addValidator;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="addValidator"></param>
        /// <param name="editValidator"></param>
        public BasePlatformRepository(IRepository<TEntity> repository, IValidator<TAddDto> addValidator, IValidator<TEditDto> editValidator, IObjectMapper objectMapper)
        {
            _repository = repository;
            _objectMapper = objectMapper;
            _addValidator = addValidator;
            _editValidator = editValidator;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public virtual async Task<AppResult> Add(TAddDto input)
        {
            input.NotNull(nameof(input));
            if (_addValidator != null)
            {
                var validator = await _addValidator.ValidateAsync(input);
                if (!validator.IsValid)
                    return AppResult.Error(validator);
            }

            var entity = _objectMapper.Map<TEntity>(input!);
            var result = await _repository.InsertAsync(entity!).ConfigureAwait(false);
            return AppResult.RetAppResult(result);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public virtual async Task<AppResult> Delete(long id)
        {
            var res = await _repository.DeleteAsync(id);
            return AppResult.RetAppResult(res);
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public virtual async Task<AppResult> Edit(TEditDto input)
        {
            input.NotNull(nameof(input));
            if (_editValidator is not null)
            {
                var validator = await _editValidator.ValidateAsync(input);
                if (!validator.IsValid)
                    return AppResult.Error(validator);
            }
            var entity = await _repository.FindAsync(input.Id).ConfigureAwait(false);
            entity = _objectMapper.Map(input, entity);
            var result = await _repository.UpdateAsync(entity!).ConfigureAwait(false);
            return AppResult.RetAppResult(result);
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>

        public virtual Task<AppResult> Page(TPageParam param)
        {
            throw new NotImplementedException();
        }
    }
}
