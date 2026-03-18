using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using GameRev.Configurations;
using GameRev.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

//!DotNetEnv.Env.Load();

var builder = WebApplication.CreateBuilder(args);

//!builder.Configuration.AddEnvironmentVariables();

builder.Services.AddOpenApi();

builder.Services.AddDbContext(builder.Configuration);

builder.Services.AddRepositories();
builder.Services.AddServices();

builder.Services.AddCors(options => {
    options.AddPolicy("GameRevPolicy", policy => {
        policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
    });
});

builder.Services.AddAuthentication(
    JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "PVC.sbiruli.it", //! ENV DATA
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("simoncinolimoncinosbirulinoballerinopiccolinopatatinotonypitony"))
    };

    options.Events = new JwtBearerEvents
    {
        OnTokenValidated = async context =>
        {
            var database = context.HttpContext.RequestServices.GetRequiredService<AppDbContext>();
            var tokenUserId = context.Principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var tokenJtid = context.Principal.FindFirst(JwtRegisteredClaimNames.Jti)?.Value;

            if(string.IsNullOrEmpty(tokenUserId) || string.IsNullOrEmpty(tokenJtid))
            {
                context.Fail("Invalid token credentials given");
                return;
            }

            long userId = long.Parse(tokenUserId);
            long jtid = long.Parse(tokenJtid);

            var session = await database.UserSessions.FirstOrDefaultAsync(s => s.UserId == userId && s.Jtid == jtid);

            if(session is null)
            {
                context.Fail("The current session seems to be expired, please log in again");
            }
        }
    };
});

builder.Services.AddAuthorization();

var app = builder.Build();

app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.UseCors("GameRevPolicy");

app.Run();