using ApiKeysAuthentication.Api;
using ApiKeysAuthentication.Api.Authentication;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add API Key authentication
builder.Services
    .AddAuthorization(x =>
    {
        x.AddPolicy(PolicyNames.RequiresWeather, p => p.RequireRole(RoleNames.ReadWeather));
    })
    .AddApiKeysAuthentication();

// Add services to the container.
builder.Services.AddSingleton<IApiKeys>(
    new StaticApiKeys(
        new() { { "Test", "T35t" } },
        new() { { "T35t", [RoleNames.ReadWeather] } }));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition(ApiKeyAuthenticationSchemeOptions.DefaultScheme, new OpenApiSecurityScheme
    {
        Name = ApiKeyAuthenticationSchemeOptions.AuthorizationHeaderName,
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Description = "Authorization by x-api-key inside request's header",
        Scheme = ApiKeyAuthenticationSchemeOptions.DefaultScheme,
    });
    OpenApiSecurityScheme key = new OpenApiSecurityScheme
    {
        Reference = new OpenApiReference
        {
            Type = ReferenceType.SecurityScheme,
            Id = ApiKeyAuthenticationSchemeOptions.DefaultScheme
        },
        In = ParameterLocation.Header
    };
    OpenApiSecurityRequirement securityRequirement = new OpenApiSecurityRequirement {
        {
            key,
            new List<string>()
        } };
    options.AddSecurityRequirement(securityRequirement);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(x => {
        x.DefaultModelExpandDepth(2);
        x.EnableTryItOutByDefault();
        x.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.Full);
    });
}

app.UseHttpsRedirection();

app.UseAuthentication().UseAuthorization();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
    .RequireAuthorization(PolicyNames.RequiresWeather)
    .WithName("GetWeatherForecast")
    .WithOpenApi();

app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
