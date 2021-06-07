using BlazorCalc.Business;
using BlazorCalc.Business.Contracts;
using BlazorCalc.Business.Operators;
using BlazorCalc.Repository;
using BlazorCalc.Repository.Classes;
using BlazorCalc.Repository.Contracts;
using BlazorCalc.Server.Parsers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BlazorCalc.Server
{
    public class Startup
    {
        public Startup(IHostEnvironment ihe)
        {
            Configuration = new ConfigurationBuilder()
                .AddJsonFile("appSettings.json") // Get Connectionstring from appsetting.json
                .Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddRazorPages();

            services.AddDbContext<HistoryDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("default")));

            //Custom registration of operators
            services.AddSingleton<IOperator, PlusOperator>();
            services.AddSingleton<IOperator, MinusOperator>();
            services.AddSingleton<IOperator, DivideOperator>();
            services.AddSingleton<IOperator, MultiplyOperator>();

            services.AddScoped<IRepository, HistoryRepository>();
            services.AddSingleton<OutputParser>();
            services.AddSingleton<InputParser>();
            services.AddSingleton<IBusinessService, CalculationService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapFallbackToFile("index.html");
            });

            using var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>()?.CreateScope();

            if (serviceScope == null) return;
            var context = serviceScope.ServiceProvider.GetRequiredService<HistoryDbContext>();

            context.Database.EnsureCreated();
        }
    }
}