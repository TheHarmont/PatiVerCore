using Foms;
using PatiVerCore.ServiceLayer.FomsService.Abstract;
using PatiVerCore.ServiceLayer.FomsService.Model.Request;
using PatiVer;
using PatiVerCore.ServiceLayer.FomsService.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatiVerCore.ServiceLayer.FomsService
{
    public class FomsService : IFomsService
    {
        private readonly MiacBDZServiceIdentClient Foms;

        public FomsService()
        {
            Foms = new MiacBDZServiceIdentClient();
        }

        public PersonResponse GetPersonInfo_FIO(PersonRequestFIO req)
        {
            //Запрос в ФОМС по ФИО
            var fomsData = Foms.GetPersonInfo_FIO(
                req.MoId, req.Surname,
                req.Firstname,
                req.Patronymic,
                DateTime.Parse(req.Birthday).ToString("yyyy-MM-dd"),
                req.Username,
                req.Password,
                req.IsIPRAfirst,
                req.MIS);

            //Если вернулось что-угодно, но не данные пациента
            if (fomsData.Result != "1") return new PersonResponse() { SearchResult = fomsData.Result };

            var result = fomsData.ConvertToResponse();

            //Дополняем данными из запроса
            result.PatientData.Surname = req.Surname;
            result.PatientData.Name = req.Firstname;
            result.PatientData.Patronymic = req.Patronymic;
            result.PatientData.BirthDate = DateTime.Parse(req.Birthday);

            result.CreateDate = DateTime.Now;

            return result;
        }

        public PersonResponse GetPersonInfo_SNILS(PersonRequestSNILS req)
        {
            //Запрос в ФОМС по СНИЛСу
            var fomsData = Foms.GetPersonInfo_SNILS(
                req.MoId,
                req.Snils,
                req.Username,
                req.Password,
                req.IsIPRAfirst,
                req.MIS);

            //Если вернулось что-угодно, но не данные пациента
            if (fomsData.Result != "1") return new PersonResponse() { SearchResult = fomsData.Result };
            var result = fomsData.ConvertToResponse();

            result.CreateDate = DateTime.Now;

            return result;
        }

        public PersonResponse GetPersonInfo_Polis(PersonRequestPolis req)
        {
            //Запрос в ФОМС по ПОЛИСу
            var fomsData = Foms.GetPersonInfo_Polis(
                req.MoId,
                req.Polis,
                req.Username,
                req.Password,
                req.IsIPRAfirst,
                req.MIS);

            //Если вернулось что-угодно, но не данные пациента
            if (fomsData.Result != "1") return new PersonResponse() { SearchResult = fomsData.Result };

            var result = fomsData.ConvertToResponse();

            result.CreateDate = DateTime.Now;

            return result;
        }
    }
}
