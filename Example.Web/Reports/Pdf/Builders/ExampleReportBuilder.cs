namespace Example.Web.Reports.Pdf.Builders;

using Example.Web.Reports.Pdf.Helpers;

using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;

public class ExampleReportBuilder : ReportBuilderBase
{
    // Can injection service

    protected override void Build(Stream stream)
    {
        using var document = new PdfDocument();
        var page = document.AddPage();
        var gfx = XGraphics.FromPdfPage(page);

        gfx.DrawStringCenter("てすと", Fonts.LargeFontB, 0, 0, page.Width, page.Height);

        document.Save(stream);
    }
}
