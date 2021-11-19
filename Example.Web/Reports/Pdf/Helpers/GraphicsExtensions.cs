namespace Example.Web.Reports.Pdf.Helpers;

using System;
//using System.Drawing;
//using System.Drawing.Imaging;
//using System.IO;
//using System.Runtime.InteropServices;

using PdfSharpCore.Drawing;

//using ZXing;
//using ZXing.QrCode;

public static class GraphicsExtensions
{
    [ThreadStatic]
    private static XPen? pen;

    private static XPen Pen => pen ??= new XPen(XColors.Black, 1);

    public static void DrawStringCenter(this XGraphics g, string s, XFont font, double x, double y)
    {
        g.DrawString(s, font, XBrushes.Black, x, y, XStringFormats.Center);
    }

    public static void DrawStringCenterLeft(this XGraphics g, string s, XFont font, double x, double y)
    {
        g.DrawString(s, font, XBrushes.Black, x, y, XStringFormats.CenterLeft);
    }

    public static void DrawStringTopLeft(this XGraphics g, string s, XFont font, double x, double y)
    {
        g.DrawString(s, font, XBrushes.Black, x, y, XStringFormats.TopLeft);
    }

    public static void DrawStringTopRight(this XGraphics g, string s, XFont font, double x, double y)
    {
        g.DrawString(s, font, XBrushes.Black, x, y, XStringFormats.TopRight);
    }

    public static void DrawStringTopCenter(this XGraphics g, string s, XFont font, double x, double y)
    {
        g.DrawString(s, font, XBrushes.Black, x, y, XStringFormats.TopCenter);
    }

    public static void DrawStringBottomRight(this XGraphics g, string s, XFont font, double x, double y)
    {
        g.DrawString(s, font, XBrushes.Black, x, y, XStringFormats.BottomRight);
    }

    public static void DrawStringBottomLeft(this XGraphics g, string s, XFont font, double x, double y)
    {
        g.DrawString(s, font, XBrushes.Black, x, y, XStringFormats.BottomLeft);
    }

    public static void DrawStringCenter(this XGraphics g, string s, XFont font, double x, double y, double width, double height)
    {
        g.DrawString(s, font, XBrushes.Black, new XRect(x, y, width, height), XStringFormats.Center);
    }

    public static void DrawStringCenterRight(this XGraphics g, string s, XFont font, double x, double y, double width, double height, double padding)
    {
        g.DrawString(s, font, XBrushes.Black, new XRect(x, y, width - padding, height), XStringFormats.CenterRight);
    }

    public static void DrawStringCenterLeft(this XGraphics g, string s, XFont font, double x, double y, double width, double height, double padding)
    {
        g.DrawString(s, font, XBrushes.Black, new XRect(x + padding, y, width - padding, height), XStringFormats.CenterLeft);
    }

    public static void DrawStringBottomCenter(this XGraphics g, string s, XFont font, double x, double y, double width, double height, double padding)
    {
        g.DrawString(s, font, XBrushes.Black, new XRect(x + padding, y, width - padding, height), XStringFormats.BottomCenter);
    }

    public static void DrawLine(this XGraphics g, double x1, double y1, double x2, double y2)
    {
        g.DrawLine(Pen, x1, y1, x2, y2);
    }

    public static void DrawRectangle(this XGraphics g, double x, double y, double width, double height)
    {
        g.DrawRectangle(Pen, x, y, width, height);
    }

    //public static void DrawQRImage(this XGraphics g, double x, double y, int size, string text)
    //{
    //    var writer = new BarcodeWriterPixelData
    //    {
    //        Format = BarcodeFormat.QR_CODE,
    //        Options = new QrCodeEncodingOptions
    //        {
    //            ErrorCorrection = ZXing.QrCode.Internal.ErrorCorrectionLevel.M,
    //            CharacterSet = "UTF-8",
    //            Width = (int)(size / 72.0 * 96),
    //            Height = (int)(size / 72.0 * 96),
    //            Margin = 0,
    //        }
    //    };

    //    var data = writer.Write(text);

    //    using var bitmap = new Bitmap(data.Width, data.Height, PixelFormat.Format32bppRgb);
    //    var bitmapData = bitmap.LockBits(
    //        new Rectangle(0, 0, data.Width, data.Height),
    //        ImageLockMode.WriteOnly,
    //        PixelFormat.Format32bppRgb);
    //    try
    //    {
    //        Marshal.Copy(data.Pixels, 0, bitmapData.Scan0, data.Pixels.Length);
    //    }
    //    finally
    //    {
    //        bitmap.UnlockBits(bitmapData);
    //    }

    //    using var ms = new MemoryStream();
    //    bitmap.Save(ms, ImageFormat.Bmp);
    //    ms.Seek(0, SeekOrigin.Begin);

    //    var image = XImage.FromStream(() => ms);
    //    g.DrawImage(image, x, y);
    //}
}
