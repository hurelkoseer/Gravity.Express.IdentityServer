using IdentityServer4.Models;

namespace Gravity.Express.IdentityServer;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
        };

    public static IEnumerable<ApiScope> ApiScopes => new List<ApiScope>
    {
        new ApiScope("Gravity.Express.API", "Gravity Express API")
    };

    public static IEnumerable<ApiResource> ApiResources()
    {
        var api = new ApiResource("Gravity.Express.API", "Gravity Express API");
        api.Scopes = ApiScopes.Select(x => x.Name).ToList();

        return new List<ApiResource>
        {
            api
        };
    }


    public static IEnumerable<Client> Clients => new List<Client>
    {
        new Client
        {
            ClientId = "Gravity_Express",
            AllowedGrantTypes = GrantTypes.ClientCredentials,
            ClientSecrets = { new Secret("secret".Sha256()) },
            AllowedScopes = { "Gravity.Express.API" }
        }
    };
}