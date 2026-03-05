using Microsoft.EntityFrameworkCore;
using Videogame_Review_App.Data;
using Videogame_Review_App.Configurations;

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