using System.Text;
using BG.NET.Library.BusinessLogicLayer;
using BG.NET.Library.DataAccessLayer;
using BG.NET.Library.DatabaseMigrator;
using BG.NET.Library.Models.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services
    .AddOptions<JwtOptions>()
    .Bind(config.GetSection(JwtOptions.SectionName))
    .ValidateOnStart();

builder.Services.AddSwaggerGen(options => {
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Auth API for BG.Library",
        Version = "v1",
        Description = "API for users autorization",
        Contact = new OpenApiContact
        {
            Name = "Ruslan Diadiushkin",
            Email = "contact@xnrudyson.anonaddy.me",
            Url = new Uri("https://www.linkedin.com/in/rudyson")
        }
    });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme {
        In = ParameterLocation.Header, 
        Description = "Please insert JWT with Bearer into field",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement {
        { 
            new OpenApiSecurityScheme 
            { 
                Reference = new OpenApiReference 
                { 
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer" 
                } 
            },
            Array.Empty<string>()
        } 
    });
});

builder.Services.AddRouting(options => options.LowercaseUrls = true);

var jwtOptions = config
    .GetSection(JwtOptions.SectionName)
    .Get<JwtOptions>();

var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions!.Secret));

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            
            ValidAudience = jwtOptions.Audience,
            ValidIssuer = jwtOptions.Issuer,
            IssuerSigningKey = signingKey,
            
            RequireExpirationTime = true
        };
    });

builder.Services.AddCors(
    options =>
    {
        options.AddPolicy("AllowAll", p =>
        {
            p.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
    });

builder.Services.ExecuteDatabaseMigrator(config);
builder.Services.AddDataAccessLayer(config);
builder.Services.AddBusinessLogicLayer(config);

var app = builder.Build();

app.UseCors("AllowAll");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();