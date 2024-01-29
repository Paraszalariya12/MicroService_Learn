using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Ecomm_Service.ProductAPI.Extensions
{
    public static class WebApplicationBuilderExtensions
    {
        public static WebApplicationBuilder AddAppAuthentication(this WebApplicationBuilder builder)
        {

            var SecretKey = builder.Configuration.GetValue<string>("ApiSettings:SecretKey");
            var Issuer = builder.Configuration.GetValue<string>("ApiSettings:Issuer");
            var Audience = builder.Configuration.GetValue<string>("ApiSettings:Audience");

            var Key = Encoding.ASCII.GetBytes(SecretKey);

            builder.Services.AddAuthentication(a => { a.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; a.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme; }).
                AddJwtBearer(a =>
                {
                    a.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Key),
                        ValidateIssuer = true,
                        ValidIssuer = Issuer,
                        ValidateAudience = true,
                        ValidAudience = Audience,
                    };
                });

            builder.Services.AddAuthorization();

            return builder;
        }
    }
}
