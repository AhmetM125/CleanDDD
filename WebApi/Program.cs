using Application;
using Application.Behaviors;
using FluentValidation;
using Infastracture;
using MediatR;
using Microsoft.AspNetCore.Diagnostics;
using Presentation;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddApplication()
    .AddPresentation()
    .AddInfastracture(builder.Configuration);

builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
builder.Services.AddValidatorsFromAssembly(typeof(ApplicationReference).Assembly
    , includeInternalTypes: true);

builder.Services.AddMediatR(x =>
    x.RegisterServicesFromAssembly(typeof(ApplicationReference).Assembly));


builder.Host.UseSerilog((context, configuration)
    => configuration.ReadFrom.Configuration(context.Configuration));

builder.Services
    .AddControllers()
    .AddApplicationPart(typeof(PresentationReference).Assembly);

builder.Services.AddExceptionHandler<
    WebApi.Middleware
    .ExceptionHandlerMiddleware>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler();

app.UseSerilogRequestLogging();
app.UseHttpsRedirection();

app.Run();
