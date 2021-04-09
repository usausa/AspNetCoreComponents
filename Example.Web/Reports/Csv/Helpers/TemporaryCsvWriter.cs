namespace Example.Web.Reports.Csv.Helpers
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Text;
    using System.Threading.Tasks;

    using CsvHelper;
    using CsvHelper.Configuration;

    public sealed class TemporaryCsvWriter : IDisposable
    {
        private readonly StreamWriter sr;

        private readonly FileStream fs;

        public CsvWriter Writer { get; }

        public string Path { get; }

        public bool DeleteOnDispose { get; set; } = true;

        public TemporaryCsvWriter(Type mapType)
        {
            Path = System.IO.Path.GetTempFileName();
            fs = new FileStream(Path, FileMode.Create);
            sr = new StreamWriter(fs, Encoding.GetEncoding("Shift_JIS"));

            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                ShouldQuote = _ => true,
                Delimiter = ",",
            };

            Writer = new CsvWriter(sr, config);
            Writer.Context.RegisterClassMap(mapType);
        }

        public void Dispose()
        {
            Writer.Dispose();
            fs.Dispose();
            sr.Dispose();

            if (DeleteOnDispose)
            {
                File.Delete(Path);
            }
        }

        public async ValueTask FlushAsync()
        {
            await Writer.FlushAsync();
            await sr.FlushAsync();
        }

        public async ValueTask CopyToAsync(Stream stream)
        {
            fs.Seek(0, SeekOrigin.Begin);
            await fs.CopyToAsync(stream);
        }
    }
}
