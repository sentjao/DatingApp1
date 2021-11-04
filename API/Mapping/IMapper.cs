using API.DTO;
using API.Entities;
namespace API.Mapping
{
    public interface IMapper<TEntity, TDtoRequest, TDtoResponse> where TEntity: EntityBase 
                                                            where TDtoRequest: DtoBase

    {
         TDtoResponse EntityToDto(TEntity entity);
         TEntity DtoToEntity(TDtoRequest dto);
    }
}