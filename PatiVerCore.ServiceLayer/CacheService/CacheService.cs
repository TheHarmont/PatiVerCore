using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PatiVerCore.DataLayer.Abstract;
using PatiVerCore.DataLayer.DAL;
using PatiVerCore.ServiceLayer.CacheService.Abstract;
using PatiVerCore.ServiceLayer.CacheService.Tools;
using PatiVer;
using PatiVerCore.ServiceLayer.FomsService.Tools;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatiVerCore.ServiceLayer.CacheService
{
    public class CacheService : ICacheService
    {
        private readonly ILogger<CacheService> _logger;

        private readonly IServiceScopeFactory _serviceScopeFactory; //Решение проблемы с внедрением Scoped зависимостей в Singleton
        private ConcurrentDictionary<string, PersonResponse> PersonResponseCache;

        public CacheService(ILogger<CacheService> logger, IServiceScopeFactory serviceScopeFactory)
        {
            _logger = logger;

            _serviceScopeFactory = serviceScopeFactory;
            PersonResponseCache = new ConcurrentDictionary<string, PersonResponse>();
        }

        public void InitializeCache()
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var personResponseRepository = scope.ServiceProvider.GetRequiredService<IPersonResponseRepository>();
                var data = personResponseRepository.GetAllActualPerson().GonvertToResponse();

                foreach (var person in data)
                {
                    UpdateCache(person);
                }

               _logger.LogDebug("Произведена инициализация КЭШа. Количество записей: " + PersonResponseCache.Count());
            }
        }

        public void UpdateCache(PersonResponse entity)
        {
            var mapper = new ObjectMapper();
            var keys = mapper.GetObjectKeys(entity);
            if (keys != null) 
            {
                keys.Where(key => !string.IsNullOrEmpty(key))
                    .ToList()
                    .ForEach(key =>
                    {
                        if (PersonResponseCache.ContainsKey(key) && PersonResponseCache[key].CreateDate < entity.CreateDate)
                        {
                            PersonResponseCache.TryUpdate(key, entity, PersonResponseCache[key]);
                        }
                        else
                        {
                            PersonResponseCache.TryAdd(key, entity);
                        }
                    });
            }
        }

        public bool FindCacheById(string key, out PersonResponse person)
        {
            person = null;
            if (PersonResponseCache.ContainsKey(key))
            {
                person = PersonResponseCache[key];
                if (person.CreateDate > DateTime.Now.AddDays(-7))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
