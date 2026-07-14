namespace Example.Web.Reports.Csv.Helpers;

using System.Globalization;
using System.Text;

using CsvHelper;
using CsvHelper.Configuration;

public sealed class StreamCsvWriter : IAsyncDisposable, IDisposable
{
    private readonly StreamWriter writer;

    public CsvWriter Writer { get; }

    public StreamCsvWriter(Stream stream, Type mapType, Encoding? encoding = null, bool leaveOpen = false)
    {
        writer = new StreamWriter(stream, encoding ?? new UTF8Encoding(false), bufferSize: 4096, leaveOpen: leaveOpen);

        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = true,
            ShouldQuote = static _ => true,
            Delimiter = ","
        };

        Writer = new CsvWriter(writer, config, leaveOpen: true);
        Writer.Context.RegisterClassMap(mapType);
    }

    public async ValueTask FlushAsync(CancellationToken cancel = default)
    {
        await Writer.FlushAsync();
        await writer.FlushAsync(cancel);
    }

    public void Dispose()
    {
        Writer.Dispose();
        writer.Dispose();
    }

    public async ValueTask DisposeAsync()
    {
        await Writer.DisposeAsync();
        await writer.DisposeAsync();
    }
}
