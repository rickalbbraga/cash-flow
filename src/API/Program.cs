using API.Configurations.Filters;
using API.Configurations.Middlewares;
using API.Configurations.Swagger;
using API.IoC;
using Application.Configurations;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfraDataConfigurations(builder.Configuration);
builder.Services.AddCQSConfigurations();
builder.Services.AddDomainConfigurations();
builder.Services.AddAutoMapper(typeof(AutoMapperConfiguration));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SchemaFilter<EnumDescriptionSchemaFilter>();
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Fluxo de Caixa API",
        Description = "API para gerenciar o fluxo de caixa.",
    });

    var layers = new[]{ "API.xml", "Application.xml" };
    
    foreach (var item in layers)
    {
        var xmlPath = Path.Combine(AppContext.BaseDirectory, item);
        options.IncludeXmlComments(xmlPath);
    }
});
builder.Services.AddControllers(options => 
{
    options.Filters.Add<DomainNotificationFilter>();
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseReDoc(c =>
    {
        c.DocumentTitle = "REDOC API Documentation";
        c.SpecUrl = "/swagger/v1/swagger.json";
        c.HideDownloadButton();
    });
}

app.UseMiddleware<ExceptionMiddleware>();
app.UseHttpsRedirection();
app.MapControllers();
app.Run();


