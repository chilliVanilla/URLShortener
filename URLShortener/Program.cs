
using Microsoft.EntityFrameworkCore;
using URLShortener.Data;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("Default");

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