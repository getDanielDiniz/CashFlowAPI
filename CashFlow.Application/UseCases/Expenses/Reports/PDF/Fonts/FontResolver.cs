using PdfSharp.Fonts;
using System.Reflection;

namespace CashFlow.Application.UseCases.Expenses.Reports.PDF.Fonts;
public class FontResolver : IFontResolver
{
    public byte[]? GetFont(string faceName)
    {
        Stream? stream = ReadFontFile(faceName);

        stream ??= ReadFontFile(FontHelper.DEFAULT_FONT);

        int length = (int)stream!.Length;

        byte[] data = new byte[length];

        stream.Read(data, 0, length);
        return data;
    }

    public FontResolverInfo? ResolveTypeface(string familyName, bool bold, bool italic)
    {
        return new FontResolverInfo(familyName);
    }

    private static Stream? ReadFontFile(string familyName)
    {
        var assembly = Assembly.GetExecutingAssembly();

        return assembly.GetManifestResourceStream($"CashFlow.Application.UseCases.Expenses.Reports.PDF.Fonts.{familyName}.ttf");
    }
}
