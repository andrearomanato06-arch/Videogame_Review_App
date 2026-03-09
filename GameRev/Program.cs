using Microsoft.EntityFrameworkCore;
using GameRev.Data;
using GameRev.Configurations;

//!DotNetEnv.Env.Load();

var builder = WebApplication.CreateBuilder(args);

//!builder.Configuration.AddEnvironmentVariables();

builder.Services.AddOpenApi();

builder.Services.AddDbContext(builder.Configuration);

builder.Services.AddRepositories();

builder.Services.AddCors(options => {
    options.AddPolicy("VideogameReviewAppPolicy", policy => {
        policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
    });
});

var app = builder.Build();

app.UseCors("VideogameReviewAppPolicy");

app.Run();