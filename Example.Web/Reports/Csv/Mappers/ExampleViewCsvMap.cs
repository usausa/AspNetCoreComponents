namespace Example.Web.Reports.Csv.Mappers;

using CsvHelper.Configuration;

using Example.Web.Areas.Default.Models;

public sealed class ExampleViewCsvMap : ClassMap<ExampleView>
{
    // ReSharper disable VirtualMemberCallInConstructor
    public ExampleViewCsvMap()
    {
        Map(m => m.No).Name("No").TypeConverterOption.Format("D5");
        Map(m => m.Name).Name("Name");
    }
    // ReSharper restore VirtualMemberCallInConstructor
}
