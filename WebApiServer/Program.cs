using Microsoft.EntityFrameworkCore;
using WebApi_Server.Repositories;

namespace WebApi_Server
{
    public class Program
    {
        private static ConfigurationManager? _config { get; set; }
        public static ConfigurationManager? Config { get => _config; }

        private static string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

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
        /// Add services to the container
        /// </summary>
        /// <param name="services"></param>
        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddMvc();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddDbContext<PersonContext>(options =>
                options.UseSqlServer(_config?.GetConnectionString("WebApiServerDb")));

            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                    policy =>
                    {
                        policy.WithOrigins("https://localhost:7000");
                    });
            });
        }

        /// <summary>
        /// Configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        private static void ConfigureBuild(WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors(MyAllowSpecificOrigins);

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();
        }
    }
}