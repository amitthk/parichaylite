namespace ParichayLite.Models.Providers
{
    using AspNet.Security.OpenIdConnect.Server;
    using Microsoft.AspNet.Http;
    using Microsoft.IdentityModel.Protocols.OpenIdConnect;

    /// <summary>
    /// Provides context information used when validating a token request.
    /// </summary>
    public sealed class ValidateTokenRequestContext : BaseValidatingClientContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidateTokenRequestContext"/> class.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="options"></param>
        /// <param name="request"></param>
        internal ValidateTokenRequestContext(
            HttpContext context,
            OpenIdConnectServerOptions options,
            OpenIdConnectMessage request)
            : base(context, options, request)
        {
        }
    }
}