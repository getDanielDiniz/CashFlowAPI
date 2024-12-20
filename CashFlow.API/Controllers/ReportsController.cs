using CashFlow.Application.UseCases.Expenses.Reports.Excel;
using CashFlow.Application.UseCases.Expenses.Reports.PDF;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace CashFlow.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ReportsController : ControllerBase
{

    [HttpGet("Excel/")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetExcelReport([FromQuery]DateOnly month,
       [FromServices] IExpenseExcelReport report)
    {
        var file = await report.Execute(month);
        if (file.Length == 0)
            return NoContent();

        return File(file, MediaTypeNames.Application.Octet, "Report.xlsx" );
    }

    [HttpGet("PDF")]
    [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
    [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetPDFReport([FromQuery] DateOnly month,
        [FromServices] IExpensePDFReport pdfReport)
    {
        byte[] file = await pdfReport.Execute(month);

        if (file.Length == 0) return NoContent();

        return File(file,MediaTypeNames.Application.Pdf, "report.pdf" );
    }
}
