namespace Example.Web.Reports.Csv.Helpers;

using System.Text;

using Microsoft.AspNetCore.Mvc;

public sealed class CsvExporter
{
    private readonly Encoding encoding = Encoding.GetEncoding("Shift_JIS");

    public IActionResult MakeFileResult<T>(
        string downloadName,
        Type mapType,
        IEnumerable<T> source) =>
        new CsvStreamResult<T>(downloadName, mapType, source, encoding);
}
