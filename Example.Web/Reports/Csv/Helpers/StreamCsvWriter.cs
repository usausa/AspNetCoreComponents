namespace Example.Web.Reports.Csv.Helpers;

using System.Globalization;
using System.Text;

using CsvHelper;

public sealed class StreamCsvWriter : IAsyncDisposable, IDisposable
{
    private readonly StreamWriter writer;

    public CsvWriter Writer { get; }

    public StreamCsvWriter(Stream stream, Encoding? encoding = null, bool leaveOpen = false)
    {
        writer = new StreamWriter(stream, encoding ?? new UTF8Encoding(false), bufferSize: 4096, leaveOpen: leaveOpen);

        var config = new CsvWriterOptions
        {
            HasHeader = true,
            Quoting = CsvQuoting.Always,
            Separator = ',',
            FormatProvider = CultureInfo.InvariantCulture
        };

        Writer = CsvWriter.Create(writer, config, leaveOpen: true);
    }

    public async ValueTask FlushAsync(CancellationToken cancel = default)
    {
        await Writer.FlushAsync(cancel);
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
