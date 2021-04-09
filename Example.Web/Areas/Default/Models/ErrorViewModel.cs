namespace Example.Web.Areas.Default.Models
{
    using System.Diagnostics.CodeAnalysis;

    public class ErrorViewModel
    {
        public int StatusCode { get; set; }

        [AllowNull]
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
