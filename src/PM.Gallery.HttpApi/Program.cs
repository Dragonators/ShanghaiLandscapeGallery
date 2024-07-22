using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PM.Gallery.Application;
using PM.Gallery.Domain;
using PM.Gallery.HttpApi.Filter;
using PM.Gallery.Infrastructure;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

builder.Services.AddSerilog();

builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddApplicationServices();

builder.Services.AddDomainServices();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        corsPolicyBuilder =>
        {
            corsPolicyBuilder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

builder.Services.AddAuthentication("Bearer").AddJwtBearer("Bearer", p =>
{
    p.Authority = "https://localhost:7001";
    p.TokenValidationParameters.ValidateAudience = false;
    // p.TokenValidationParameters = new TokenValidationParameters
    // {
    //     ValidateAudience = false
    // };
    // p.TokenValidationParameters.ValidTypes = new[] { "at+jwt" };
});

builder.Services.AddAuthorization(p =>
{
    p.AddPolicy("GalleryPolicy", opt =>
    {
        opt.RequireAuthenticatedUser();
        opt.RequireClaim("scope", "gallery_api");
        // opt.RequireRole("Admin");
    });
});

// 其他服务注册
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Type=SecuritySchemeType.OAuth2,
        Flows = new OpenApiOAuthFlows
        {
            Implicit = new OpenApiOAuthFlow
            {
                AuthorizationUrl = new Uri("https://localhost:7001/connect/authorize"),
                Scopes = new Dictionary<string, string>
                {
                    {"gallery_api", "Access to Gallery API"},
                },
            },
        }
    });
    opt.OperationFilter<AuthorizeCheckOperationFilter>();
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(
        setup =>
        {
            setup.OAuthClientId("swagger_api");
        });
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();