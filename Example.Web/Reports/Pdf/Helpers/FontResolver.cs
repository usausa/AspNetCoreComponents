namespace Example.Web.Reports.Pdf.Helpers;

using PdfSharpCore.Fonts;

public class FontResolver : IFontResolver
{
    private readonly string path;

    private readonly IDictionary<string, string> fontFiles;

    public string DefaultFontName { get; }

    public FontResolver(string path, string defaultFontName, IDictionary<string, string> fontFiles)
    {
        this.path = path;
        this.fontFiles = fontFiles;
        DefaultFontName = defaultFontName;
    }

    public FontResolverInfo ResolveTypeface(string familyName, bool isBold, bool isItalic)
    {
        if (fontFiles.TryGetValue(familyName, out var fileName))
        {
            return new FontResolverInfo(fileName);
        }

        return null!;
    }

    public byte[] GetFont(string faceName) => File.ReadAllBytes(Path.Combine(path, faceName));
}
