using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Orders;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Dima.Web.Components.Orders
{
    public partial class ConfirmPage : ComponentBase
    {

        [Parameter]
        public string Number { get; set; }

        public Order Order { get; set; }

        [Inject]
        public IOrderHandler OrderHandler { get; set; }
        
        [Inject]
        public ISnackbar Snackbar { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var res = await OrderHandler.HandleAsync(new PayOrderRequest { Id = Number });

            if (!res.IsSuccess)
            {
                Snackbar.Add(res.Message, Severity.Error);
            }
            Snackbar.Add(res.Message, Severity.Success);

        }
    }
}
