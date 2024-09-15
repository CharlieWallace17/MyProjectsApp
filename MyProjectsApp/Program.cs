
using Microsoft.AspNetCore.HttpLogging;

namespace MyProjectsApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                  policy =>
                                  {
                                      policy.WithOrigins("http://localhost:5173").AllowAnyHeader();
                                  });
            });

            // Add services to the container.
            builder.Services.AddAuthorization();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddHttpLogging(options => // <--- Setup logging
            {
                // Specify all that you need here:
                options.LoggingFields = HttpLoggingFields.RequestHeaders |
                                        HttpLoggingFields.RequestBody |
                                        HttpLoggingFields.ResponseHeaders |
                                        HttpLoggingFields.ResponseBody;
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            //if (app.Environment.IsDevelopment())
            //{
            app.UseCors(MyAllowSpecificOrigins);

            app.UseHttpLogging();

            app.UseSwagger();
            app.UseSwaggerUI();
            //}

            app.UseHttpsRedirection();

            var summaries = new[]
            {
                "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
            };

            app.MapGet("/weatherforecast", (HttpContext httpContext) =>
            {
                var forecast = Enumerable.Range(1, 5).Select(index =>
                    new WeatherForecast
                    {
                        Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                        TemperatureC = Random.Shared.Next(-20, 55),
                        Summary = summaries[Random.Shared.Next(summaries.Length)]
                    })
                    .ToArray();
                return forecast;
            })
            .WithName("GetWeatherForecast")
            .WithOpenApi();

            app.MapGet("/binaryConverter/{binaryInput}", (HttpContext httpContext, string binaryInput) =>
            {
                double decimalOutput = BinaryConverter.GetDecimalFromBinary(binaryInput);
                return Task.FromResult(System.Text.Json.JsonSerializer.Serialize(decimalOutput));
            }).WithOpenApi();

            app.Run();
        }
    }
}
