using System.Text;
using BGLibrary.Identity.Contexts;
using BGLibrary.Identity.Tools;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

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

builder.Services.AddAutoMapper(typeof(AutomapperProfile).Assembly);

builder.Services.AddDbContext<IdentityDbContext>(options =>
{
    var dbServer = Environment.GetEnvironmentVariable("POSTGRES_HOST") ?? "localhost";
    var dbAddress = Environment.GetEnvironmentVariable("POSTGRES_DB") ?? "bglibrary";
    var dbUser = Environment.GetEnvironmentVariable("POSTGRES_USER") ?? "admin";
    var dbPassword = Environment.GetEnvironmentVariable("POSTGRES_PASSWORD") ?? "pswd1234";
    
    var connectionString = $"Server={dbServer};" +
                           $"Database={dbAddress};" +
                           $"Port=5432;" +
                           $"User Id={dbUser};" +
                           $"Password={dbPassword};";
    
    options.UseNpgsql(connectionString);
});

var secretKey = Environment.GetEnvironmentVariable("JWT_SECRET") ?? "SC1";
var issuer = Environment.GetEnvironmentVariable("JWT_ISSUER") ?? "I1";
var audience = Environment.GetEnvironmentVariable("JWT_AUDIENCE") ?? "A1";

var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

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
            
            ValidAudience = audience,
            ValidIssuer = issuer,
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