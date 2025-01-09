using Scalar.AspNetCore;
using YYYoinkAPI.Services.Users;

var builder = WebApplication.CreateBuilder(args);
{
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
    app.MapControllers();
    app.Run();
}