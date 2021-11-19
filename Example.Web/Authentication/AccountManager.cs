namespace Example.Web.Authentication;

using System.Security.Claims;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

public class AccountManager
{
    private IHttpContextAccessor HttpContextAccessor { get; }

    public AccountManager(IHttpContextAccessor httpContextAccessor)
    {
        HttpContextAccessor = httpContextAccessor;
    }

    public async ValueTask<bool> LoginAsync(string userId, string password)
    {
        // TODO
        if (userId != password)
        {
            return false;
        }

        var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
        // TODO claims & role
        identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, userId));
        identity.AddClaim(new Claim(ClaimTypes.Name, userId));
        if (userId.Length > 3)
        {
            identity.AddClaim(new Claim(ClaimTypes.Role, Role.Admin));
        }

        await HttpContextAccessor.HttpContext!.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(identity),
            new AuthenticationProperties { AllowRefresh = true, IsPersistent = true });

        return true;
    }

    public async ValueTask LogoutAsync() =>
        await HttpContextAccessor.HttpContext!.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
}
