using Dima.Core.Requests.Stripe;
using Dima.Core.Responses;
using Dima.Core.Responses.Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dima.Core.Handlers
{
    public interface IStripeHandler
    {
        Task<Response<string?>> HandleAsync(CreateSessionRequest request);
        Task<Response<List<StripeTransactionResponse>?>> HandleAsync(GetTransactionsByOrderNumberRequest request);
    }
}
