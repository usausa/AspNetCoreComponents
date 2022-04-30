namespace Example.Web.Reports.Pdf.Helpers;

using PdfSharpCore.Drawing;

public static class Fonts
{
    [ThreadStatic]
    private static XFont? largeFontB;

    public static XFont LargeFontB => largeFontB ??= new XFont("Gothic", 28, XFontStyle.Bold);

    [ThreadStatic]
    private static XFont? normalFont;

    public static XFont NormalFont => normalFont ??= new XFont("Gothic", 11, XFontStyle.Regular);

    [ThreadStatic]
    private static XFont? smallFont;

    public static XFont SmallFont => smallFont ??= new XFont("Gothic", 10, XFontStyle.Regular);

    [ThreadStatic]
    private static XFont? minimumFont;

    public static XFont MinimumFont => minimumFont ??= new XFont("Gothic", 9, XFontStyle.Regular);
}
