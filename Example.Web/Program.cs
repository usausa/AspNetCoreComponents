using System.Data;
using System.Globalization;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json.Serialization;
using System.Text.Unicode;

using AutoMapper;

using Example.Web.Reports.Csv.Helpers;
using Example.Web.Reports.Pdf.Builders;
using Example.Web.Reports.Pdf.Helpers;
using Example.Web.Settings;

using Microsoft.AspNetCore.Http.Features;
using Microsoft.Data.SqlClient;
using Microsoft.Net.Http.Headers;

using PdfSharpCore.Fonts;

using Serilog;

using Smart.AspNetCore;
using Smart.AspNetCore.ApplicationModels;
using Smart.AspNetCore.Filters;
using Smart.Data;
using Smart.Data.Accessor.Extensions.DependencyInjection;
using Smart.Data.SqlClient;

using StackExchange.Profiling;
using StackExchange.Profiling.Data;

// Configure builder
#pragma warning disable CA1812
var builder = WebApplication.CreateBuilder(args);

// Log
builder.Logging.ClearProviders();
builder.Services.AddSerilog(option =>
{
    option.ReadFrom.Configuration(builder.Configuration);
});

// System
Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

// PDF
GlobalFontSettings.FontResolver = new FontResolver(Directory.GetCurrentDirectory(), FontNames.Gothic, new Dictionary<string, string>
{
    { FontNames.Gothic, "ipaexg.ttf" }
});

// Add framework builder.Services.
builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton(HtmlEncoder.Create(UnicodeRanges.All));

// Settings
var serverSetting = builder.Configuration.GetSection("Server").Get<ServerSetting>()!;

// Mvc
builder.Services.AddSingleton<ExceptionStatusFilter>();
builder.Services.Configure<TimeLoggingOptions>(option =>
{
    option.Threshold = serverSetting.LongTimeThreshold;
});
builder.Services.AddSingleton<TimeLoggingFilter>();

builder.Services.Configure<RouteOptions>(options =>
{
    options.AppendTrailingSlash = true;
});

builder.Services.Configure<FormOptions>(options =>
{
    options.ValueLengthLimit = int.MaxValue;
    options.MultipartBodyLengthLimit = Int64.MaxValue;
});

builder.Services
    .AddControllersWithViews(options =>
    {
        options.Filters.AddExceptionStatus();
        options.Filters.AddTimeLogging();
        options.Conventions.Add(new LowercaseControllerModelConvention());
    })
#if DEBUG
            .AddRazorRuntimeCompilation()
#endif
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        options.JsonSerializerOptions.Encoder = JavaScriptEncoder.Create(UnicodeRanges.All);
    });

// SignalR
builder.Services.AddSignalR();

// Profiler
if (!builder.Environment.IsProduction())
{
    builder.Services.AddMiniProfiler(options =>
    {
        options.RouteBasePath = "/profiler";
    });
}

// Health
builder.Services.AddHealthChecks();

// Mapper
builder.Services.AddSingleton<IMapper>(new Mapper(new MapperConfiguration(c =>
{
    c.AddProfile<Example.Web.Areas.Default.MappingProfile>();
})));

// Database
var connectionString = builder.Configuration.GetConnectionString("Default");
builder.Services.AddSingleton<IDbProvider>(builder.Environment.IsProduction()
    ? new DelegateDbProvider(() => new SqlConnection(connectionString))
    : new DelegateDbProvider(() => new ProfiledDbConnection(new SqlConnection(connectionString), MiniProfiler.Current)));

builder.Services.AddSingleton<IDialect, SqlDialect>();

builder.Services.AddDataAccessor(c =>
{
    c.EngineOption.ConfigureTypeMap(map =>
    {
        map[typeof(DateTime)] = DbType.DateTime2;
    });
});

// Csv
builder.Services.AddSingleton<CsvExporter>();

// Report
builder.Services.AddSingleton<ExampleReportBuilder>();

// Configure
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsProduction())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/error/500");
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/error/{0}");

app.UseHealthChecks("/health");

app.UseHttpsRedirection();
app.UseStaticFiles(new StaticFileOptions
{
    OnPrepareResponse = ctx =>
    {
        ctx.Context.Response.Headers[HeaderNames.CacheControl] = "public, max-age=31536000";
        ctx.Context.Response.Headers.Append("Expires", DateTime.UtcNow.AddYears(1).ToString("R", CultureInfo.InvariantCulture));
    }
});

if (!app.Environment.IsProduction())
{
    app.UseMiniProfiler();
}

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
