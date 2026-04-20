using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// 1. Configuration
builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

// 2. Register Services (MUST BE BEFORE builder.Build())
builder.Services.AddCors(options => {
    options.AddPolicy("AllowNetlify",
        policy => {
            policy.WithOrigins("https://glowing-sunshine-d907db.netlify.app")
                  .AllowAnyMethod()
                  .AllowAnyHeader()
                  .AllowCredentials();
        });
});

builder.Services.AddOcelot();

// 3. Port Configuration
var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
builder.WebHost.UseUrls($"http://0.0.0.0:{port}");

var app = builder.Build();

// 4. Middleware Pipeline (Order Matters!)

// Always put CORS near the top
app.UseCors("AllowNetlify"); 

app.MapGet("/", () => "Gateway is Running!");

// Ocelot should generally be at the end
await app.UseOcelot();

app.Run();