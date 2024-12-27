using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Orders;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Dima.Web.Pages.Orders
{
    public partial class DetailsPage : ComponentBase
    {
        [Parameter]
        public string Number { get; set; }
        public Order Order { get; set; }

        [Inject]
        public ISnackbar Snackbar { get; set; }

        [Inject]
        public IOrderHandler OrderHandler { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var res = await OrderHandler.HandleAsync(new GetOrderByNumberRequest() { Number = Number });

            if (res.IsSuccess)
                Order = res.Data!;
            else
                Snackbar.Add(res.Message, Severity.Error);
        }

        public void RefreshState(Order order)
        {
            Order = order;
            StateHasChanged();
        }
    }
}
