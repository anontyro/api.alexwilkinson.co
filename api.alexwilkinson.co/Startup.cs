using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using api.alexwilkinson.co.Auth;

namespace api.alexwilkinson.co
{
    public class Startup
    {

        private static SymmetricSecurityKey signingKey;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Connection._secretKey));
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddDbContextPool<alexwilkinsonContext>(options =>
                options.UseMySql(Connection._defaultConnection));

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options=> {
                options.TokenValidationParameters = getTokenParameters();
            });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {

            app.UseAuthentication();

            app.UseJWTTokenProviderMiddleware(Options.Create(setJwtOptions()));

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }

        private TokenValidationParameters getTokenParameters()
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,

                ValidateIssuer = true,
                ValidIssuer = Connection._issuer,

                ValidateAudience = true,
                ValidAudience = Connection._audience,

                ValidateLifetime = true,

                ClockSkew = TimeSpan.Zero
            };

            return tokenValidationParameters;

        }
        private JwtIssuerOptions setJwtOptions()
        {
            var jwtOptions = new JwtIssuerOptions
            {
                Audience = Connection._audience,
                Issuer = Connection._issuer,
                SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
            };

            return jwtOptions;

        }

    }
}
