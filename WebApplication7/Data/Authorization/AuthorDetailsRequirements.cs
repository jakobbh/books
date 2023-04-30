using Microsoft.AspNetCore.Authorization;

namespace WebApplication7.Data.Authorization
{
    public class AuthorDetailsRequirements : IAuthorizationRequirement
    {
        public int _required;
        public AuthorDetailsRequirements(int required) 
        {
            _required = required;
        }
        public class AuthorDetailsRequirementsHandler : AuthorizationHandler<AuthorDetailsRequirements>
        {
            protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AuthorDetailsRequirements requirement)
            {
                if (!context.User.HasClaim(x => x.Type == "CustomProperty"))
                {
                    return Task.CompletedTask;
                } else
                {
                    var yearsActive = int.Parse(context.User.Claims.FirstOrDefault(x => x.Type == "CustomProperty").Value);
                    if (yearsActive > 2)
                    {
                        context.Succeed(requirement);
                    }
                    return Task.CompletedTask;
                }
            }
        }
    }
}
