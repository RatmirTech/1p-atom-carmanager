using _1p_atom_carmanager.backend.core;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.OpenApi.Models;
using Serilog;

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
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Atom FMS 1P", Version = "v1" });
    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
    var filePath = Path.Combine(AppContext.BaseDirectory, "AtomFMS.xml");
    c.IncludeXmlComments(filePath);
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

#region[Cors]
//var defaultOrigin = "_defaultOrigin";
//string[] origins = { "http://localhost:3000" };
//builder.Services.AddCors(o => o.AddPolicy(defaultOrigin, builder =>
//{
//    builder.WithOrigins(origins)
//    .AllowAnyMethod()
//    .AllowAnyHeader().AllowCredentials();
//}));
#endregion

#region[Db and Core]
builder.Services.AddCore(builder.Configuration["ConnectionString"]);
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

//app.UseCors(defaultOrigin);
app.UseAuthentication();

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseDefaultFiles();

app.UseStaticFiles();

app.MapControllers();

app.Run();
