namespace MicroCredential
{
    using AutoMapper;
    using MediatR;
    using MicroCredential.Infrastructure;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Options;
    using System.Reflection;
    using Microsoft.OpenApi.Models;

    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMediatR(Assembly.Load("MicroCredential.Domain"));
            services.AddAutoMapper(Assembly.Load("MicroCredential.Domain"));
            services.Configure<CustomerDatabaseSettings>(Configuration.GetSection(nameof(CustomerDatabaseSettings)));
            services.AddSingleton<ICustomerDatabaseSettings>(sp => sp.GetRequiredService<IOptions<CustomerDatabaseSettings>>().Value);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MicroServices - Authentication Api", Version = "v1" });
            });
            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Authentication Service");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
