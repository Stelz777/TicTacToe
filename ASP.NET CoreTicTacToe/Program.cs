using ASP.NETCoreTicTacToe.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;

namespace ASP.NETCoreTicTacToe
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<TicTacToeContext>();
                }
                catch (InvalidOperationException exception)
                {
                    var logger = services.GetRequiredService<ILogger<Startup>>();
                    string errorMessage = "An error occured seeding the DB.";
#pragma warning disable CA1303 // �� ���������� �������� � �������� �������������� ����������
                    logger.LogError(exception, errorMessage);
#pragma warning restore CA1303 // �� ���������� �������� � �������� �������������� ����������
                }
            }
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
