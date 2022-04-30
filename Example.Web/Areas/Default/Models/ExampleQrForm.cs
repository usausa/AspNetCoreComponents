namespace Example.Web.Areas.Default.Models;

using System.ComponentModel.DataAnnotations;

public class ExampleQrForm
{
    [Required]
    public string Barcode { get; set; } = default!;
}
