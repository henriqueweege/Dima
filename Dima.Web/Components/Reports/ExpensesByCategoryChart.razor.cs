using Dima.Core.Handlers;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Dima.Web.Components.Reports
{
    public partial class ExpensesByCategoryChartComponent : ComponentBase
    {
        public List<double> Data { get; set; } = [];
        public List<string> Labels { get; set; } = [];

        [Inject]
        public IReportHandler Handler { get; set; }
        [Inject]
        public ISnackbar Snackbar { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await GetExpensesByCategoryAsync();
        }

        private async Task GetExpensesByCategoryAsync()
        {
            var res = await Handler.GetExpensesByCategoryReportAsync(new Core.Requests.Reports.GetExpensesByCategoryRequest());

            if(!res.IsSuccess || res.Data is null)
            {
                Snackbar.Add("Falha ao obeter dados do relatório", Severity.Error);
            }
            else
            {
                foreach(var item in res.Data)
                {
                    Labels.Add($"{item.Category} ({item.Expenses:C})");
                    Data.Add(Math.Abs((double)item.Expenses));
                }
            }
        }
    }
}
