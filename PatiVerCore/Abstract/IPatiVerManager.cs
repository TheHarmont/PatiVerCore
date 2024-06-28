using PatiVerCore.ServiceLayer.FomsService.Model.Request;
using PatiVer;

namespace PatiVerCore.Abstract
{
    public interface IPatiVerManager
    {
        /// <summary>
        /// Возвращает данные о пациенте либо из КЭШа, ФОМСа или локальной базы.
        /// Использует запрос по ФИО
        /// Дополнительно выполняет запись в кэш/базу для актуальных записей
        /// </summary>
        public PersonResponse GetPersonByFio(PersonRequestFIO req);

        /// <summary>
        /// Возвращает данные о пациенте либо из КЭШа, ФОМСа или локальной базы.
        /// Использует запрос по СНИЛСу
        /// Дополнительно выполняет запись в кэш/базу для актуальных записей
        /// </summary>
        public PersonResponse GetPersonBySnils(PersonRequestSNILS req);

        /// <summary>
        /// Возвращает данные о пациенте либо из КЭШа, ФОМСа или локальной базы.
        /// Использует запрос по ПОЛИСу
        /// Дополнительно выполняет запись в кэш/базу для актуальных записей
        /// </summary>
        public PersonResponse GetPersonByPolis(PersonRequestPolis req);
    }
}
