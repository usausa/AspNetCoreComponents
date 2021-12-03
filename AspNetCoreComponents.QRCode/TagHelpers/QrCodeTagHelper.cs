namespace AspNetCoreComponents.QrCode.TagHelpers;

using System;
using System.Diagnostics.CodeAnalysis;

using Microsoft.AspNetCore.Razor.TagHelpers;

using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.PixelFormats;

using ZXing;
using ZXing.QrCode;

[HtmlTargetElement("qrcode")]
public sealed class QrCodeTagHelper : TagHelper
{
    [HtmlAttributeName("content")]
    [AllowNull]
    public string Content { get; set; }

    [HtmlAttributeName("width")]
    public int Width { get; set; }

    [HtmlAttributeName("height")]
    public int Height { get; set; }

    [HtmlAttributeName("alt")]
    [AllowNull]
    public string Alt { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        var writer = new BarcodeWriterPixelData
        {
            Format = BarcodeFormat.QR_CODE,
            Options = new QrCodeEncodingOptions
            {
                Width = Width,
                Height = Height
            }
        };

        var data = writer.Write(Content);

        var image = Image.LoadPixelData<Rgba32>(data.Pixels, Width, Height);

        output.TagName = "img";
        output.Attributes.Clear();
        output.Attributes.Add("width", Width);
        output.Attributes.Add("height", Height);
        output.Attributes.Add("src", image.ToBase64String(PngFormat.Instance));
        if (!String.IsNullOrEmpty(Alt))
        {
            output.Attributes.Add("alt", Alt);
        }
    }
}
