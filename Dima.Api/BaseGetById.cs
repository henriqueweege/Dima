

using Microsoft.AspNetCore.Mvc;

namespace Dima.Core.Requests
{
    public class BaseGetById<T> : BaseRequest<T> where T : class
    {
       [FromQuery] public long Id { get; set; }
    }
}
