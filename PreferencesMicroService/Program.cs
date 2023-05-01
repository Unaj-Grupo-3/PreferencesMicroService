using Application.Interfaces;
using Application.UseCases;
using Infrastructure.Commands;
using Infrastructure.Persistence;
using Infrastructure.Queries;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Microservicio Preferencias", Version = "v1" });
});

// INYECCION POR DEPENDENCIAS//ver timelife
//builder.Services.AddScoped<IClass, Class>();

//CONECTION STRING
var connectionString = builder.Configuration["ConnectionString"];
builder.Services.AddDbContext<AppDbContext>(op => op.UseSqlServer(connectionString));

//TRANSIENTS
builder.Services.AddTransient<IGenderPreferenceCommand, GenderPreferenceCommand>();
builder.Services.AddTransient<IGenderPreferenceQuery, GenderPreferenceQuery>();
builder.Services.AddTransient<IGenderPreferenceService, GenderPreferenceService>();

builder.Services.AddTransient<IInterestCategoryCommand, InterestCategoryCommand>();
builder.Services.AddTransient<IInterestCategoryQuery, InterestCategoryQuery>();
builder.Services.AddTransient<IInterestCategoryService, InterestCategoryService>();

builder.Services.AddTransient<IInterestCommand, InterestCommand>();
builder.Services.AddTransient<IInterestQuery, InterestQuery>();
builder.Services.AddTransient<IInterestService, InterestService>();

builder.Services.AddTransient<IOverallPreferenceCommand, OverallPreferenceCommand>();
builder.Services.AddTransient<IOverallPreferenceQuery, OverallPreferenceQuery>();
builder.Services.AddTransient<IOverallPreferenceService, OverallPreferenceService>();

builder.Services.AddTransient<IPreferenceCommand, PreferenceCommand>();
builder.Services.AddTransient<IPreferenceQuery, PreferenceQuery>();
builder.Services.AddTransient<IPreferenceService, PreferenceService>();

builder.Services.AddTransient<IUserApiService, UserApiService>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
