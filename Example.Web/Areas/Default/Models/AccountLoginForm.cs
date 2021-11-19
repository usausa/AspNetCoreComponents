namespace Example.Web.Areas.Default.Models;

using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

public class AccountLoginForm
{
    [Required]
    [AllowNull]
    public string UserId { get; set; }

    [Required]
    [AllowNull]
    public string Password { get; set; }
}
