using dotnetMVP.Middleware;
using dotnetMVP.Models;
using dotnetMVP.Models.DTO.ChallengeDto;
using dotnetMVP.Models.DTO.PlatformDto;
using dotnetMVP.Models.DTO.User;
using dotnetMVP.Models.DTO.Writeup;
using dotnetMVP.Models.Entities;
using dotnetMVP.Services.Interface;
using dotnetMVP.Services.Realization;
using dotnetMVP.Types;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Serilog;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Host.ConfigureSerilog();

builder.Services.AddOpenTelemetry().ConfigureResource(r => r.AddService("WriteUpDemo")) //чтобы понимать откуда пришло 
    //.UseOtlpExporter(OpenTelemetry.Exporter.OtlpExportProtocol.HttpProtobuf,)
    .WithTracing(r =>
    {
        r.AddAspNetCoreInstrumentation(); // использование интструментов SDK? под трейсы и спаны ( ниже то же, но с метриками ) 
        r.AddHttpClientInstrumentation();
        r.AddConsoleExporter();
    })
    .WithMetrics(r =>
    {
        r.AddAspNetCoreInstrumentation()
            .AddHttpClientInstrumentation()
            .AddRuntimeInstrumentation().AddPrometheusExporter();

    });
// Add services to the container.
builder.Services.AddControllers();
 
builder.Services.AddDbContext<ApplicationDBcontext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresqlDefault"))
);
builder.Services.AddIdentity<AppUser, IdentityRole<Guid>>()
                .AddEntityFrameworkStores<ApplicationDBcontext>() ;

builder.Services.AddScoped<IWriteUpService,WriteUpService>();
builder.Services.AddScoped<WriteupMapper>();

builder.Services.AddScoped<IChallengeService, ChallengeService>();
builder.Services.AddScoped<ChallengeMapper>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<UserMapper>();

builder.Services.AddScoped<IPlatformService, PlatformService>();
builder.Services.AddScoped<PlatformMapper>();

builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddAuthentication(options => { 
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;})
                .AddJwtBearer(options => {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidateAudience = true,

                        ValidIssuer = builder.Configuration["Jwt:ValidIssuer"],
                        ValidAudience = builder.Configuration["Jwt:ValidAudience"],

                        //Change later to more secure approach , like using a key vault or environment variable ( mb docker image variables ) 
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SymmetricKey"]!)),
                        ValidAlgorithms = [SecurityAlgorithms.HmacSha256Signature]
    };
});

builder.Services.AddScoped<IJwtHandler, JwtHandler>();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("FullAccess", policy =>
    {
        policy.RequireClaim(ClaimTypes.Role, "Admin", "Moder");
    });
});


builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var serv = app.Services.CreateScope())
{
    var db = serv.ServiceProvider.GetRequiredService<ApplicationDBcontext>();
    db.Database.Migrate();
    SeedDatabase seed = new SeedDatabase(db, serv.ServiceProvider.GetRequiredService<UserManager<AppUser>>());
    await seed.EnsureRolesAndAdmins();
}
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    //app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//Enabling Swagger
app.UseSwagger();
app.UseSwaggerUI(); 

app.UseMiddleware<ExceptionHandlerMiddleware>();    

app.UseHttpsRedirection();
app.UseSerilogRequestLogging();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllers();
app.MapPrometheusScrapingEndpoint();
app.Run();
