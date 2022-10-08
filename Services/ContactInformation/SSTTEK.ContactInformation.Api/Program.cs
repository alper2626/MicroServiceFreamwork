using AmqpBase.MassTransit.RabbitMq.Middlewares;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapperAdapter;
using CastleInterceptors.AutoFacModule;
using CommonMiddlewares;
using FluentValidation.AspNetCore;
using FluentValidationAdapter;
using FluentValidationAdapter.Filter;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RedisCacheService.Middleware;
using RedisCacheService.Models;
using RestHelpers.DIHelpers;
using ServerBaseContract;
using SSTTEK.ContactInformation.Api.Middlewares;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
var businessAssembly = Assembly.Load(Assembly.GetExecutingAssembly().GetReferencedAssemblies().SingleOrDefault(q => q.FullName.Contains("SSTTEK.ContactInformation.Business")));
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

#region Add Interceptors

builder.Host
    .UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>(cfg =>
    {
        cfg.RegisterModule(new InterceptorsAutoFacModule(businessAssembly));
    });

#endregion

#region Add Controllers And Filters

builder.Services.AddControllers(options =>
{
    options.Filters.Add(typeof(ModelValidatorAttribute));
});


#endregion

#region Add Core Di

builder.Services.AddCore();

#endregion

#region Add Fluent Validation

builder.Services.AddFluentValidation(options =>
{
    // Validate child properties and root collection elements
    options.ImplicitlyValidateChildProperties = true;
    options.ImplicitlyValidateRootCollectionElements = true;
    options.Configure();
});

#endregion

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

#region DatabaseConfigurations

builder.Services.Configure<DatabaseOptions>(options =>
{
    options.ConnectionString = builder.Configuration["DatabaseOptions:ConnectionString"];
    options.DatabaseName = builder.Configuration["DatabaseOptions:DatabaseName"];
});
builder.Services.AddSingleton<DatabaseOptions>(sp =>
{
    return sp.GetRequiredService<IOptions<DatabaseOptions>>().Value;
});


#endregion

#region Add MassTransit

builder.Services.AddRabbitMqModules(
    new AmqpBase.Model.RabbitMqOptions
    {
        Host = builder.Configuration["RabbitMq:Host"],
        Password = builder.Configuration["RabbitMq:Password"],
        UserName = builder.Configuration["RabbitMq:UserName"],
        Port = ushort.Parse(builder.Configuration["RabbitMq:Port"]),
    });

#endregion

#region Add Redis

builder.Services.Configure<RedisOptions>(options =>
{
    options.Host = builder.Configuration["RedisOptions:Host"];
    options.Port = builder.Configuration["RedisOptions:Port"];
    options.Password = builder.Configuration["RedisOptions:Password"];
});

builder.Services.AddSingleton<RedisOptions>(sp =>
{
    return sp.GetRequiredService<IOptions<RedisOptions>>().Value;
});

builder.Services.AddRedis(new RedisOptions
{
    Host = builder.Configuration["RedisOptions:Host"],
    Port = builder.Configuration["RedisOptions:Port"],
    Password = builder.Configuration["RedisOptions:Password"],
});

#endregion

#region AutoMapper Configuration

AutoMapperWrapper.Configure();

#endregion

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.InjectContactInformation(
    new DatabaseOptions { ConnectionString = builder.Configuration["DatabaseOptions:ConnectionString"], DatabaseName = builder.Configuration["DatabaseOptions:DatabaseName"] },
    Convert.ToBoolean(builder.Configuration["DatabaseChanged"]));

/// <summary>
/// Tüm Di lar enjecte oldu provider ý olustur. Ayný zamanda bu çaðýrýlmassa migration çalýþmaz.
/// </summary>
var services = ServiceTool.Create(builder.Services);

var app = builder.Build();

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
