using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;
using System.Security.Claims;

namespace DevIO.Api.Extensions
{
    public class CustomAuthorize 
    {
        public static bool ValidarClaimsUsuario(HttpContext context, string claimName, string ClaimValue)
        {
            return context.User.Identity.IsAuthenticated &&
                   context.User.Claims.Any(c => c.Type == claimName && c.Value.Contains(ClaimValue));
        }
    }

    public class ClaimsAuthorizeAttribute : TypeFilterAttribute
    {
        public ClaimsAuthorizeAttribute(string claimName, string ClaimValue) : base(typeof(RequitoClaimFilter))
        {
            Arguments = new object[] { new Claim(claimName, ClaimValue) };
        }
    }

    public class RequitoClaimFilter : IAuthorizationFilter 
    {
        private readonly Claim _claim;

        public RequitoClaimFilter(Claim claim)
        {
            _claim = claim;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.User.Identity.IsAuthenticated)
            {
                context.Result = new StatusCodeResult(401);
                return;
            }

            if(!CustomAuthorize.ValidarClaimsUsuario(context.HttpContext, _claim.Type, _claim.Value))
            {
                context.Result = new StatusCodeResult(403);
            }
        }
    }



}
