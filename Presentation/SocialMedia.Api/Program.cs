using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.Core;
using SocialMedia.Application;
using SocialMedia.Infrastructure;
using SocialMedia.Infrastructure.Services.Storage.Local;
using SocialMedia.Persistance;
using SocialMedia.SignalR;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddPersistanceServices();
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddSignalRServices();
builder.Services.AddStorage<LocalStorage>();


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer("Bearer", options =>
    {
        options.TokenValidationParameters = new()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidAudience = builder.Configuration["Token:Audience"],
            ValidIssuer = builder.Configuration["Token:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"])),
            LifetimeValidator = (notBefore, expires, securityToken, validationParameters) => expires != null ? expires > DateTime.UtcNow : false,
        };
    });


Logger logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.MSSqlServer(builder.Configuration.GetConnectionString("SqlServer"),"Logs",autoCreateSqlTable:true)
    .Enrich.FromLogContext()
    .MinimumLevel.Information()
    .CreateLogger();

builder.Host.UseSerilog(logger);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();
app.MapHubs();


app.Run();
