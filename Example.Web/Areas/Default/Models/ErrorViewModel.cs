namespace Example.Web.Areas.Default.Models;

public class ErrorViewModel
{
    public int StatusCode { get; set; }

    public string RequestId { get; set; } = default!;

    public bool ShowRequestId => !String.IsNullOrEmpty(RequestId);
}
