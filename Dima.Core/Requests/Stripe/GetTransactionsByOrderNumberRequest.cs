namespace Dima.Core.Requests.Stripe
{
    public class GetTransactionsByOrderNumberRequest : BaseRequest<string>
    {
        public string Number { get; set; }
    }
}
