using Dima.Core.Handlers;
using Dima.Core.Models.Reports;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Dima.Web.Pages
{
    public partial class HomePage : ComponentBase
    {
        public bool ShowValues { get; set; } = true;
        public FinancialSummary? Summary { get; set; }

        [Inject]
        public ISnackbar Snackbar { get; set; }
        [Inject]
        public IReportHandler Handler { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var res = await Handler.GetFinancialSummaryReportAsync(new());
            if (res.IsSuccess)
            {
                Summary = res.Data;
            }
        }

        public void ToggleShowValues()
            => ShowValues =  !ShowValues;
    }
}
