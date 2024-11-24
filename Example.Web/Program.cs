using System.Data;
using System.Globalization;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json.Serialization;
using System.Text.Unicode;

using AutoMapper;

using Example.Web.Reports.Csv.Helpers;
using Example.Web.Settings;

using Microsoft.AspNetCore.Http.Features;
using Microsoft.Data.SqlClient;
using Microsoft.Net.Http.Headers;

using Serilog;

using Smart.AspNetCore;
using Smart.AspNetCore.ApplicationModels;
using Smart.AspNetCore.Filters;
using Smart.Data;
using Smart.Data.Accessor.Extensions.DependencyInjection;
using Smart.Data.SqlClient;

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

// Add framework builder.Services.
builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton(HtmlEncoder.Create(UnicodeRanges.All));

// Settings
var serverSetting = builder.Configuration.GetSection("Server").Get<ServerSetting>()!;

// Mvc
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
    options.ValueLengthLimit = Int32.MaxValue;
    options.MultipartBodyLengthLimit = Int64.MaxValue;
});

builder.Services
    .AddControllersWithViews(options =>
    {
        options.Conventions.Add(new KebabControllerModelConvention());
        options.Filters.AddTimeLogging();
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

// Health
builder.Services.AddHealthChecks();

// Mapper
builder.Services.AddSingleton<IMapper>(new Mapper(new MapperConfiguration(c =>
{
    c.AddProfile<Example.Web.Areas.Default.MappingProfile>();
})));

// Database
var connectionString = builder.Configuration.GetConnectionString("Default");
builder.Services.AddSingleton<IDbProvider>(new DelegateDbProvider(() => new SqlConnection(connectionString)));

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

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
