using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using SERU_BACKEND_TES.Helper;
using SERU_BACKEND_TES.Interface;
using SERU_BACKEND_TES.Models.Data;
using SERU_BACKEND_TES.Services;
using SERU_BACKEND_TES.ViewModels.JwtSetting;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
#region jwt configuration
//jwt configuration
ConfigurationManager configuration = builder.Configuration;

var _jwtSetting = configuration.GetSection("JWTsettings");
builder.Services.Configure<JwtSettings>(_jwtSetting);

var authKey = configuration.GetValue<string>("JWTsettings:securityKey");

builder.Services.AddAuthentication(item =>
{
    item.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    item.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(item =>
{
    item.RequireHttpsMetadata = true;
    item.SaveToken = true;
    item.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authKey)),
        ValidateIssuer = false,
        ValidateAudience = false,
    };
});

#endregion

builder.Services.AddTransient<DapperDBContext>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<GenerateJwt>();
builder.Services.AddScoped<LoginIntf, LoginSvc>();
builder.Services.AddScoped<VehicleIntf, VehicleSvc>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.UseSwagger();
    //app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
