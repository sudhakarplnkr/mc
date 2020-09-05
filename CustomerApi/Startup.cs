namespace MicroCredential.CustomerApi
{
    using AutoMapper;
    using MediatR;
    using MicroCredential.Infrastructure;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using System.Reflection;
    using Microsoft.OpenApi.Models;
    using Microsoft.EntityFrameworkCore;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            this.Configuration = new ConfigurationBuilder()
#if DEBUG
                .AddJsonFile("appsettings.Development.json")
#else
                .AddJsonFile("appsettings.json")
#endif
                .Build();
            services.AddMediatR(Assembly.Load("MicroCredential.Domain"));
            services.AddAutoMapper(Assembly.Load("MicroCredential.Domain"));
            services.AddDbContext<CustomerDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("CustomerConnection")));
            services.AddTransient<IRedisContext>(s=> new RedisContext(Configuration.GetConnectionString("RedisConnection")));
            services.AddTransient<ICustomerRedisContext, CustomerRedisContext>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MicroCredential - Customer Api", Version = "v1" });
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
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Customer Service");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            ConfigureDatabase(app);
        }

        private void ConfigureDatabase(IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();
            var context = serviceScope.ServiceProvider.GetRequiredService<CustomerDbContext>();
            context.Database.EnsureCreated();
        }
    }
}
