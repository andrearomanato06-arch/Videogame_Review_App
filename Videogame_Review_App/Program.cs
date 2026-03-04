using Microsoft.EntityFrameworkCore;
using VideogameReviewApp.Data;

DotNetEnv.Env.Load();

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables();

builder.Services.AddOpenApi();
Console.WriteLine("Prima : "+builder.Configuration.GetConnectionString("Server"));
builder.Services.AddDbContext<AppDbContext>(options => { 
        var connectionString = builder.Configuration.GetConnectionString("Server");
        var serverVersion = ServerVersion.AutoDetect(connectionString);
        options.UseMySql( connectionString, serverVersion);
    }
);

Console.WriteLine("Dopo : "+ builder.Configuration.GetConnectionString("Server"));
builder.Services.AddCors(options => {
    options.AddPolicy("VideogameReviewAppPolicy", policy => {
        policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
    });
});

var app = builder.Build();

app.UseCors("VideogameReviewAppPolicy");

app.Run();