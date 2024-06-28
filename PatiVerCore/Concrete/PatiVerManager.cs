using NLog;
using PatiVerCore.Abstract;
using PatiVerCore.DataLayer.Abstract;
using PatiVerCore.ServiceLayer.CacheService.Abstract;
using PatiVerCore.ServiceLayer.FomsService.Abstract;
using PatiVerCore.ServiceLayer.FomsService.Model.Request;
using PatiVer;
using PatiVerCore.ServiceLayer.FomsService.Tools;
using PatiVerCore.Tools;
namespace PatiVerCore.Concrete
{
    public class PatiVerManager : IPatiVerManager
    {
        private readonly ILogger<PatiVerManager> _logger;

        private readonly ICacheService _cacheService;
        private readonly IFomsService _foms;
        private readonly IPersonResponseRepository _personResponse;
        public PatiVerManager(ILogger<PatiVerManager> logger, ICacheService cacheService,IPersonResponseRepository personResponse, IFomsService fomsService)
        {
            _logger = logger;

            _cacheService = cacheService;
            _personResponse = personResponse;
            _foms = fomsService;
        }

        public PersonResponse GetPersonByFio(PersonRequestFIO req)
        {
            //Записываем в контекст хэш запроса, для отображения в логах
            using (ScopeContext.PushProperty("OperationHash", OperationProvider.GetHashRequest(req)))
            {
                _logger.LogInformation($"Начало обработки запроса по ФИО: {req.MoId}, {req.Surname} {req.Firstname} {req.Patronymic}, {req.Birthday}");
                if (DateTime.TryParse(req.Birthday, out DateTime birthdate))
                {
                    var key = req.Firstname?.ToLower().Trim() + req.Surname?.ToLower().Trim() + req.Patronymic?.ToLower().Trim() + birthdate;
                    //Проверка в КЭШ
                    if (_cacheService.FindCacheById(key, out PersonResponse cacheData))
                    {
                        _logger.LogInformation("Пациент был найден в КЭШ");
                        _logger.LogInformation("Пациент верифицирован");

                        return cacheData;
                    }

                    //Проверка в ФОМС
                    try
                    {
                        var fomsData = _foms.GetPersonInfo_FIO(req);

                        if (fomsData.SearchResult != "1")
                        {
                            _logger.LogInformation("Пациент не найден");
                            return new PersonResponse() { SearchResult = fomsData.SearchResult };
                        }

                        //Сохраняем данные в БД и КЭШ
                        PerformDataSaving(fomsData);

                        _logger.LogInformation("Пициент был найден в ФОМС");
                        _logger.LogInformation("Пациент верифицирован");
                        return fomsData;
                    }
                    catch (TimeoutException to_ex)  // Пытаемся поймать TimeoutException при запросе в ФОМС
                    {
                        _logger.LogError(to_ex, $"Ошибка TimeOut: {to_ex.Message}");
                        _logger.LogInformation("Превышено время ожидания от ФОМС, будет произведен поиск в локальной БД");
                        //Возвращаем даныне из локальной БД
                        var localData = _personResponse
                                            .GetLocalDataByFio(req.Firstname, req.Surname, req.Patronymic, birthdate)
                                            .GonvertToResponse();
                        if (localData == null)
                        {
                            _logger.LogInformation("Пациент не найден, либо найдено больше одного");
                            return new PersonResponse()
                            {
                                MessageData = "Пациент не найден, либо найдено больше одного",
                                SearchResult = "-1"
                            };
                        }
                        _logger.LogInformation("Пициент был найден в локальной БД");
                        return localData;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, $"Ошибка во время выполнения запроса: {ex.Message}");
                        return new PersonResponse()
                        {
                            MessageData = "Пациент не найден, либо найдено больше одного",
                            SearchResult = "-1"
                        };
                    }
                }
                else
                {
                    _logger.LogInformation("В верификации отказано: Дата рождения указана в неверном формате");
                    return new PersonResponse()
                    {
                        MessageData = "Дата рождения указана в неверном формате",
                        SearchResult = "-1"
                    };
                }
            }
        }

        public PersonResponse GetPersonBySnils(PersonRequestSNILS req)
        {
            //Записываем в контекст хэш запроса, для отображения в логах
            using (ScopeContext.PushProperty("OperationHash", OperationProvider.GetHashRequest(req)))
            {
                _logger.LogInformation($"Начало выполнения запроса по СНИЛС: {req.MoId}, {req.Snils}");
                //Проверка в КЭШ
                if (_cacheService.FindCacheById(req.Snils, out PersonResponse cacheData))
                {
                    _logger.LogInformation("Пациент был найден в КЭШ. Данные отправлены");
                    _logger.LogInformation("Пациент верифицирован");

                    return cacheData;
                }

                try
                {
                    //Возвращаем данные из ФОМС
                    var fomsData = _foms.GetPersonInfo_SNILS(req);
                    if (fomsData.SearchResult != "1") return new PersonResponse() { SearchResult = fomsData.SearchResult };

                    //Сохраняем данные в БД и КЭШ
                    PerformDataSaving(fomsData);

                    _logger.LogInformation("Пициент был найден в ФОМС");
                    _logger.LogInformation("Пациент верифицирован");
                    return fomsData;
                }
                catch (TimeoutException to_ex)  // Пытаемся поймать TimeoutException при запросе в ФОМС
                {
                    _logger.LogError(to_ex, $"Ошибка TimeOut: {to_ex.Message}");
                    _logger.LogInformation("Превышено время ожидания от ФОМС, будет произведен поиск в локальной БД");
                    //Возвращаем даныне из локальной БД
                    var localData = _personResponse
                                        .GetLocalDataBySnils(req.Snils)
                                        .GonvertToResponse();
                    if (localData == null)
                    {
                        _logger.LogInformation("Пациент не найден, либо найдено больше одного");
                        return new PersonResponse()
                        {
                            MessageData = "Пациент не найден, либо найдено больше одного",
                            SearchResult = "-1"
                        };
                    }
                    _logger.LogInformation("Данные были найдены в локальной БД");
                    return localData;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Ошибка во время выполнения запроса: {ex.Message}");
                    return new PersonResponse()
                    {
                        MessageData = "Пациент не найден, либо найдено больше одного",
                        SearchResult = "-1"
                    };
                }
            }
        }

        public PersonResponse GetPersonByPolis(PersonRequestPolis req)
        {
            //Записываем в контекст хэш запроса, для отображения в логах
            using (ScopeContext.PushProperty("OperationHash", OperationProvider.GetHashRequest(req)))
            {
                //Проверка в КЭШ
                _logger.LogInformation($"Начало выполнения запроса по Polis: {req.MoId}, {req.Polis}");
                if (_cacheService.FindCacheById(req.Polis, out PersonResponse cacheData))
                {
                    _logger.LogInformation("Пациент был найден в КЭШ");
                    _logger.LogInformation("Пациент верифицирован");

                    return cacheData;
                }

                try
                {
                    //Возвращаем данные из ФОМС
                    var fomsData = _foms.GetPersonInfo_Polis(req);
                    if (fomsData.SearchResult != "1") return new PersonResponse() { SearchResult = fomsData.SearchResult };

                    //Сохраняем данные в БД и КЭШ
                    PerformDataSaving(fomsData);

                    _logger.LogInformation("Пициент был найден в ФОМС");
                    _logger.LogInformation("Пациент верифицирован");
                    return fomsData;
                }
                catch (TimeoutException to_ex)  // Пытаемся поймать TimeoutException при запросе в ФОМС
                {
                    _logger.LogError(to_ex, $"Ошибка TimeOut: {to_ex.Message}");
                    _logger.LogInformation("Превышено время ожидания от ФОМС, будет произведен поиск в локальной БД");
                    //Возвращаем даныне из локальной БД
                    var localData = _personResponse
                                        .GetLocalDataByPolis(req.Polis)
                                        .GonvertToResponse();
                    if (localData == null)
                    {
                        _logger.LogInformation("Пациент не найден, либо найдено больше одного");
                        return new PersonResponse()
                        {
                            MessageData = "Пациент не найден, либо найдено больше одного",
                            SearchResult = "-1"
                        };
                    }
                    _logger.LogInformation("Данные были найдены в локальной БД");
                    _logger.LogInformation("Пациент верифицирован");
                    return localData;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Ошибка во время выполнения запроса: {ex.Message}");
                    return new PersonResponse()
                    {
                        MessageData = "Пациент не найден, либо найдено больше одного",
                        SearchResult = "-1"
                    };
                }
            }
        }

        /// <summary>
        /// Выполняет запис в КЭШ и в БазуДанных
        /// </summary>
        /// <param name="fomsData"></param>
        private void PerformDataSaving(PersonResponse fomsData)
        {
            //Асинхронно записываем данные в кэш
            Task.Run(() => _cacheService.UpdateCache(fomsData));
            //Асинхронно записываем данные в БД
            _personResponse.UploadToDBAsync(fomsData.ConvertToDBModel());
        }
    }
}
