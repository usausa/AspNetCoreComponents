namespace AspNetCoreComponents.QrCode.TagHelpers
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Runtime.InteropServices;

    using Microsoft.AspNetCore.Razor.TagHelpers;

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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:ValidatePlatformCompatibility", Justification = "Windows only")]
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

            using var bitmap = new Bitmap(data.Width, data.Height, PixelFormat.Format32bppRgb);
            using var ms = new MemoryStream();
            var bitmapData = bitmap.LockBits(
                new Rectangle(0, 0, data.Width, data.Height),
                ImageLockMode.WriteOnly,
                PixelFormat.Format32bppRgb);
            try
            {
                Marshal.Copy(data.Pixels, 0, bitmapData.Scan0, data.Pixels.Length);
            }
            finally
            {
                bitmap.UnlockBits(bitmapData);
            }

            bitmap.Save(ms, ImageFormat.Png);

            output.TagName = "img";
            output.Attributes.Clear();
            output.Attributes.Add("width", Width);
            output.Attributes.Add("height", Height);
            output.Attributes.Add("src", $"data:image/png;base64,{Convert.ToBase64String(ms.ToArray())}");
        }
    }
}
