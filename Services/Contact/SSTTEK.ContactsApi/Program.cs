using AmqpBase.MassTransit.RabbitMq.Middlewares;
using AutoMapperAdapter;
using CommonMiddlewares;
using FluentValidation.AspNetCore;
using FluentValidationAdapter;
using FluentValidationAdapter.Filter;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RestHelpers.DIHelpers;
using ServerBaseContract;
using SSTTEK.Contact.Api.Middlewares;
using SSTTEK.Contact.Business.HttpClients;
using SSTTEK.Contact.Business.HttpClients.Handler;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);


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

#region AutoMapper Configuration

AutoMapperWrapper.Configure(Assembly.Load("SSTTEK.MassTransitCommon"));

#endregion

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

/// <summary>
/// Module DI's Added.
/// </summary>
builder.Services.InjectContact(
    new DatabaseOptions { ConnectionString = builder.Configuration["DatabaseOptions:ConnectionString"], DatabaseName = builder.Configuration["DatabaseOptions:DatabaseName"] },
    Convert.ToBoolean(builder.Configuration["DatabaseChanged"]));

/// <summary>
/// HttpClients added.
/// </summary>
builder.Services.AddHttpClient<IContactInformationClient, ContactInformationClient>(c => c.BaseAddress = new System.Uri(builder.Configuration["HttpClients:ContactInformationClient"]))
                .AddHttpMessageHandler<UpdateHeaderHandler>();


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

app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
