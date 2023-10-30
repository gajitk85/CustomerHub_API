using Microsoft.OpenApi.Models;
using CustomerHub.API.Infra;
using CustomerHub.API.Application;


var policyName = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: policyName,
                      builder =>
                      {
                          builder
                            .AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                      });
});
var services = builder.Services;
services.AddMvc();
services.AddHttpClient();
services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "CustomerApis", Version = "v1", });
});
services.AddInfrastructre(builder.Configuration);
services.AddApplication();


var app = builder.Build();
app.MapControllers();
app.UseHttpsRedirection();
app.UseSwagger();

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "CustomerApis v1");
}
);
app.UseCors(policyName);
app.Run();

