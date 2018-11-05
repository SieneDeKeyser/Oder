using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Oder.Domain;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Oder.Services
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly ILogger<BasicAuthenticationHandler> _logger;

        public BasicAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            ILogger<BasicAuthenticationHandler> nlogger)
            
            : base(options, logger, encoder, clock)
        {
            _logger = nlogger;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
            {
                _logger.LogError(" Missing Authorization Header");
                return AuthenticateResult.Fail("No AuthorizationHeader");
            }

            Administrator admin = null;

            try
            {
                var authenticationHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
                var credentialBytes = Convert.FromBase64String(authenticationHeader.Parameter);
                var credentials = Encoding.UTF8.GetString(credentialBytes).Split(":");

                var userName = credentials[0];
                var passWord = credentials[1];
                await Task.Run(() => admin = new Administrator() { Username = userName, Password = passWord });
                

            }
            catch
            {
                _logger.LogError("Invalid Authorization Header");
            }

            if (admin.Password == "AdminPassword" && admin.Username == "Admin")
            {
                var claims = new[] { new Claim(ClaimTypes.Name, admin.Username) };
                var identity = new ClaimsIdentity(claims, Scheme.Name);
                var principal = new ClaimsPrincipal(identity);
                var ticket = new AuthenticationTicket(principal, Scheme.Name);
                return AuthenticateResult.Success(ticket);
            }

            _logger.LogError("No correct username or/and password");
            return AuthenticateResult.Fail("No correct username or/and password");
        }
    }
}
