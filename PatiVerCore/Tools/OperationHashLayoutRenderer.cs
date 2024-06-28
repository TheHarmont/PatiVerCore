using NLog.LayoutRenderers;
using NLog;
using System.Text;

namespace PatiVerCore.Tools
{
    /// <summary>
    /// Овечает за отображение ip адреса клиента в логах
    /// </summary>
    [LayoutRenderer("operationHash")]
    public class OperationHashLayoutRenderer : LayoutRenderer
    {
        protected override void Append(StringBuilder builder, LogEventInfo logEvent)
        {
            var opNum = OperationProvider.GitOperationHash();
            if (!string.IsNullOrEmpty(opNum))
            {
                builder.Append(opNum);
            }
        }

    }
}
