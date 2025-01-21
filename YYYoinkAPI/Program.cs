using Scalar.AspNetCore;
using YYYoinkAPI.Services.Users;

var CORSPolicy = "cors_policy";

var builder = WebApplication.CreateBuilder(args);
{
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

var app = builder.Build();
{
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
    app.UseCors(CORSPolicy);
    app.MapControllers();
    app.Run();
}