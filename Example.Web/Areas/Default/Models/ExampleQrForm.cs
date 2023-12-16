namespace Example.Web.Areas.Default.Models;

using System.ComponentModel.DataAnnotations;

public sealed class ExampleQrForm
{
    [Required]
    public string Barcode { get; set; } = default!;
}
