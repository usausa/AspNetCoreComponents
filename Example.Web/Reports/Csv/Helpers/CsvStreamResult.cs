namespace Example.Web.Reports.Csv.Helpers;

using System.Text;

using CsvHelper;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;

public sealed class CsvStreamResult<T> : IActionResult
{
    private readonly string downloadName;
    private readonly CsvRecordWriter<T> writer;
    private readonly IEnumerable<T> source;
    private readonly Encoding encoding;

    public CsvStreamResult(string downloadName, CsvRecordWriter<T> writer, IEnumerable<T> source, Encoding encoding)
    {
        this.downloadName = downloadName;
        this.writer = writer;
        this.source = source;
        this.encoding = encoding;
    }

    public async Task ExecuteResultAsync(ActionContext context)
    {
        var response = context.HttpContext.Response;
        response.ContentType = "text/csv";

        var disposition = new ContentDispositionHeaderValue("attachment");
        disposition.SetHttpFileName(downloadName);
        response.Headers.ContentDisposition = disposition.ToString();

        var cancel = context.HttpContext.RequestAborted;
        try
        {
            await using var csv = new StreamCsvWriter(response.Body, encoding, leaveOpen: true);
            await csv.Writer.WriteRecordsAsync(source, writer, cancel);
            await csv.FlushAsync(cancel);
        }
        catch (OperationCanceledException)
        {
            throw;
        }
        catch (Exception ex)
        {
            context.HttpContext.RequestServices
                .GetRequiredService<ILogger<CsvStreamResult<T>>>()
                .LogError(ex, "CSV export failed.");
            throw;
        }
    }
}
