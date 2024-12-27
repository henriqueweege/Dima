using Dima.Core.Handlers;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Reflection.Emit;

namespace Dima.Web.Components.Reports
{
    public partial class IncomeAndExpenseComponent : ComponentBase
    {
        public ChartOptions Options { get; set; } = new();
        public List<ChartSeries>? Series { get; set; } = [];
        public List<string> Labels { get; set; } = [];

        [Inject]
        public IReportHandler   Handler { get; set; }
        
        [Inject]
        public ISnackbar Snackbar { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var res =  await Handler.GetIncomesAndExpensesReportAsync(new());

            if (!res.IsSuccess || res.Data is null)
            {
                Snackbar.Add("Falha ao obeter dados do relatório", Severity.Error);
            }
            else
            {
                var incomes = new List<double>();
                var expenses = new List<double>();

                foreach(var item in res.Data)
                {
                    incomes.Add((double)item.Incomes);
                    expenses.Add((double)Math.Abs(item.Expenses));
                    Labels.Add(GetMonthName(item.Month));
                }

                Options.YAxisTicks = 1000;
                Options.LineStrokeWidth = 5;
                Options.ChartPalette = ["#76FF01", Colors.Red.Default];

                Series = [
                    new ChartSeries{Name = "Receitas", Data = incomes.ToArray()},
                    new ChartSeries{Name = "Saídas", Data = expenses.ToArray()},
                    ];
            }

            StateHasChanged();
        }

        private static string GetMonthName(int month)
        {
            return new DateTime(DateTime.Now.Year, month, 1).ToString("MMMM");
        }
    }
}
