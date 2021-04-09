namespace Example.Web.Areas.Default.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.Diagnostics.CodeAnalysis;

    public class ExampleQrForm
    {
        [Required]
        [AllowNull]
        public string Barcode { get; set; }
    }
}
