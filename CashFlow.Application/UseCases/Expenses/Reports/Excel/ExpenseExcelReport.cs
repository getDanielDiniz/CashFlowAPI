
using CashFlow.Communication.Types.PaymentType;
using CashFlow.Domain.Entities;
using CashFlow.Domain.ReportsResource;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Domain.Types.Payment;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;

namespace CashFlow.Application.UseCases.Expenses.Reports.Excel;
public class ExpenseExcelReport : IExpenseExcelReport
{
    private readonly IReadOnlyExpenseRepository _repository;
    public ExpenseExcelReport(IReadOnlyExpenseRepository repository)
    {
        _repository = repository;
    }

    public async Task<byte[]> Execute(DateOnly month)
    {
        List<Expense> expenses = await _repository.FilterByMonth(month);
        if (expenses.Count == 0)
            return [];

        XLWorkbook wb = new();
        wb.Style.Font.FontSize = 12;
        wb.Author = "Daniel Dinz";
        wb.Style.Font.FontName = "Roboto";

        IXLWorksheet ws = wb.AddWorksheet(month.ToString("Y"));

        SetHeader(ws);
        SetContent(ws, expenses);

        ws.Columns().AdjustToContents();

        var stream = new MemoryStream();
        wb.SaveAs(stream);

        return stream.ToArray();

    }

    private static void SetHeader(IXLWorksheet ws)
    {
        ws.Cell("B2").Value = ReportsResource.TITLE;
        ws.Cell("C2").Value = ReportsResource.DATE;
        ws.Cell("D2").Value = ReportsResource.PAYMENT_TYPE;
        ws.Cell("E2").Value = ReportsResource.AMOUNT;
        ws.Cell("F2").Value = ReportsResource.DESCRIPTION;

        ws.Cells("B2:F2").Style.Font.FontColor = XLColor.FromHtml("#FFFFFF");
        ws.Cells("B2:F2").Style.Font.Bold = true;
        ws.Cells("B2:F2").Style.Fill.BackgroundColor = XLColor.FromHtml("#630f8a");
        ws.Cells("B2:F2").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
        ws.Cell("E2").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
        
    }

    private static void SetContent(IXLWorksheet ws, List<Expense> expenses)
    {
        int rowIndex = 3;
        foreach (var expense in expenses)
        {
            ws.Cell($"B{rowIndex}").Value = expense.Name;
            ws.Cell($"C{rowIndex}").Value = expense.Date;
            ws.Cell($"D{rowIndex}").Value = expense.PaymentType.Convert();

            ws.Cell($"E{rowIndex}").Value = expense.Amount;
            ws.Cell($"E{rowIndex}").Style.NumberFormat.Format = "- $#,##0.00";

            ws.Cell($"F{rowIndex}").Value = expense.Description;

            rowIndex++;
        }
    }
}
