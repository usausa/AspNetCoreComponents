namespace Example.Web.Areas.Default.Models;

using System.ComponentModel.DataAnnotations;

public class DashboardIndexForm
{
    public bool Go { get; set; }

    [Range(1, Int32.MaxValue)]
    public int? Page { get; set; }

    public bool? Flag { get; set; }
}
