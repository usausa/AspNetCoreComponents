namespace AspNetCoreComponents.QrCode.TagHelpers;

using Microsoft.AspNetCore.Razor.TagHelpers;

using QRCoder;

[HtmlTargetElement("qrcode")]
public sealed class QrCodeTagHelper : TagHelper
{
    [HtmlAttributeName("content")]
    public string Content { get; set; } = default!;

    [HtmlAttributeName("width")]
    public int Width { get; set; }

    [HtmlAttributeName("height")]
    public int Height { get; set; }

    [HtmlAttributeName("pixel")]
    public int Pixel { get; set; } = 20;

    [HtmlAttributeName("alt")]
    public string Alt { get; set; } = default!;

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        using var generator = new QRCodeGenerator();
        using var data = generator.CreateQrCode(Content, QRCodeGenerator.ECCLevel.Q);
        using var png = new PngByteQRCode(data);
        var bytes = png.GetGraphic(Pixel);

        output.TagName = "img";
        output.Attributes.Clear();
        output.Attributes.Add("width", Width);
        output.Attributes.Add("height", Height);
        output.Attributes.Add("src", "data:image/png;base64," + Convert.ToBase64String(bytes));
        if (!String.IsNullOrEmpty(Alt))
        {
            output.Attributes.Add("alt", Alt);
        }
    }
}
