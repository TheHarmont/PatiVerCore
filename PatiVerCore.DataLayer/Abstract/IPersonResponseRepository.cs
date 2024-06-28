using PatiVerCore.DataLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatiVerCore.DataLayer.Abstract
{
    public interface IPersonResponseRepository
    {
        /// <summary>
        /// Возвращает всех пользователей из базы данных, актуальных за последние 7 дней.
        /// </summary>
        public IQueryable<PersonResponseModel> GetAllActualPerson();

        /// <summary>
        /// Возвращает единственного пользователя по данными ФИО. Если найдено больше 1, то вернет null
        /// </summary>
        public LocalData GetLocalDataByFio(string firstName, string lastName, string patronymic, DateTime birthDate);

        /// <summary>
        /// Возвращает единственного пользователя по данным СНИЛС. Если найдено больше 1, то вернет null
        /// </summary>
        public LocalData GetLocalDataBySnils(string snils);

        /// <summary>
        /// Возвращает единственного пользователя по данным ПОЛИС. Если найдено больше 1, то вернет null
        /// </summary>
        public LocalData GetLocalDataByPolis(string polis);

        /// <summary>
        /// Сохраняет запись в базе данных
        /// </summary>
        public Task UploadToDBAsync(PersonResponseModel model);

        /// <summary>
        /// Доавить в базу соответствующую запись
        /// </summary>
        public void UploadToDB(PersonResponseModel model);
    }
}
