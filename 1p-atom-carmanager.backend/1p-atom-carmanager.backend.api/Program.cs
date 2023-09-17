using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

#region[Controllers]
builder.Services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);
#endregion

#region[Endpoints]
builder.Services.AddEndpointsApiExplorer();
#endregion

#region[Logger | Serilog]
builder.Host.UseSerilog((hostingContext, loggerConfiguration) =>
{
    loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration);
});
#endregion

#region[Swagger]
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "MpfinApi", Version = "v1" });
    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});
#endregion

#region[Guard key]
//start ключ безопасности
var environment = builder.Services.BuildServiceProvider()
    .GetRequiredService<IWebHostEnvironment>();
builder.Services.AddDataProtection()
                    .SetApplicationName($"carmanager-{environment.EnvironmentName}")
                    .PersistKeysToFileSystem(new DirectoryInfo($@"{environment.ContentRootPath}\keys"));
//end ключ безопасности
#endregion

#region[Identity]
builder.Services.AddAuthorization();
/*builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.User.RequireUniqueEmail = true;    // óíèêàëüíûé email
    options.SignIn.RequireConfirmedEmail = false;
    options.Password.RequiredLength = 4;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireDigit = false;
    options.Password.RequireNonAlphanumeric = false;

    options.User.AllowedUserNameCharacters =
            "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
}).AddEntityFrameworkStores<ApplicationContext>().AddDefaultTokenProviders();*/
#endregion

#region[Cors]
var defaultOrigin = "_defaultOrigin";
string[] origins = { "http://localhost:3000" };
builder.Services.AddCors(o => o.AddPolicy(defaultOrigin, builder =>
{
    builder.WithOrigins(origins)
    .AllowAnyMethod()
    .AllowAnyHeader().AllowCredentials();
}));
#endregion

#region[Cookie]
builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.SameSite = SameSiteMode.None;
    options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
});
#endregion

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseExceptionHandler(c => c.Run(async context =>
{
    var exception = context.Features
        .Get<IExceptionHandlerPathFeature>()!
        .Error;
    var response = new { exception.Message };
    await context.Response.WriteAsJsonAsync(response);
}));

app.UseCors(defaultOrigin);
app.UseAuthentication();

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseDefaultFiles();

app.UseStaticFiles();

app.MapControllers();

app.Run();
