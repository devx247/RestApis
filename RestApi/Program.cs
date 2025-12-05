using System.Text;
using CodeFirstRestApi.Extensions;
using CodeFirstRestApi.Models.DataManager;
using CodeFirstRestApi.Models.Repository;
using CodeFirstRestApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.AzureAppServices;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Configure Services

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
//builder.Logging.AddJsonConsole(options =>
//{
//    options.IncludeScopes = false;
//    options.TimestampFormat = "hh:mm:ss ";
//    options.JsonWriterOptions = new JsonWriterOptions
//    {
//        Indented = true
//    };
//});
builder.Logging.AddAzureWebAppDiagnostics();


#region JWT Bearer Token Configuration

builder.Services.AddAuthentication(opt =>
    {
        opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; // "Bearer"
        opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme; // "Bearer"
    })
    .AddJwtBearer(opt =>
    {
        var signingKey = builder.Configuration["Jwt:SigningKey"];
        var issuer = builder.Configuration["Jwt:Issuer"];
        var audience = builder.Configuration["Jwt:Audience"];

        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey));
        opt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = "zone-eric-issuer-server",
            ValidAudience = "zone-eric-audience-client",
            IssuerSigningKey = symmetricSecurityKey
        };
    });

#endregion

builder.Services.AddDbContext<EmployeeDbContext>(opts =>
{
    opts.UseSqlServer(builder.Configuration.GetConnectionString("CodeFirstRestApi"),
        x => x.MigrationsHistoryTable("__RestApiMigrationsHistory", "restapi"));
});

builder.Services.AddScoped<IDataRepository<Employee>, EmployeeManager>();


builder.Services.ConfigureCors();
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "CodeFirstRestApi", Version = "v1" });

    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        In = ParameterLocation.Header,
        Description = "Please enter API Token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });

    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference()
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// Azure App Service File Logger Options
builder.Services.Configure<AzureFileLoggerOptions>(options =>
{
    options.FileName = "azure-diagnostics-";
    options.FileSizeLimit = 50 * 1024;
    options.RetainedFileCountLimit = 5;
});


var app = builder.Build();

// Configure HTTP request pipeline


var startupLogger = app.Services.GetService<ILogger<Program>>();
startupLogger.LogInformation("Startup Logger Init");

app.Use(async (context, next) =>
{
    var requestPath = context.Request.Path.Value;
    // TODO Log 
    startupLogger.LogInformation($"{nameof(requestPath)}: {requestPath}");


    await next();
});


if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}


app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "CodeFirstRestApi v1");
    c.RoutePrefix = String.Empty;
});

app.UseHttpsRedirection();

app.UseRouting();
app.UseCors("CorsPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


app.Run();