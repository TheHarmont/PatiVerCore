using PatiVerCore.ServiceLayer.FomsService.Model.Request;
using PatiVer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatiVerCore.ServiceLayer.FomsService.Abstract
{
    public interface IFomsService
    {
        /// <summary>
        /// Возвращает данные из ФОМС при запросе по ФИО
        /// </summary>
        PersonResponse GetPersonInfo_FIO(PersonRequestFIO req);

        /// <summary>
        /// Возвращает данные из ФОМС при запросе по СНИЛСу
        /// </summary>
        PersonResponse GetPersonInfo_SNILS(PersonRequestSNILS req);

        /// <summary>
        /// Возвращает данные из ФОМС при запросе по ПОЛИСу
        /// </summary>
        PersonResponse GetPersonInfo_Polis(PersonRequestPolis req);
    }
}
