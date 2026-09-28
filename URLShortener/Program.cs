
using Microsoft.EntityFrameworkCore;
using URLShortener.Data;
using URLShortener.Interfaces;
using URLShortener.Services;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("Default");

builder.Services.AddSingleton<ICodeGenerator, CodeGenerator>();
builder.Services.AddScoped<IUrlValidator, UrlValidator>();
builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddDbContext<ApplicationDbContext>(options => 
options.UseNpgsql(connectionString));


var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI( options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "URLShortener V1");
    });
}
app.UseHttpsRedirection();
// app.UseAuthorization();
app.MapControllers();
app.Run();