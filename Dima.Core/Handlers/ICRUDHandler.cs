using Dima.Core.Requests;
using Dima.Core.Responses;

namespace Dima.Core.Handlers;

public interface ICRUDHandler<Model, CreateRequest, UpdateRequest, DeleteRequest, GetAllRequest, GetByIdRequest> where Model : class
{
    Task<Response<Model>> Create(CreateRequest request);
    Task<Response<Model>> Update(UpdateRequest request);
    Task<Response<Model>> Delete(DeleteRequest request);
    Task<PagedResponse<IEnumerable<Model>>> GetAll(GetAllRequest request);
    Task<Response<Model>> GetById(GetByIdRequest request);
}
