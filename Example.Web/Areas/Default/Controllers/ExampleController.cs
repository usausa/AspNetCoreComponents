namespace Example.Web.Areas.Default.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Example.Models.View;
    using Example.Web.Areas.Default.Models;
    using Example.Web.Reports.Csv.Helpers;
    using Example.Web.Reports.Csv.Mappers;
    using Example.Web.Reports.Pdf.Builders;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [AllowAnonymous]
    public class ExampleController : BaseDefaultController
    {
        [HttpGet]
        public IActionResult Report()
        {
            return View();
        }

        [HttpGet]
        public async ValueTask<IActionResult> Csv([FromServices] CsvExporter csvExporter)
        {
            return await csvExporter.MakeFileResult(
                "sample.csv",
                typeof(ExampleViewCsvMap),
                QueryExampleEnumerable());
        }

        private static IEnumerable<ExampleView> QueryExampleEnumerable() =>
            Enumerable.Range(1, 10).Select(x => new ExampleView { No = x, Name = $"Name-{x}" });

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
}
