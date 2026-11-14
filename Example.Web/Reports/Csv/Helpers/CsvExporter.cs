namespace Example.Web.Reports.Csv.Helpers;

using System.Text;

using CsvHelper;

using Microsoft.AspNetCore.Mvc;

public sealed class CsvExporter
{
    private readonly Encoding encoding = Encoding.GetEncoding("Shift_JIS");

    public IActionResult MakeFileResult<T>(
        string downloadName,
        CsvRecordWriter<T> writer,
        IEnumerable<T> source) =>
        new CsvStreamResult<T>(downloadName, writer, source, encoding);
}
