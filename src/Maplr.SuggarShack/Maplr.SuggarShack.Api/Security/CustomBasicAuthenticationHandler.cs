﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using Maplr.SuggarShack.Core;

namespace Maplr.SuggarShack.Api.Security;

public class CustomBasicAuthenticationHandler : AuthenticationHandler<CustomBasicAuthenticationSchemeOptions>
{
    public CustomBasicAuthenticationHandler(IOptionsMonitor<CustomBasicAuthenticationSchemeOptions> options,
                                                   ILoggerFactory logger,
                                                   UrlEncoder encoder,
                                                   ISystemClock clock) : base(options, logger, encoder, clock)
    {
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        // validation comes in here
        if (!Request.Headers.ContainsKey(Constants.AuthorizationHeaderName))
        {
            return AuthenticateResult.Fail("Header Not Found.");
        }

        var headerValue = Request.Headers[Constants.AuthorizationHeaderName];

        UserDto? user = null;
        try
        {
            var authHeader = AuthenticationHeaderValue.Parse(headerValue);
            var credentialBytes = Convert.FromBase64String(authHeader.Parameter);
            var credentials = Encoding.UTF8.GetString(credentialBytes).Split(new[] { ':' }, 2);
            var username = credentials[0];
            var password = credentials[1];
            //user = await _userService.Authenticate(username, password);

            //TODO: It will be returned by a _userService
            user = new()
            {
                Username = username,
                Password = password
            };
        }
        catch
        {
            return AuthenticateResult.Fail("Invalid Authorization Header");
        }

        if (user == null)
            return AuthenticateResult.Fail("Invalid Username or Password");

        var claims = new[] {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
            };
        var identity = new ClaimsIdentity(claims, Scheme.Name);
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, Scheme.Name);

        return AuthenticateResult.Success(ticket);
    }
}

