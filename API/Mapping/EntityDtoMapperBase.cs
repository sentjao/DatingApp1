using API.DTO;
using API.Entities;

namespace API.Mapping
{
    public abstract class EntityDtoMapperBase<TEntity, TDtoRequest, TDtoResponse> : 
                                        IMapper<TEntity, TDtoRequest, TDtoResponse>
                                                where TEntity: EntityBase 
                                                where TDtoRequest: DtoBase
    {
        public abstract TDtoResponse EntityToDto(TEntity entity);
        public abstract TEntity DtoToEntity(TDtoRequest dto);
    }
}