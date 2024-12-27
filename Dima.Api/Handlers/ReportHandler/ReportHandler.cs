using Dima.Api.Data;
using Dima.Api.Extensions;
using Dima.Core.Handlers;
using Dima.Core.Models.Reports;
using Dima.Core.Requests.Reports;
using Dima.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace Dima.Api.Handlers.ReportHandler
{
    public class ReportHandler(AppDbContext context) : IReportHandler
    {
        public async Task<Response<List<ExpensesByCategory>?>> GetExpensesByCategoryReportAsync(GetExpensesByCategoryRequest request)
        {
            try
            {
                var data = await context.ExpensesByCategories.AsNoTracking().Where(x => x.UserId == request.UserId).OrderByDescending(x => x.Year).ThenBy(x => x.Category).ToListAsync();
                return new Response<List<ExpensesByCategory>?>(data);
            }
            catch
            {
                return new Response<List<ExpensesByCategory>?>(null, 500, "Não foi possível obter as saidas por categoria");
            }
        }

        public async Task<Response<FinancialSummary?>> GetFinancialSummaryReportAsync(GetFinancialSummaryRequest request)
        {
            var startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1); try
            {

                var query = await context.Transactions.AsNoTracking().Where(x => x.UserId == request.UserId && x.PaidOrReceivedAt >= startDate.ToDateOnly() && x.PaidOrReceivedAt <= DateTime.Now.ToDateOnly()).GroupBy(x => true).Select(x => new FinancialSummary(request.UserId, x.Where(y=>y.Type == Core.Enums.ETransactionType.Deposit).Sum(y=>y.Amount), x.Where(y => y.Type == Core.Enums.ETransactionType.Deposit).Sum(y => y.Amount))).FirstOrDefaultAsync();
                return new Response<FinancialSummary>(query);
            }
            catch (Exception)
            {

                return new Response<FinancialSummary>(null, 500, "Não foi possível obter as entradas e saídas");

            }

        }

        public async Task<Response<List<IncomesAndExpenses>?>> GetIncomesAndExpensesReportAsync(GetIncomesAndExpensesRequest request)
        {
            try
            {
                var data = await context.IncomesAndExpenses.AsNoTracking().Where(x=>x.UserId == request.UserId).OrderByDescending(x=>x.Year).ThenBy(x=>x.Month).ToListAsync();
                return new Response<List<IncomesAndExpenses>?>(data);
            }
            catch 
            {

                return new Response<List<IncomesAndExpenses>?>(null, 500, "Não foi possível obter as entradas e saídas");

            }

        }

        public async Task<Response<List<IncomesByCategory>?>> GetIncomesByCategoryReportAsync(GetIncomesByCategoryRequest request)
        {
            try
            {
                var data = await context.IncomesByCategories.AsNoTracking().Where(x => x.UserId == request.UserId).OrderByDescending(x => x.Year).ThenBy(x => x.Category).ToListAsync();
                return new Response<List<IncomesByCategory>?>(data);
            }
            catch
            {

                return new Response<List<IncomesByCategory>?>(null, 500, "Não foi possível obter as entradas por categoria");

            }
        }
    }
}
