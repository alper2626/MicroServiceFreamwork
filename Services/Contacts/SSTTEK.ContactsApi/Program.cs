using AmqpBase.MassTransit.RabbitMq.Middlewares;
using AmqpBase.Model;
using FluentValidation.AspNetCore;
using FluentValidationAdapter.Filter;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RestHelpers.DIHelpers;
using ServerBaseContract;
using SSTTEK.Contact.DataAccess.Context;

var builder = WebApplication.CreateBuilder(args);

#region Add Controllers And Filters

builder.Services.AddControllers(options =>
{
    options.Filters.Add(typeof(ModelValidatorAttribute));
});


#endregion

#region Add Fluent Validation

builder.Services.AddFluentValidationAutoValidation(options =>
{
    // Validate child properties and root collection elements
    options.ImplicitlyValidateChildProperties = true;
    options.ImplicitlyValidateRootCollectionElements = true;
});

#endregion

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

#region DatabaseConfigurations

builder.Services.Configure<DatabaseOptions>(options =>
{
    options.ConnectionString = builder.Configuration.GetValue<string>("ConnectionString");
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
        Password = builder.Configuration["Password"],
        UserName = builder.Configuration["UserName"],
        Port = ushort.Parse(builder.Configuration["Port"]),
    });

#endregion

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

/// <summary>
/// Tüm Di lar enjecte oldu provider ý olustur.
/// </summary>
var services = ServiceTool.Create(builder.Services);

/// <summary>
/// Database de deðiþiklik var ise migrate et.
/// </summary>
if (Convert.ToBoolean(builder.Configuration["DatabaseChanged"]))
{
    var context = ServiceTool.ServiceProvider.GetRequiredService<ContactModuleContext>();
    context.Database.Migrate();
}


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
