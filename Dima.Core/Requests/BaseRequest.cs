namespace Dima.Core.Requests;

public abstract class BaseRequest<T> where T : class
{
    public string UserId { get; set; } = default!;
}
