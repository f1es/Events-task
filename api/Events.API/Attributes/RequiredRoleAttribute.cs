using Events.API.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System.IdentityModel.Tokens.Jwt;
using System.Net;

namespace Events.API.Attributes;

public class RequiredRoleAttribute : AuthorizeAttribute, IAuthorizationFilter
{
	private List<string> _roles = new List<string>();
    public RequiredRoleAttribute(string roles)
    {
        _roles = roles
			.Replace(" ", string.Empty)
			.ToLower()
			.Split(',')
			.ToList();
    }

    public RequiredRoleAttribute()
    {
        
    }

    public void OnAuthorization(AuthorizationFilterContext context)
	{
		if (!IsUserAuthorized(context))
		{
			context.Result = new Microsoft.AspNetCore.Mvc.ForbidResult();
		}
	}

	private bool IsUserAuthorized(AuthorizationFilterContext context)
	{
		var token = context.HttpContext.Request.GetAccessToken();

		if (token == null) 
			return false;

		var userRole = GetRoleFromJwtToken(token);

		if (!_roles.Contains(userRole))
			return false;

		return true;
	}

	private string GetRoleFromJwtToken(string token)
	{
		var jwt = new JwtSecurityTokenHandler().ReadJwtToken(token);
		var role = jwt.Claims.First(c => c.Type == "role").Value;

		return role;
	}
}
