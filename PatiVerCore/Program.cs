using CoreWCF.Configuration;
using CoreWCF.Description;
using Microsoft.EntityFrameworkCore;
using NLog;
using NLog.Web;
using PatiVerCore;
using PatiVerCore.Abstract;
using PatiVerCore.Concrete;
using PatiVerCore.DataLayer.Abstract;
using PatiVerCore.DataLayer.DAL;
using PatiVerCore.ServiceLayer.CacheService;
using PatiVerCore.ServiceLayer.CacheService.Abstract;
using PatiVerCore.ServiceLayer.FomsService;
using PatiVerCore.ServiceLayer.FomsService.Abstract;
using PatiVerCore.Tools;

//��������� ��������� ������ ��� Nlog
LogManager.Setup().SetupExtensions(s =>
{
    s.RegisterLayoutRenderer<IpAddressLayoutRenderer>("ipAddress");
    s.RegisterLayoutRenderer<OperationHashLayoutRenderer>("operationHash");
});

var logger = NLog.LogManager.Setup()
        .LoadConfigurationFromAppSettings()
        .GetCurrentClassLogger();
logger.Debug("������ ����������");
try
{
    var builder = WebApplication.CreateBuilder(args);

    // ��������� ������
    builder.Logging.ClearProviders();
    builder.Logging.SetMinimumLevel
            (Microsoft.Extensions.Logging.LogLevel.Trace);
    builder.Host.UseNLog();

    var connectionString = builder.Configuration.GetConnectionString("PersonResponse");
    builder.Services.AddDbContext<PatiVerContext>(option => { option.UseSqlServer(connectionString); });

    builder.Services.AddServiceModelServices();
    builder.Services.AddServiceModelMetadata();
    builder.Services.AddSingleton<IServiceBehavior, UseRequestHeadersForMetadataAddressBehavior>();

    builder.Services.AddScoped<IPatiVerManager, PatiVerManager>();
    builder.Services.AddScoped<IPersonResponseRepository, PersonResponseRepository>();
    builder.Services.AddScoped<IFomsService, FomsService>();
    builder.Services.AddSingleton<ICacheService, CacheService>();

    builder.Services.AddTransient<WcfService>();

    var app = builder.Build();

    //����� ������ InitializeCache, ������� ��������� ��� ������� �� ��.
    using (var scope = app.Services.CreateScope())
    {
        var cacheService = scope.ServiceProvider.GetRequiredService<ICacheService>();
        cacheService.InitializeCache();
    }

    app.UseServiceModel(serviceBuilder =>
    {
        serviceBuilder.AddService<WcfService>();
        serviceBuilder.AddServiceEndpoint<WcfService, IWcfService>(new CoreWCF.BasicHttpBinding(CoreWCF.Channels.BasicHttpSecurityMode.None), "/PatiVerWcf.svc");
        var serviceMetadataBehavior = app.Services.GetRequiredService<ServiceMetadataBehavior>();
        serviceMetadataBehavior.HttpGetEnabled = true;
        serviceMetadataBehavior.HttpsGetEnabled = true;
    });

    app.MapGet("/", () => "��������!");

    logger.Debug("���������� ��������");
    app.Run();
}
catch (Exception ex)
{
    logger.Error(ex, $"����������� ��-�� ����������: {ex.Message}" );
    throw;
}
finally
{
    NLog.LogManager.Shutdown();
}

