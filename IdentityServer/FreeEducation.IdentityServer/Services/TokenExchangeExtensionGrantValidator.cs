using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Validation;

namespace FreeEducation.IdentityServer.Services
{
    public class TokenExchangeExtensionGrantValidator : IExtensionGrantValidator
    {
        private readonly ITokenValidator _tokenValidator;
        
        public TokenExchangeExtensionGrantValidator(ITokenValidator tokenValidator)
        {
            _tokenValidator = tokenValidator;
        }
        
        public async Task ValidateAsync(ExtensionGrantValidationContext context)
        {
            var requestRaw = context.Request.Raw.ToString();
            var token = context.Request.Raw.Get("subject_token");
            if (string.IsNullOrEmpty(token))
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidRequest, "Missing subject_token");
                return;
            }
            var tokenValidationResult =await _tokenValidator.ValidateAccessTokenAsync(token);
            if (tokenValidationResult.IsError)
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "Invalid access token");
                return;
            }
            
            var subject = tokenValidationResult.Claims.FirstOrDefault(x => x.Type == "sub");
            
            if (subject == null)
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidRequest, "Missing subject");
                return;
            }
            
            context.Result = new GrantValidationResult(subject.Value, "access_token", tokenValidationResult.Claims);
            
            return;
        }

        public string GrantType => "urn:ietf:params:oauth:grant-type:token-exchange";
    }
}