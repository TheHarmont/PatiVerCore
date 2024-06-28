using PatiVer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatiVerCore.ServiceLayer.CacheService.Abstract
{
    public interface ICacheService
    {
        /// <summary>
        /// Метод наполняет кэш актуальными данными из бд
        /// </summary>
        public void InitializeCache();

        /// <summary>
        /// Обновляет кэш, при условии, что такой записи нет. Иначе создаст новую.
        /// </summary>
        public void UpdateCache(PersonResponse entity);

        /// <summary>
        /// Выполняет поиск в кэше по ключу
        /// </summary>
        /// <returns>Данные по пациенту из БД</returns>
        public bool FindCacheById(string key, out PersonResponse person);
    }
}
