namespace API.Authorization;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using API.Models;
using API.Helpers;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizeAttribute : Attribute, IAuthorizationFilter
{
    private readonly IList<Role> _roles;

    public AuthorizeAttribute(params Role[] roles)
    {
        _roles = roles ?? new Role[] { };
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        // skip authorization if action is decorated with [AllowAnonymous] attribute
        var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
        if (allowAnonymous)
            return;

        // authorization
        var user = (Organizacion)context.HttpContext.Items["Organizacion"];
        if (user == null)
        {
            // not logged in or role not authorized
            context.Result = new JsonResult(new { message = "El token no es válido" }) { StatusCode = StatusCodes.Status401Unauthorized };
        }
        if(_roles.Any() && !_roles.Contains(user.Role))
        {
            context.Result = new JsonResult(new { message = "Debes ser Administrador para acceder" }) { StatusCode = StatusCodes.Status401Unauthorized };

        }
    }
}

