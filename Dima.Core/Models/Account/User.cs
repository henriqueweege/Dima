namespace Dima.Core.Models.Account;

public class User
{
    public string Email { get; set; } = default!;
    public Dictionary<string, string> Claims { get; set; } = default!;
}
