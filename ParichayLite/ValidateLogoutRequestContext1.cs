namespace ParichayLite.Models.Providers
{
    using AspNet.Security.OpenIdConnect.Server;
    using Microsoft.AspNet.Http;
    using Microsoft.IdentityModel.Protocols.OpenIdConnect;
    using System;
    public sealed class ValidateLogoutRequestContext : BaseValidatingContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidateLogoutRequestContext"/> class.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="options"></param>
        /// <param name="request"></param>
        internal ValidateLogoutRequestContext(
            HttpContext context,
            OpenIdConnectServerOptions options,
            OpenIdConnectMessage request)
            : base(context, options)
        {
            Request = request;

            // Note: if the optional post_logout_redirect_uri parameter
            // is missing, mark the validation context as skipped.
            // See http://openid.net/specs/openid-connect-session-1_0.html#RPLogout
            if (string.IsNullOrEmpty(request.PostLogoutRedirectUri))
            {
                Skip();
            }
        }

        /// <summary>
        /// Gets the authorization request.
        /// </summary>
        public new OpenIdConnectMessage Request { get; }

        /// <summary>
        /// Gets the post logout redirect URI.
        /// </summary>
        public string PostLogoutRedirectUri
        {
            get { return Request.PostLogoutRedirectUri; }
            set { Request.PostLogoutRedirectUri = value; }
        }

        /// <summary>
        /// Marks this context as validated by the application.
        /// IsValidated becomes true and HasError becomes false as a result of calling.
        /// </summary>
        /// <returns></returns>
        public override bool Validated()
        {
            if (string.IsNullOrEmpty(PostLogoutRedirectUri))
            {
                // Don't allow default validation when
                // redirect_uri not provided with request.
                return false;
            }

            return base.Validated();
        }

        /// <summary>
        /// Checks the redirect URI to determine whether it equals <see cref="PostLogoutRedirectUri"/>.
        /// </summary>
        /// <param name="redirectUri"></param>
        /// <returns></returns>
        public bool Validate(string redirectUri)
        {
            if (redirectUri == null)
            {
                throw new ArgumentNullException("redirectUri");
            }

            if (!string.IsNullOrEmpty(PostLogoutRedirectUri) &&
                !string.Equals(PostLogoutRedirectUri, redirectUri, StringComparison.Ordinal))
            {
                // Don't allow validation to alter redirect_uri provided with request
                return false;
            }

            PostLogoutRedirectUri = redirectUri;

            return Validated();
        }

        /// <summary>
        /// Resets post_logout_redirect_uri and marks
        /// the context as skipped by the application.
        /// </summary>
        public bool Skip()
        {
            // Reset post_logout_redirect_uri if validation was skipped.
            PostLogoutRedirectUri = null;

            return base.Skipped();
        }

        /// <summary>
        /// Resets post_logout_redirect_uri and marks
        /// the context as rejected by the application.
        /// </summary>
        public bool Reject()
        {
            // Reset post_logout_redirect_uri if validation failed.
            PostLogoutRedirectUri = null;

            return base.Rejected();
        }
    }
}