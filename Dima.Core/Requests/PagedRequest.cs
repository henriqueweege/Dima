namespace Dima.Core.Requests;

public abstract class PagedRequest<T> : BaseRequest<T> where T : class
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = Configuration.DefaultPageSize;
}
