using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace EmployeeManagementSystem
{
    /// <summary>
    /// This will have the main program where the Apllication actually starts
    /// </summary>
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// Here the connection Startup file has been established
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

    }
}
