namespace Dima.Core.Responses.Stripe
{
    public class StripeTransactionResponse
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public long Amount { get; set; }
        public long AmountCaptured { get; set; }
        public string Status { get; set; }
        public bool Paid { get; set; }
        public bool Refunded { get; set; }
    }
}
