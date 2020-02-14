using ASP.NETCoreTicTacToe.Infrastructure;
using ASP.NETCoreTicTacToe.Models;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace ASP.NETCoreTicTacToe
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }



        private readonly ILoggerFactory LoggerFactory = 
            Microsoft.Extensions.Logging.LoggerFactory.Create(builder => 
                builder
                    .AddFilter((category, level) => category == DbLoggerCategory.Database.Command.Name)
                    .AddConsole()
                    .AddDebug());

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            string connection = Configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<TicTacToeContext>(options =>
            {
                options.UseSqlServer(connection);
                if (Configuration.GetValue<bool>("LogQueries"))
                {
                    options.UseLoggerFactory(LoggerFactory);
                }
            });
            

            services.AddCors();

            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddControllersWithViews();

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "my-app/build";
            });

            services.AddSingleton<Models.GameFarm>();
            services.AddSingleton<Models.BotFarm>();
            services.AddSingleton<Models.Lobby>();
            services.AddTransient<GameAPI>();
            

            services.AddMvc()
                .SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_3_0);
        }



        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseStaticFiles();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();


            });


            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "my-app";
                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });
        }
    }
}
