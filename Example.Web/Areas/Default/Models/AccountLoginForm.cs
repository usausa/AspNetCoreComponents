namespace Example.Web.Areas.Default.Models;

using System.ComponentModel.DataAnnotations;

public class AccountLoginForm
{
    [Required]
    public string UserId { get; set; } = default!;

    [Required]
    public string Password { get; set; } = default!;
}
