using Autofac.Extensions.DependencyInjection;
using Autofac;
using FreeEducation.Services.Catalog.Services;
using FreeEducation.Services.Catalog.Settings;
using Microsoft.Extensions.Options;
using System.Reflection;
using FreeEducation.Services.Catalog.Repositories.Interfaces;
using FreeEducation.Services.Catalog.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Autofac Container'ı kullanarak servisleri yapılandırın
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
       .ConfigureContainer<ContainerBuilder>(containerBuilder =>
       {
           var dataAccess = Assembly.GetExecutingAssembly();

           containerBuilder.RegisterAssemblyTypes(dataAccess)
                  .Where(t => t.Name.EndsWith("Repository")|| t.Name.EndsWith("Service"))
                  .AsImplementedInterfaces();
       });

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection(nameof(DatabaseSettings)));
builder.Services.AddSingleton<IDatabaseSettings>(sp =>
{
    return sp.GetRequiredService<IOptions<DatabaseSettings>>().Value;
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
