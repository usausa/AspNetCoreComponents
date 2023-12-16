namespace Example.Web.Areas.Default.Controllers;

using Example.Web.Areas.Default.Models;
using Example.Web.Reports.Csv.Helpers;
using Example.Web.Reports.Csv.Mappers;
using Example.Web.Reports.Pdf.Builders;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[AllowAnonymous]
public sealed class ExampleController : BaseDefaultController
{
    [HttpGet]
    public IActionResult Report()
    {
        return View();
    }

    [HttpGet]
    public ValueTask<IActionResult> Csv([FromServices] CsvExporter csvExporter)
    {
        return csvExporter.MakeFileResult(
            "sample.csv",
            typeof(ExampleViewCsvMap),
            QueryExampleEnumerable());
    }

    private static IEnumerable<ExampleView> QueryExampleEnumerable() =>
        Enumerable.Range(1, 10).Select(static x => new ExampleView { No = x, Name = $"Name-{x}" });

    [HttpGet]
    public IActionResult Pdf([FromServices] ExampleReportBuilder builder)
    {
        return File(builder.Build(), "application/pdf", "sample.pdf");
    }

    [HttpGet]
    public IActionResult Qr()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Qr([FromForm] ExampleQrForm form)
    {
        return View(form);
    }
}
