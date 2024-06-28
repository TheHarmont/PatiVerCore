using CoreWCF;
using CoreWCF.Channels;
using NLog;
using PatiVerCore.ServiceLayer.FomsService.Model.Request;
using System.Text;
using System.Security.Cryptography;

namespace PatiVerCore.Tools
{
    internal static class OperationProvider
    {
        /// <summary>
        /// Возвращает ip адрес клиента, от которого поступил запрос
        /// </summary>
        internal static string? GetClientIpAddress()
        {
            // Получение текущего контекста операции
            var context = OperationContext.Current;
            if (context == null)
            {
                return null;
            }

            // Получение свойств входящего сообщения
            var properties = context.IncomingMessageProperties;

            // Проверка наличия свойств HTTP-запроса
            if (properties.ContainsKey(HttpRequestMessageProperty.Name))
            {
                var httpRequest = (HttpRequestMessageProperty)properties[HttpRequestMessageProperty.Name];

                // Извлечение заголовка X-Forwarded-For
                var xForwardedFor = httpRequest.Headers["X-Forwarded-For"];
                if (!string.IsNullOrEmpty(xForwardedFor))
                {
                    // В случае нескольких IP-адресов, возвращаем первый
                    return xForwardedFor.Split(',')[0].Trim();
                }
            }
            // Если заголовок X-Forwarded-For отсутствует, используем IP-адрес соединения
            return (properties[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty).Address;
        }

        /// <summary>
        /// Возаращает значение OperationHash из контекста логгера
        /// </summary>
        internal static string? GitOperationHash()
        {
            return ScopeContext.TryGetProperty("OperationHash", out var value) ? value?.ToString() : null;
        }

        /// <summary>
        /// Возвращает хэш строки
        /// </summary>
        /// <param name="req">Запрос по ФИО</param>
        internal static string GetHashRequest(PersonRequestFIO req)
        {
            return String.Format("{0:X}",(req.Surname + req.Firstname + req.Patronymic + DateTime.Now.ToString()).GetHashCode());
        }

        /// <summary>
        /// Возвращает хэш строки
        /// </summary>
        /// <param name="req">Запрос по Снилс</param>
        internal static string GetHashRequest(PersonRequestSNILS req)
        {
            return String.Format("{0:X}", (req.Snils + DateTime.Now.ToString()).GetHashCode());
        }

        /// <summary>
        /// Возвращает хэш строки
        /// </summary>
        /// <param name="req">Запрос по Полис</param>
        internal static string GetHashRequest(PersonRequestPolis req)
        {
            return String.Format("{0:X}", (req.Polis + DateTime.Now.ToString()).GetHashCode());
        }
    }
}
