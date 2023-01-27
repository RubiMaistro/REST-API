using BlazorWebClient.Data;
using BlazorWebClient.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorWebClient
{
    public class Program
    {

        private static ConfigurationManager _config { get; set; }

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            _config = builder.Configuration;

            ConfigureServices(builder.Services);

            var app = builder.Build();
            ConfigureBuild(app);
            app.Run();
        }
        /// <summary>
        /// Add services to the container.
        /// </summary>
        /// <param name="services"></param>
        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddSingleton<WeatherForecastService>();

            // Initialize base uri addres of web API server
            var dataverseBaseUri = _config.GetSection("DataverseConfig").GetValue<string>("BaseUri");
            services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(dataverseBaseUri) });

            services.AddHttpClient<IPersonService, PersonService>(client =>
            {
                client.BaseAddress = new Uri(dataverseBaseUri);
            });

        }
        /// <summary>
        /// Configure the HTTP request pipeline.
        /// </summary>
        /// <param name="webApplication"></param>
        /// <exception cref="NotImplementedException"></exception>
        private static void ConfigureBuild(WebApplication app)
        {
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");
        }

    }
}