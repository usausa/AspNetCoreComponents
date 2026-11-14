namespace Example.Web.Reports.Csv.Writers;

using CsvHelper;

using Example.Web.Areas.Default.Models;

public sealed class ExampleViewWriter : CsvRecordWriter<ExampleView>
{
    public override int FieldCount => 2;

    public override void WriteHeader(CsvWriter writer)
    {
        writer.WriteField("No");
        writer.WriteField("Name");
    }

    public override void Write(CsvWriter writer, ExampleView record)
    {
        writer.WriteField(record.No, "D5");
        writer.WriteField(record.Name);
    }
}
