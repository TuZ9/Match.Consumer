using Suitability.Consumer.Infrastructure.Ioc.Utils;
using Suitability.Consumer.Infrastructure.Static;
using Suitability.Consumer.Application.Services.BackgroundJobs;
using Suitability.Consumer.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);
SwaggerConfiguration.AddSwagger(builder.Services);
SerilogExtension.AddSerilog(builder.Configuration);
RunTimeConfig.SetConfigs(builder.Configuration);
builder.Services.AddHttpClients();

builder.Services.AddMemoryCache();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHealthChecks();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options => options.AddPolicy("All", opt => opt
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials()
                        .SetIsOriginAllowed(hostname => true)));
builder.WebHost.UseKestrel(so =>
{
    so.Limits.KeepAliveTimeout = TimeSpan.FromSeconds(10000);
    so.Limits.MaxRequestBodySize = 52428800;
    so.Limits.MaxConcurrentConnections = 100;
    so.Limits.MaxConcurrentConnections = 100;
});
builder.Services.AddHostedService<BackgroundJobs>();
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseRouting();
app.UseAuthentication();
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthorization();
app.UseCors("All");
app.MapControllers();
app.Run();