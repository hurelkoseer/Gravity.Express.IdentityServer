using Gravity.Express.IdentityServer.Model;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Identity;

namespace Gravity.Express.IdentityServer.Validator;

public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
{
    private readonly UserManager<IdentityUser> _userManager;

    public ResourceOwnerPasswordValidator(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
    {
        var user = await _userManager.FindByNameAsync(context.UserName);
        if (user != null && await _userManager.CheckPasswordAsync(user, context.Password))
        {
            context.Result = new GrantValidationResult(user.Id, "password", null, "local", null);
        }
        else
        {
            context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "Invalid username or password");
        }
    }
}
