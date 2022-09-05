using System.Net.Mime;
using System.Reflection;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using OrderStatusApi.Interfaces;
using OrderStatusApi.Models;
using OrderStatusApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<OrderDbContext>(options => options.UseInMemoryDatabase("OrderDatabase"));
builder.Services.AddScoped<IOrderService, OrderService>();

builder.Services.AddControllers(options =>
{
    options.Filters.Add(new ProducesAttribute(MediaTypeNames.Application.Json));
    options.Filters.Add(new ConsumesAttribute(MediaTypeNames.Application.Json));
}).AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(2, 0);
    options.ReportApiVersions = true;
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.OrderActionsBy(sort => sort.RelativePath);
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "1.0",
        Title = "OrderStatus API",
        Description = "API de exemplo para calcular o percentual de cada tipo de \"Status de Pedido\".",
        Contact = new OpenApiContact { Name = "Github", Url = new Uri("https://github.com/Rafael-Dagostim") },
    });

    options.SwaggerDoc("v2", new OpenApiInfo
    {
        Version = "2.0",
        Title = "OrderStatus API",
        Description = "API de exemplo para calcular o percentual de cada tipo de \"Status de Pedido\". Agora inclui armazenamento de valores em um banco de dados em memória.",
        Contact = new OpenApiContact { Name = "Github", Url = new Uri("https://github.com/Rafael-Dagostim") },
    });

    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint($"/swagger/v1/swagger.json", "OrderStatus API v1");
        options.SwaggerEndpoint($"/swagger/v2/swagger.json", "OrderStatus API v2");
    });
}

app.UseCors("AllowAllOrigins");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
