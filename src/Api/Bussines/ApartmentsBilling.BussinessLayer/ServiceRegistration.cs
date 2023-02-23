using ApartmentsBilling.BussinessLayer.Configuration.Authorize;
using ApartmentsBilling.BussinessLayer.Configuration.Cache;
using ApartmentsBilling.BussinessLayer.Mapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ApartmentsBilling.BussinessLayer
{
    public static class ServiceRegistration
    {
        public static void BussinesServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpContextAccessor();
            services.AddAutoMapper(config =>
            {
                config.AddProfile(new GenericProfile());
            });
            //AutoFac den dolayı gerek kalmadı.
            //services.AddScoped<IUserService, UserService>();
            //services.AddScoped<IBillService, BillService>();
            //services.AddScoped<IFlatService, FlatService>();
            //services.AddScoped<IBillTypeService, BillTypeService>();
            //services.AddScoped<IApartmentService, ApartmanServices>();
            //services.AddScoped<IVehicleService, VehicleService>();
            //services.AddScoped<IMessageService, MeesageService>();
            services.AddScoped<ICache_Helper, Cache_Helper>();
            services.AddScoped<IAuthHorize, AuthHorize>();
            var tokenOptions = configuration.GetSection("TokenOptions").Get<Configuration.Auth.TokenOption>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidIssuer = tokenOptions.Issuer,
                        ValidAudience = tokenOptions.Audience,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOptions.SecurityKey))
                    };
                });
        }
    }
}
