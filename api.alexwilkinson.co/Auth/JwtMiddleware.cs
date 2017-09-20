using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace api.alexwilkinson.co.Auth
{
    public class JwtMiddleware
    {

        private readonly RequestDelegate _next;
        private JwtIssuerOptions _options;
        private alexwilkinsonContext _db;

        public JwtMiddleware(RequestDelegate next, IOptions<JwtIssuerOptions> options)
        {
            _next = next;
            _options = options.Value;
        }
        public Task Invoke(HttpContext context, alexwilkinsonContext db)
        {
            _db = db;


            if (!context.Request.Path.Equals(_options.Path, StringComparison.Ordinal))
            {
                return _next(context);
            }
            if (!context.Request.Method.Equals("POST") || !context.Request.HasFormContentType)
            {
                context.Response.StatusCode = 400;
                return context.Response.WriteAsync("Bad Request");
            }
            return GenerateToken(context);
        }

        private async Task GenerateToken(HttpContext context)
        {
            string username = context.Request.Form["username"];
            string password = context.Request.Form["password"];


            var user = _db.Author.Where(x => x.AuthorUsername == username && x.AuthorPassword == password).FirstOrDefault();

            if (user == null)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync("Invalid username or password");
                return;
            }
            var now = DateTime.UtcNow;
            var expires = DateTime.UtcNow.AddDays(1);

            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64));




            var jwt = new JwtSecurityToken(
                issuer: _options.Issuer,
                audience: _options.Audience,
                claims: claims,
                notBefore: now,
                expires: expires,
                signingCredentials: _options.SigningCredentials);

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                user = username,
                token_expires = (((DateTimeOffset)expires).ToUnixTimeSeconds() - ((DateTimeOffset)now).ToUnixTimeSeconds())
            };

            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonConvert.SerializeObject(response, new JsonSerializerSettings { Formatting = Formatting.Indented }));
        }

    }
    public static class TokenProviderMiddlewareExtensions
    {
        public static IApplicationBuilder UseJWTTokenProviderMiddleware(this IApplicationBuilder builder, IOptions<JwtIssuerOptions> options)
        {
            return builder.UseMiddleware<JwtMiddleware>(options);
        }


    }
}
