﻿using Autofac.Extensions.DependencyInjection;
using Autofac;
using Core.Extensions;
using Core.Utilities.Security.Encryption;
using Core.Utilities.Security.Jwt;
using DataAccess.Concrete;
using Helper.AppSetting;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Reflection;
using Business.DependencyResolvers.Autofac;
using Microsoft.AspNetCore.Localization;
using System.Globalization;
using Core.Middleware;
using Entities.VMs;
using Communication.EmailManager.Abstract;
using Communication.EmailManager.Concrete;
using Serilog.Events;
using Serilog.Formatting.Json;
using Serilog;
using System.Net;
using Serilog.Sinks.Email;
using LogEvent = Core.CrossCuttingConcerns.Logging.LogEvent;
using Core.Utilities.IoC;
using Core.CrossCuttingConcerns;
using System.Security.Claims;
using Core.CrossCuttingConcerns.Logging.SeriLog;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Core.CrossCuttingConcerns.Logging.ElasticSearch;
using Core.Helpers;
using Microsoft.Extensions.Caching.Memory;

IConfiguration Configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appSettings.json", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();


static IEdmModel GetEdmModel()
{
    var odataBuilder = new ODataConventionModelBuilder();
    odataBuilder.EnableLowerCamelCase();
    odataBuilder.EntitySet<BlogVm>("Blog");
    odataBuilder.EntitySet<BlogCategoryVm>("BlogCategory");
    odataBuilder.EntitySet<ProjectVm>("Project");
    odataBuilder.EntitySet<ProjectFeaturesVm>("ProjectFeature");
    odataBuilder.EntitySet<UserVm>("User");
    odataBuilder.EntitySet<ContactRequestEntityVm>("ContactRequest");
    odataBuilder.EntitySet<CommentVm>("Comment");
    return odataBuilder.GetEdmModel();
}


var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    Args = args,
    ContentRootPath = Directory.GetCurrentDirectory(),
});

builder.Configuration.AddJsonFile("appSettings.json", optional: false, reloadOnChange: true);
builder.Configuration.AddEnvironmentVariables();

var tokenOptions = builder.Configuration.GetSection("TokenOptions").Get<TokenOptions>();
var applicationName = "RayonApi";
var notifyMails = "huseyincankeskin10@gmail.com";

Log.Logger = new LoggerConfiguration()
   .ReadFrom.Configuration(Configuration)
   .Enrich.FromLogContext()
   .MinimumLevel.Override("Quartz", LogEventLevel.Error)
   .Enrich.WithProperty("Application", applicationName)
   .WriteTo.File(
       formatter: new JsonFormatter(),
       path: Directory.GetCurrentDirectory() + "/DataLogs/rayon-logs-.json",
       rollingInterval: RollingInterval.Day,
       fileSizeLimitBytes: 50000000,
       rollOnFileSizeLimit: true)
   .WriteTo.Conditional(e =>
   {
       if (!string.IsNullOrEmpty(notifyMails) && e.Properties.TryGetValue("EventId", out var value))
       {
           var eventId = JsonConvert.DeserializeObject<EventId>(value.ToString());
           return eventId == LogEvent.Notify;
       }
       return false;
   },
   (configuration) => configuration.Email(new EmailConnectionInfo
   {
       FromEmail = "info@rayonproperty.com",
       MailServer = "srvm11.trwww.com",
       Port = 587,
       NetworkCredentials = new NetworkCredential("info@rayonproperty.com", "Tf5FyFCYA$.1", "rayonproperty.com"),
       ToEmail = notifyMails,
       EmailSubject = "RayonProperty Transaction Log"
   },
   outputTemplate: "Timestamp: {Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz}\n" +
   "Level: {Level}\n" +
   "Message: {Message}\n" +
   "Data: {Data}\n" +
   "Exception: {Exception}\n\n"))
   .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddDbContext<RayonPropertyContext>(options =>
                options.UseSqlServer(Configuration.GetDecryptedConnectionString(ConnectionStringType.Development)));



var contractResolver = new DefaultContractResolver
{
    NamingStrategy = new CamelCaseNamingStrategy()
};

builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
    options.SerializerSettings.ContractResolver = contractResolver;
    options.UseCamelCasing(true);
}).AddOData(opt => opt.AddRouteComponents("api", GetEdmModel()).EnableQueryFeatures().Count().Select().OrderBy());

builder.Services.AddMvcCore(options =>
{
    foreach (var outputFormatter in options.OutputFormatters.OfType<ODataOutputFormatter>().Where(_ => _.SupportedMediaTypes.Count == 0))
    {
        outputFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/prs.odatatestxx-odata"));
    }
    foreach (var inputFormatter in options.InputFormatters.OfType<ODataInputFormatter>().Where(_ => _.SupportedMediaTypes.Count == 0))
    {
        inputFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/prs.odatatestxx-odata"));
    }
}).AddApiExplorer();


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = tokenOptions.Issuer,
            ValidAudience = tokenOptions.Audience,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey),
            ClockSkew = TimeSpan.Zero
        };
    });

var assemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();
var assembly = Assembly.Load("Business");
assemblies.Add(assembly);

builder.Services.AddAutoMapper(assemblies.ToArray());
builder.Services.AddHttpClient();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSingleton<IJwtHelper, JwtHelper>();
builder.Services.AddSingleton<IEmailManager, EmailManager>();
builder.Services.AddSingleton<ISeriLogService, SerilogService>();
builder.Services.AddSingleton<IMemoryCache, MemoryCache>();
builder.Services.AddSingleton<IElasticSearchService, ElasticSearchService>();
builder.Services.TryAddSingleton<IHttpAccessorHelper, HttpAccessorHelper>();
builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();


builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>(builder =>
    {
        builder.RegisterModule(new AutofacBusinessModule());

    });



builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder => builder.WithOrigins("https://www.rayonproperty.com",
                                                            "http://www.rayonproperty.com",
                                                            "https://rayon-management.rayonproperty.com",
                                                            "http://rayon-management.rayonproperty.com",
                                                            "http://localhost:3000",
                                                            "http://localhost:4000"
                                                            ).AllowAnyHeader().AllowAnyMethod().SetIsOriginAllowedToAllowWildcardSubdomains());
});
var app = builder.Build();

app.UseSerilogRequestLogging(options =>
{
    options.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
    {
        diagnosticContext.Set("LogType", (int)LogType.Request);

        var isUserExist = httpContext.User.FindFirstValue(CustomClaimTypes.UserId) != null;
        diagnosticContext.Set("User", isUserExist ? new TokenUserInfo
        {
            UserId = Guid.Parse(httpContext.User.FindFirstValue(CustomClaimTypes.UserId)),
            UserName = httpContext.User.FindFirstValue(CustomClaimTypes.UserName),
            Email = httpContext.User.FindFirstValue(CustomClaimTypes.UserEmail),
        } : null, isUserExist);
    };
});

ServiceTool.Create(null, app.Services);
app.UseSwaggerUI();
var defaultDateCulture = "tr-TR";
var ci = new CultureInfo(defaultDateCulture);
ci.NumberFormat.NumberDecimalSeparator = ",";
ci.NumberFormat.CurrencyDecimalSeparator = ",";

app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture(ci),
    SupportedCultures = new List<CultureInfo>
                {
                    ci,
                },
    SupportedUICultures = new List<CultureInfo>
                {
                    ci,
                }
});

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseMiddleware<ExceptionMiddleware>();
app.UseHttpsRedirection();
app.UseRouting();
app.UseCors();
app.UseStaticFiles();
app.UseSwagger(x => x.SerializeAsV2 = true);
app.UseMiddleware<JwtMiddleware>();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
