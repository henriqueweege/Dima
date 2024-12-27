using Dima.Core.Enums;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Web.Pages.Orders;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;

namespace Dima.Web.Components.Orders
{
    public class OrderActionComponent : ComponentBase
    {
        [Parameter, EditorRequired]
        public Order Order { get; set; }

        [CascadingParameter]
        public DetailsPage Parent { get; set; }

        [Inject]
        public IDialogService DialogService { get; set; }

        [Inject]
        public IOrderHandler OrderHandler { get; set; }

        [Inject]
        public IStripeHandler StripeHandler { get; set; }

        [Inject]
        public ISnackbar Snackbar { get; set; }

        [Inject]
        public IJSRuntime JsRuntime { get; set; }

        public async void OnCancelButtonClicked()
        {
            var cancel = await DialogService.ShowMessageBox("ATENçÃO", "Deseja cancelar o pedido?", yesText: "SIM", noText: "NÃO");

            if (cancel is not null && (bool)cancel)
            {
                await CancelOrderAsync();
            }
        }


        public async void OnRefundButtonClicked()
        {
            var refund = await DialogService.ShowMessageBox("ATENçÃO", "Deseja estornar o pedido?", yesText: "SIM", noText: "NÃO");

            if (refund is not null && (bool)refund)
            {
                await RefundOrderAsync();
            }
        }

        private async Task RefundOrderAsync()
        {
            var res = await OrderHandler.HandleAsync(new Core.Requests.Orders.RefundOrderRequest() { Id = Order.Id });

            if (res.IsSuccess)
            {
                Parent.RefreshState(res.Data!);
            }
            else
            {
                Snackbar.Add(res.Message, Severity.Error);
            }
        }


        public async void OnPayButtonClicked()
        {
            await PayOrderAsync();

        }

        private async Task PayOrderAsync()
        {
            try
            {

                var res = await StripeHandler.HandleAsync(new Core.Requests.Stripe.CreateSessionRequest() { OrderNumber = Order.Number, OrderTotal = (long)Math.Round(Order.Total * 100, 2), ProductTitle = Order.Product.Title, ProductDescription = Order.Product.Desciption });
                if (!res.IsSuccess || res.Data is not null)
                {
                    Snackbar.Add(res.Message, Severity.Error);
                    return;
                }

                await JsRuntime.InvokeVoidAsync("checkout",Configuration.StripePublicKey, res.Data);

            }
            catch
            {
                Snackbar.Add("Não foi possível processar o pagamento", Severity.Error);
            }
        }

        private async Task CancelOrderAsync()
        {
            var res = await OrderHandler.HandleAsync(new Core.Requests.Orders.CancelOrderRequest() { Id = Order.Id });

            if (res.IsSuccess)
            {
                Parent.RefreshState(res.Data!);
            }
            else
            {
                Snackbar.Add(res.Message, Severity.Error);
            }
        }



    }
}
