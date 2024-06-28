using Foms;
using PatiVerCore.DataLayer.Entity;
using PatiVer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatiVerCore.ServiceLayer.FomsService.Tools
{
    public static class ModelMapper
    {
        /// <summary>
        /// Преобразует объект ResponseData в PersonResponse
        /// </summary>
        public static PersonResponse ConvertToResponse(this ResponseData data)
        {
            return new PersonResponse()
            {
                SearchResult = data.Result,
                PatientData = PatientData.FromFomsData(data.personData),
                AttachmentData = PatientAttachment.FromFomsData(data.attachmentData),
                PolisData = Polis.FromFomsData(data.polisData),
                MessageData = data.Message
            };
        }

        /// <summary>
        /// Преобразует объект LocalData в PersonResponse
        /// </summary>
        public static PersonResponse GonvertToResponse(this LocalData data)
        {
            if (data == null) return null;

            var fomsData = new Foms.ResponseData();
            PersonResponse result = new PersonResponse()
            {
                SearchResult = fomsData.Result,
                PatientData = PatientData.FromFomsData(new Foms.PersonData()),
                AttachmentData = PatientAttachment.FromFomsData(new Foms.AttachmentData()),
                PolisData = Polis.FromFomsData(new Foms.PolisData()),
                MessageData = fomsData.Message
            };

            //Наполнение данными
            result.SearchResult = "1";
            result.AttachmentData.CodeMO = data.CodeMO.ToString();

            result.PatientData.Name = data.Firstname;
            result.PatientData.Surname = data.Lastname;
            result.PatientData.Patronymic = data.Patronymic;
            if (DateTime.TryParse(data.Birthdate, out DateTime personBirthDate)) result.PatientData.BirthDate = personBirthDate;
            if (DateTime.TryParse(data.BeginDate, out DateTime attachBeginDate)) result.AttachmentData.BeginDate = attachBeginDate;
            if (DateTime.TryParse(data.EndDate, out DateTime attachEndDate)) result.AttachmentData.EndDate = attachEndDate;

            result.PatientData.ENP = data.Polis;
            result.PatientData.Snils = data.Snils;

            return result;
        }

        /// <summary>
        /// Преобразует список объектов PersonResponseModel в PersonResponse
        /// </summary>
        public static List<PersonResponse> GonvertToResponse(this IQueryable<PersonResponseModel> data)
        {
            return data.Select(x => new PersonResponse()
            {
                SearchResult = x.SearchResult,
                AttachmentData = new PatientAttachment() { CodeMO = x.CodeMO, Sector = x.Sector, SectorName = x.SectorName, SectorType = x.SectorType, Type = x.Type, BeginDate = x.BeginDate, EndDate = x.EndDate, Reason = x.Reason, DetachReason = x.DetachReason, DoctorSnils = x.DoctorSnils },
                PatientData = new PatientData() { FomsId = x.FomsId, ENP = x.ENP, Surname = x.Surname, Sex = x.Sex, Name = x.Name, Patronymic = x.Patronymic, BirthDate = x.BirthDate, Snils = x.Snils, BirthPlace = x.BirthPlace, Citizenship = x.Citizenship, DocumentType = x.DocumentType, DocumentSeries = x.DocumentSeries, DocumentNumber = x.DocumentNumber, DocumentOrg = x.DocumentOrg, DocumentDate = x.DocumentDate, Phone = x.Phone, RegAddress = new Address() { Kladr = x.Kladr, Region = x.Region, SubRegion = x.SubRegion, City = x.City, Street = x.Street, House = x.House, Corpus = x.Corpus, Flat = x.Flat } },
                PolisData = new Polis() { Num = x.PolisNum, Type = x.PolisType, BeginDate = x.PolisBeginDate, EndDate = x.PolisEndDate, CloseDate = x.PolisCloseDate, SMO = x.PolisSMO, CloseReason = x.PolisCloseReason },
                MessageData = x.MessageData,
                CreateDate = x.dateAdd
            }).ToList();
        }

        /// <summary>
        /// Преобразует объект PersonResponse в пригодный для импорта в БД PersonResponseModel
        /// </summary>
        public static PersonResponseModel ConvertToDBModel(this PersonResponse data)
        {
            return new PersonResponseModel()
            {
                dateAdd = data.CreateDate,

                SearchResult = data.SearchResult,
                FomsId = data.PatientData.FomsId,
                ENP = data.PatientData.ENP,
                Surname = data.PatientData.Surname,
                Sex = data.PatientData.Sex,
                Name = data.PatientData.Name,
                Patronymic = data.PatientData.Patronymic,
                BirthDate = data.PatientData.BirthDate == DateTime.MinValue ? null : data.PatientData.BirthDate,
                Snils = data.PatientData.Snils,
                BirthPlace = data.PatientData.BirthPlace,
                Citizenship = data.PatientData.Citizenship,
                DocumentType = data.PatientData.DocumentType,
                DocumentSeries = data.PatientData.DocumentSeries,
                DocumentNumber = data.PatientData.DocumentNumber,
                DocumentOrg = data.PatientData.DocumentOrg,
                DocumentDate = data.PatientData.DocumentDate == DateTime.MinValue ? null : data.PatientData.DocumentDate,
                Kladr = data.PatientData.RegAddress.Kladr,
                Region = data.PatientData.RegAddress.Region,
                SubRegion = data.PatientData.RegAddress.SubRegion,
                City = data.PatientData.RegAddress.City,
                Street = data.PatientData.RegAddress.Street,
                House = data.PatientData.RegAddress.House,
                Corpus = data.PatientData.RegAddress.Corpus,
                Flat = data.PatientData.RegAddress.Flat,
                Phone = data.PatientData.Phone,
                CodeMO = data.AttachmentData.CodeMO,
                Sector = data.AttachmentData.Sector,
                SectorName = data.AttachmentData.SectorName,
                SectorType = data.AttachmentData.SectorType,
                Type = data.AttachmentData.Type,
                BeginDate = data.AttachmentData.BeginDate == DateTime.MinValue ? null : data.AttachmentData.BeginDate,
                EndDate = data.AttachmentData.EndDate == DateTime.MinValue ? null : data.AttachmentData.EndDate,
                Reason = data.AttachmentData.Reason,
                DetachReason = data.AttachmentData.DetachReason,
                DoctorSnils = data.AttachmentData.DoctorSnils,
                PolisNum = data.PolisData.Num,
                PolisType = data.PolisData.Type,
                PolisBeginDate = data.PolisData.BeginDate,
                PolisEndDate = data.PolisData.EndDate == DateTime.MinValue ? null : data.PolisData.EndDate,
                PolisCloseDate = data.PolisData.CloseDate == DateTime.MinValue ? null : data.PolisData.CloseDate,
                PolisSMO = data.PolisData.SMO,
                PolisCloseReason = data.PolisData.CloseReason,
                MessageData = data.MessageData
            };
        }
    }
}
