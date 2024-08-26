using System.Text.Json.Serialization;

namespace Dima.Core.Responses;

public class PagedResponse<T>  : Response<T> where T : class
{
    [JsonConstructor]
    public PagedResponse(int currentPage, int pageSize, int totalCount, T? data, int code = Configuration.DefaultStatusCode):base(data, code)
    {
        CurrentPage = currentPage;
        PageSize = pageSize;
        TotalCount = totalCount;
    }

    public PagedResponse(int currentPage, int pageSize, int totalCount, T? data, int code = Configuration.DefaultStatusCode, string message) : base(data, code, message)
    {
        CurrentPage = currentPage;
        PageSize = pageSize;
        TotalCount = totalCount;
    }

    public int CurrentPage { get; set; }
    public int TotalPage  => (int)Math.Ceiling(TotalCount / (double)PageSize);
    public int PageSize { get; set; } = Configuration.DefaultPageSize;
    public int TotalCount { get; set; }
}
