using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TodoApi.Interfaces;
using TodoApi.Models;
using TodoApi.Services;


var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
var _secretKey = "p5PvtN2GczOSiI8u8H2Qvfxlg4ZazHCQPSDm5u6b3HQ=";

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidateIssuer = true, // เพิ่มการตรวจสอบ Issuer
            ValidateAudience = true, // เพิ่มการตรวจสอบ Audience
            ClockSkew = TimeSpan.Zero, // ไม่ให้มีการปรับเวลา
            ValidIssuer = "winona",
            ValidAudience = "winona",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey))
        };
    });
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
    options.AddPolicy("Editor", policy => policy.RequireRole("Editor"));
    // options.AddPolicy("RequireUserOnly",policy => policy.RequireRole(Roles.User));
});

var connectionstring = "Server=localhost;port=3306;Database=IdentityUser;User=root;Password=P@ssw0rd;";
Console.WriteLine(connectionstring);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseMySql(connectionstring, ServerVersion.AutoDetect(connectionstring), opt => opt.EnableRetryOnFailure()));
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();


// swagg
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IFileService, FileService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
