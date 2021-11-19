namespace Example.Web.Reports.Pdf.Helpers;

using System.IO;

public abstract class ReportBuilderBase
{
    public byte[] Build()
    {
        using var ms = new MemoryStream();

        Build(ms);

        return ms.ToArray();
    }

    protected abstract void Build(Stream stream);
}
