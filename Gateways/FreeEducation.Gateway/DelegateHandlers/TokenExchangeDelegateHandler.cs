using IdentityModel.Client;

namespace FreeEducation.Gateway.DelegateHandlers;

public class TokenExchangeDelegateHandler : DelegatingHandler
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    private string _accessToken;

    public TokenExchangeDelegateHandler(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }

    private async Task<string> GetToken(string requestToken)
    {
        if (!string.IsNullOrEmpty(_accessToken))
        {
            return _accessToken;
        }

        var discovery = await _httpClient.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest
        {
            Address = _configuration["IdentityServerURL"],
            Policy = new DiscoveryPolicy { RequireHttps = false }
        });
        if (discovery.IsError)
        {
            throw new Exception(discovery.Error);
        }

        TokenExchangeTokenRequest tokenRequest = new()
        {
            Address = discovery.TokenEndpoint,
            ClientId = _configuration["ClientId"],
            ClientSecret = _configuration["ClientSecret"],
            GrantType = "urn:ietf:params:oauth:grant-type:token-exchange",
            SubjectToken = requestToken,
            SubjectTokenType = "urn:ietf:params:oauth:token-type:access_token",
            Scope = "openid profile discount_fullpermission payment_fullpermission"
        };
        var tokenResponse = await _httpClient.RequestTokenExchangeTokenAsync(tokenRequest);
        if (tokenResponse.IsError)
        {
            throw new Exception(tokenResponse.Error);
        }

        _accessToken = tokenResponse.AccessToken;
        return _accessToken;
    }

    protected  override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var requestToken = request.Headers.Authorization.Parameter;
        var newToken = await GetToken(requestToken);
        request.SetBearerToken(newToken);
        return await base.SendAsync(request, cancellationToken);
    }
}