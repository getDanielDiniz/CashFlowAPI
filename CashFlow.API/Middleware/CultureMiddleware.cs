using System.Globalization;

namespace CashFlow.API.Middleware;

public class CultureMiddleware
{
    private readonly RequestDelegate _next;
    public CultureMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        string languageRequested =  context.Request.Headers.AcceptLanguage.FirstOrDefault() ?? string.Empty;
        List<CultureInfo> allLanguages = CultureInfo.GetCultures(CultureTypes.AllCultures).ToList();

        CultureInfo culture = new("en");

        if (string.IsNullOrEmpty(languageRequested) == false 
            && allLanguages.Exists(lang => lang.Name == languageRequested)) { 
            culture = new(languageRequested);
        }

        CultureInfo.CurrentCulture = culture;
        CultureInfo.CurrentUICulture = culture;


        await _next(context);
    } 
}
