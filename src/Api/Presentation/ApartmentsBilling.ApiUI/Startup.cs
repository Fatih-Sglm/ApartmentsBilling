using ApartmentsBilling.BussinessLayer;
using ApartmentsBilling.BussinessLayer.Configuration.Filter;
using ApartmentsBilling.BussinessLayer.Configuration.LogFilters;
using ApartmentsBilling.BussinessLayer.Configuration.Validations.UserValidation;
using ApartmentsBilling.Cache.Configuration;
using ApartmentsBilling.DataAccesLayer;
using FluentValidation.AspNetCore;
using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;

namespace ApartmentsBilling.ApiUI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.DataAccesServices(Configuration);
            services.BussinesServices(Configuration);
            services.AddMemoryCache();
            services.HangfireSevices(Configuration);
            services.CacheService(Configuration);
            services.AddSingleton<MssqlLog>();
            services.AddControllers(opt =>
            {
                opt.Filters.Add<ValidationFilter>();
                opt.Filters.Add<ExceptionFilter>();
            }).AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<CreateUserDtoValidation>()).ConfigureApiBehaviorOptions(opt => opt.SuppressModelStateInvalidFilter = true);//.AddNewtonsoftJson();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,

                        },
                        new List<string>()
                    }
                });
            });
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApartmentsBilling.ApiUI v1"));
            }

            //app.UseExceptionHandler(c => c.Run(async context =>
            //{
            //    var exception = context.Features.Get<IExceptionHandlerFeature>();

            //    var statusCode = exception.Error switch
            //    {
            //        ClientSideException => 400,
            //        NotFoundException => 404,
            //        UnAuthorizedException => 403,
            //        _ => 500
            //    };
            //    context.Response.StatusCode = statusCode;


            //    var response = CustomResponseDto<NoContent>.Fail(statusCode, exception.Error.Message);
            //    await context.Response.WriteAsJsonAsync(JsonSerializer.Serialize(response));
            //}));

            app.UseHttpsRedirection();


            app.UseRouting();

            app.UseHangfireDashboard("/hangfire");
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
