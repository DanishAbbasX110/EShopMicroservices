using BuildingBlocks.Behaviors;

var builder = WebApplication.CreateBuilder(args);

// Add Service to the Container

var assembly = typeof(Program).Assembly;

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssemblies(assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
});

builder.Services.AddValidatorsFromAssembly(assembly);

builder.Services.AddCarter();

builder.Services.AddMarten(opts =>
{
    opts.Connection(builder.Configuration.GetConnectionString("Database")!);

}).UseLightweightSessions();

var app = builder.Build();

// Configure the HTTP request Pipeline

app.MapCarter();

app.Run();
