using System.Text.Json;
using DotNetEnv;
using DriverStandingsApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add config file
builder.Configuration.AddJsonFile("appsettings.json");

Env.Load();

var apiKey = Environment.GetEnvironmentVariable("RB_API_KEY");

builder.AddServiceDefaults();

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower;
        options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IDriverStandingsService, DriverStandingsService>();

builder.Services.AddHttpClient("RBApi", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["HttpClientSettings:BaseAddress"]);
    client.DefaultRequestHeaders.Add("x-api-key", apiKey);
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", builder =>
    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

var app = builder.Build();

app.MapDefaultEndpoints();

app.UseCors("AllowAllOrigins");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
