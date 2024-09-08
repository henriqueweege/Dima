using Dima.Core.Models.Base;
using Dima.Core.Requests;
using Dima.Core.Responses;

namespace Dima.Core.Handlers;

public interface ICRUDHandler<TModel, in TCreateRequest, in TUpdateRequest, in TDeleteRequest, in TGetAllRequest,
    in TGetByIdRequest> where TModel : BaseModel
{
    Task<Response<TModel?>> Handle(TCreateRequest request);
    Task<Response<TModel?>> Handle(TUpdateRequest request);
    Task<Response<TModel?>> Handle(TDeleteRequest request);
    Task<PagedResponse<IEnumerable<TModel?>>> Handle(TGetAllRequest request);
    Task<Response<TModel>> Handle(BaseGetById<TModel> request);
}
