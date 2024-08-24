using Dima.Core.Enums;

namespace Dima.Core.Models;

public class Transaction
{
    public long Id { get; set; }
    public string Title { get; set; } = default!;
    public DateTime CreatedAt { get; } = DateTime.UtcNow;
    public DateOnly? PaidOrReceivedAt { get; set; }

    public ETransactionType Type { get; set; }
    public decimal Amount { get; set; }
    public long CategoryId { get; set; }
    public Category Category { get; set; } = default!;

    public string UserId { get; set; } = default!;
}

