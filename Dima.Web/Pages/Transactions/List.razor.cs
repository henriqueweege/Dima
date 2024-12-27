using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Transactions;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Dima.Core.Extensions;
using MudBlazor.Extensions;

namespace Dima.Web.Pages.Transactions
{
    public partial class ListTransactionPage : ComponentBase
    {
        public bool IsBusy { get; set; } = false;
        public List<Transaction> Transactions { get; set; } = [];
        public string SearchTerm { get; set; } = string.Empty;
        public int CurrentYear { get; set; } = DateTime.Now.Year;
        public int CurrentMonth { get; set; } = DateTime.Now.Month;

        public int[] Years { get; set; } = {
            DateTime.Now.Year,
            DateTime.Now.AddYears(-1).Year,
            DateTime.Now.AddYears(-2).Year,
            DateTime.Now.AddYears(-3).Year
        };

        [Inject]
        public ISnackbar Snackbar { get; set; }
        
        [Inject]
        public IDialogService DialogService { get; set; }
        
        [Inject]
        public ITransactionHandler Handler { get; set; }


        protected override async Task OnInitializedAsync()
        {
            await GetTransactionsAsync();
        }

        public async void OnDeleteButtonClickedAsync(long id, string title)
        {
            var shouldDelete = await DialogService.ShowMessageBox("ATENÇÃO", $"Ao prosseguir o lançamento {title} será excluído permanentemente. Deseja continuar?", yesText: "EXCLUIR", cancelText: "Cancelar");

            if (shouldDelete is true)
            {
                await OnDeleteAsync(id, title);
            }

            StateHasChanged();
        }

        public async Task OnSearchAsync()
        {
            await GetTransactionsAsync();
            StateHasChanged();
        }

        private async Task OnDeleteAsync(long id, string title)
        {
            IsBusy = true;

            try
            {
                var res =await Handler.Handle(new DeleteTransaction() { Id = id });
                if (res.IsSuccess)
                {
                    Snackbar.Add($"Lançamento {title} excluído.", Severity.Success);
                    Transactions.RemoveAll(x => x.Id == id);
                }
                else
                {
                    Snackbar.Add(res.Message, Severity.Error);
                }

            }
            catch (Exception ex)
            {
                Snackbar.Add(ex.Message, Severity.Error);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public Func<Transaction, bool> Filter => x =>
        {
            if (string.IsNullOrWhiteSpace(SearchTerm))
                return true;

            return x.Id.ToString().Contains(SearchTerm, StringComparison.OrdinalIgnoreCase) || x.Title.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase);
        };

        private async Task GetTransactionsAsync()
        {
            IsBusy = true;

            try
            {
                var res = await Handler.Handle(new GetByDateRangeTransaction() { StartDate = DateTime.Now.GetStartDay(CurrentYear, CurrentMonth), EndDate = DateTime.Now.GetEndDay(CurrentYear, CurrentMonth), PageNumber =1, PageSize = 1000}); 
                if (res.IsSuccess)
                    Transactions = res.Data?.ToList() ?? [];

            }
            catch (Exception ex)
            {
                Snackbar.Add(ex.Message, Severity.Error);
            }
            finally
            {
                IsBusy = false;
            }

        }

    }
}
