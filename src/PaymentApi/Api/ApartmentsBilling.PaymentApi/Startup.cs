using ApartmentsBilling.PaymentApi.Filters;
using ApartmentsBilling.PaymentApiSevices.DBSettings;
using ApartmentsBilling.PaymentApiSevices.Mapper;
using ApartmentsBilling.PaymentApiSevices.Repositories.Common;
using ApartmentsBilling.PaymentApiSevices.Repositories.Concrete.Common;
using ApartmentsBilling.PaymentApiSevices.Services.Concrete;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

namespace ApartmentsBilling.PaymentApi
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
            services.AddAutoMapper(config =>
            {
                config.AddProfile(new GenericProfile());
            });

            services.AddSingleton<IDbSettings>(sp =>
            {
                return sp.GetRequiredService<IOptions<DbSettings>>().Value;
            });

            services.AddScoped<IReceiptService, ReceiptService>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.Configure<DbSettings>(Configuration.GetSection("MongoDbSettings"));
            services.AddControllers(opt =>
            {
                opt.Filters.Add<ExceptionFilter>();
            });
            services.AddSwaggerGen(c =>
            {
                c.OperationFilter<Filters.ReApplyOptionalRouteParameterOperationFilter>();
                c.EnableAnnotations();
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ApartmentsBilling.PaymentApi", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApartmentsBilling.PaymentApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
