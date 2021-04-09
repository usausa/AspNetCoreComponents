namespace Example.Web.Reports.Csv.Mappers
{
    using CsvHelper.Configuration;

    using Example.Models.View;

    public class ExampleViewCsvMap : ClassMap<ExampleView>
    {
        public ExampleViewCsvMap()
        {
            Map(m => m.No).Name("No").TypeConverterOption.Format("D5");
            Map(m => m.Name).Name("Name");
        }
    }
}
