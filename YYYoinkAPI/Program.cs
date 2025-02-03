using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using YYYoinkAPI.Services.Users;
using YYYoinkAPI.utils;

string root = Directory.GetCurrentDirectory();
string dotenv = Path.Combine(root, ".env");
DotEnv.Load(dotenv);

string CORSPolicy = "cors_policy";

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
{
    string? keyStr = Environment.GetEnvironmentVariable("JWT_KEY") ?? string.Empty;
    if (string.IsNullOrWhiteSpace(keyStr))
    {
        throw new InvalidOperationException("JWT_KEY is invalid or missing during JWT Generation");
    }

    byte[] key = Encoding.UTF8.GetBytes(keyStr);

    builder.Configuration.AddEnvironmentVariables();
    builder.Services.AddAuthorization();
    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidIssuer = Environment.GetEnvironmentVariable("JWT_ISSUER"),
                ValidAudience = Environment.GetEnvironmentVariable("JWT_AUDIENCE"),
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true,
                ValidateIssuer = true,
                ValidateAudience = true
            };
        });
    builder.Services.AddCors(
        options =>
        {
            options.AddPolicy(name: CORSPolicy,
                policy =>
                {
                    policy.WithOrigins("http://localhost:5173")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
        }
    );
    builder.Services.AddControllers();
    builder.Services.AddScoped<IUserService, UserService>();
    builder.Services.AddOpenApi();
}

WebApplication app = builder.Build();
{
    app.UseCors(CORSPolicy);
    app.UseAuthentication();
    app.UseAuthorization();
    if (app.Environment.IsDevelopment())
    {
        app.MapOpenApi();
        app.MapScalarApiReference(options =>
        {
            options
                .WithTheme(ScalarTheme.Mars);
        });
    }

    app.UseExceptionHandler("/error");
    // app.UseHttpsRedirection();
    app.MapControllers();
    app.Run();
}