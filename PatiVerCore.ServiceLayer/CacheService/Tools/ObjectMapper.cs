using PatiVer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatiVerCore.ServiceLayer.CacheService.Tools
{
    internal class ObjectMapper
    {
        /// <summary>
        /// Возвращает массив трех ключей по ФИО, ПОЛИСу и СНИЛСу
        /// </summary>
        public string[] GetObjectKeys(PersonResponse obj)
        {
            return new string[] {
                    obj.PatientData.Name?.ToLower().Trim() +
                    obj.PatientData.Surname?.ToLower().Trim() +
                    obj.PatientData.Patronymic?.ToLower().Trim() +
                    obj.PatientData.BirthDate,
                    obj.PatientData.Snils,
                    obj.PolisData.Num
            };
        }


    }
}
