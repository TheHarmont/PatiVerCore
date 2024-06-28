using NLog.LayoutRenderers;
using NLog;
using System.Text;
using CoreWCF;

namespace PatiVerCore.Tools
{
    /// <summary>
    /// Овечает за отображение ip адреса клиента в логах
    /// </summary>
    [LayoutRenderer("ipAddress")]
    public class IpAddressLayoutRenderer : LayoutRenderer
    {
        protected override void Append(StringBuilder builder, LogEventInfo logEvent)
        {
            var ipAddress = OperationProvider.GetClientIpAddress();
            if (!string.IsNullOrEmpty(ipAddress))
            {
                builder.Append(ipAddress);
            }
        }
    }
}
