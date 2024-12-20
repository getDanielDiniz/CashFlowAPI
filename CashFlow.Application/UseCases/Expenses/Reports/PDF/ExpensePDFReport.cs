
using CashFlow.Application.UseCases.Expenses.Reports.PDF.Fonts;
using CashFlow.Domain.Entities;
using CashFlow.Domain.ReportsResource;
using CashFlow.Domain.Repositories.Expenses;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using PdfSharp.Fonts;
using System.Reflection;

namespace CashFlow.Application.UseCases.Expenses.Reports.PDF;
public class ExpensePDFReport : IExpensePDFReport
{
    private readonly IReadOnlyExpenseRepository _repository;
    public const int LEFT_IDENT = 20; 
    public const int LINE_HEIGHT = 25; 

    public ExpensePDFReport(IReadOnlyExpenseRepository repository)
    {
        _repository = repository;
    }
    public async Task<byte[]> Execute(DateOnly month)
    {
        List<Expense> expenses = await _repository.FilterByMonth(month);
        if(expenses.Count == 0)
        {
            return [];
        }

        GlobalFontSettings.FontResolver = new FontResolver();
        Document document = CreateDocument(month);
        Section page = AddPage(document);

        AddHeader(page);
        
        decimal amount = expenses.Sum(x => x.Amount);
        AddSpentInSection(page,amount,month);

        foreach (var expense in expenses)
        {
            Table table = CreateTable(page);

            Row row = table.AddRow();//Primeira Linha
            row.Height = LINE_HEIGHT;
            AddTableTitle(row.Cells[0], expense.Name);
            AddAmountTitle(row.Cells[3], ReportsResource.AMOUNT);

            row = table.AddRow();//Segunda Linha
            row.Height = LINE_HEIGHT;
            row.Cells[0].Format.LeftIndent = LEFT_IDENT;
            AddExpenseInfo(row.Cells[0],expense.Date.ToString("D"));
            AddExpenseInfo(row.Cells[1],expense.Date.ToString("t"));
            //Implementar linha do enum
            AddExpenseInfo(row.Cells[3],$"-{expense.Amount} $");

            if (string.IsNullOrWhiteSpace(expense.Description) == false) {
                Row descriptionRow = table.AddRow();//Terceira Linha
                descriptionRow.Height = LINE_HEIGHT;

                AddDescription(descriptionRow.Cells[0], expense.Description);

                row.Cells[3].MergeDown = 1;
            }

            AddWhiteSpace(table);
        }

        byte[] renderedDocument = RenderDocument(document);
        return renderedDocument;
    }

    private static Document CreateDocument(DateOnly month)
    {
        Document document = new();
        document.Info.Title = month.ToString("Y");
        
        var styles = document.Styles["Normal"];
        styles!.Font.Name = FontHelper.DEFAULT_FONT;
        

        return document;
    }

    private static Section AddPage(Document document) {
        Section page = document.AddSection();
        page.PageSetup = document.DefaultPageSetup.Clone();
        
        page.PageSetup.PageFormat = PageFormat.A4;
        page.PageSetup.BottomMargin = 80;
        page.PageSetup.TopMargin = 80;
        page.PageSetup.LeftMargin = 40;
        page.PageSetup.RightMargin = 40;

        return page;
    }

    private static void AddHeader(Section page)
    {
        var header = page.AddTable();
        header.AddColumn();
        header.AddColumn("300");
        var row = header.AddRow();
        
        Assembly assembly = Assembly.GetExecutingAssembly();
        var assemblyLocation = assembly.Location;

        var path = Path.GetDirectoryName(assemblyLocation);
        var file = Path.Combine(path!, "UseCases", "Expenses", "Reports", "PDF", "logo", "logo.png");

        row.Cells[0].AddImage(file); 
        row.Cells[1].AddParagraph("Hey, Daniel Diniz!");
        row.Cells[1].Format.Font = new Font { Name = FontHelper.RALEWAY_BLACK , Size = 15};
        row.Cells[0].VerticalAlignment = VerticalAlignment.Center;
        row.Cells[1].VerticalAlignment = VerticalAlignment.Center;

        row.Format.SpaceAfter = 40;
    }

    private static void AddSpentInSection(Section page, decimal amount, DateOnly month)
    {
        string spentIn = string.Format(ReportsResource.AMOUNT_SPENT_IN, month.ToString("Y"));
        var paragraph = page.AddParagraph();

        paragraph.AddFormattedText(spentIn, new Font { Name = FontHelper.RALEWAY_REGULAR, Size = 15 });
        paragraph.AddLineBreak();
        paragraph.AddFormattedText($"{amount} $", new Font { Name = FontHelper.WORSANS_BLACK, Size = 50 });

        paragraph.Format.SpaceAfter = 40;
    }

    private static Table CreateTable(Section page)
    {
        var table = page.AddTable();

        table.AddColumn("195").Format.Alignment = ParagraphAlignment.Left;
        table.AddColumn("80").Format.Alignment = ParagraphAlignment.Center;
        table.AddColumn("120").Format.Alignment = ParagraphAlignment.Center;
        table.AddColumn("120").Format.Alignment = ParagraphAlignment.Right;

        return table;
    }

    private static void AddTableTitle(Cell cell, string expenseTitle)
    {
        cell.AddParagraph(expenseTitle);
        cell.Format.Font = new Font { Name = FontHelper.RALEWAY_BLACK, Size = 14};
        cell.MergeRight = 2;
        cell.Format.LeftIndent = LEFT_IDENT;
        cell.VerticalAlignment = VerticalAlignment.Center;
        cell.Borders.Width = 2;
        
    }

    private static void AddAmountTitle(Cell cell, string amountText)
    {
        cell.AddParagraph(amountText);
        cell.Format.Font = new Font { Name= FontHelper.RALEWAY_BLACK,Size = 14};
        cell.VerticalAlignment= VerticalAlignment.Center;
        cell.Format.Alignment = ParagraphAlignment.Center;
        cell.Borders.Width = 2;
    }

    private static void AddExpenseInfo(Cell cell, string text)
    {
        cell.AddParagraph(text);
        cell.Format.Font = new Font { Name = FontHelper.RALEWAY_REGULAR, Size = 12 };
        cell.Borders.Width = 2;
        cell.VerticalAlignment = VerticalAlignment.Center;
    }

    private static void AddDescription(Cell cell, string text)
    {
        cell.AddParagraph(text);
        cell.MergeRight= 2;
        cell.Borders.Width= 2;
        cell.Format.Font = new Font { Name = FontHelper.RALEWAY_REGULAR, Size = 10 };
        cell.Format.LeftIndent= LEFT_IDENT;
        cell.VerticalAlignment = VerticalAlignment.Center;
    }

    private static void AddWhiteSpace(Table table)
    {
        Row row = table.AddRow();
        row.Borders.SetNull();
        row.Height = LINE_HEIGHT;
    }

    private static byte[] RenderDocument(Document document) {
        var renderer = new PdfDocumentRenderer { Document = document };

        using MemoryStream stream = new();

        renderer.RenderDocument();
        renderer.PdfDocument.Save(stream);

        return stream.ToArray();
    }
}
