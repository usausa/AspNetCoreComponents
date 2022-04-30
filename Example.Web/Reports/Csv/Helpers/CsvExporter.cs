namespace Example.Web.Reports.Csv.Helpers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Smart.AspNetCore.Mvc;

public class CsvExporter
{
    private ILogger<CsvExporter> Log { get; }

    public CsvExporter(ILogger<CsvExporter> log)
    {
        Log = log;
    }

    public async ValueTask<IActionResult> MakeFileResult<T>(
        string downloadName,
        Type mapType,
        IEnumerable<T> source)
    {
        try
        {
            using var csv = new TemporaryCsvWriter(mapType);
            await csv.Writer.WriteRecordsAsync(source);
            await csv.FlushAsync();

            csv.DeleteOnDispose = false;
            return new DeletePhysicalFileResult(csv.Path, "text/csv")
            {
                FileDownloadName = downloadName
            };
        }
        catch (Exception ex)
        {
            Log.LogError(ex, "CSV export failed.");
            throw;
        }
    }
}
