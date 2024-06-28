using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PatiVerCore.DataLayer.Abstract;
using PatiVerCore.DataLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatiVerCore.DataLayer.DAL
{
    public class PersonResponseRepository(ILogger<PersonResponseRepository> logger,PatiVerContext db) : IPersonResponseRepository
    {
        public IQueryable<PersonResponseModel> GetAllActualPerson()
        {
            var actualData = DateTime.Now.AddDays(-7);
            return db.PersonsResponces
                .AsNoTracking()
                .Where(x => x.dateAdd >= actualData)
                .AsQueryable();
        }

        public LocalData GetLocalDataByFio(string firstName, string lastName, string patronymic, DateTime birthDate)
        {
            var strBirthDate = birthDate.ToString("yyyy.MM.dd");
            var result = db.FomsLocalData
                .Where(x =>
                x.Lastname.ToLower() == lastName.ToLower() &&
                x.Firstname.ToLower() == firstName.ToLower() &&
                x.Patronymic.ToLower() == patronymic.ToLower() &&
                x.Birthdate == strBirthDate);
            if (result.Count() > 1) return null;
            return result.FirstOrDefault();
        }

        public LocalData GetLocalDataBySnils(string snils)
        {

            var result = db.FomsLocalData
            .AsNoTracking()
            .Where(x => x.Snils.ToLower() == snils);

            if (result.Count() > 1) return null;
            return result.FirstOrDefault();

        }

        public LocalData GetLocalDataByPolis(string polis)
        {

            var result = db.FomsLocalData
            .AsNoTracking()
            .Where(x => x.Polis.ToLower() == polis);

            if (result.Count() > 1) return null;
            return result.FirstOrDefault();

        }

        public async Task UploadToDBAsync(PersonResponseModel model)
        {
            try
            {
                await db.PersonsResponces.AddAsync(model);
                await db.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                logger.LogError("Ошибка при добавлении записи в БД: " + ex.Message);
            }
        }

        public void UploadToDB(PersonResponseModel model)
        {
            try
            {
                db.PersonsResponces.Add(model);
                db.SaveChanges();

            }
            catch (Exception ex)
            {
                logger.LogError("Ошибка при добавлении записи в БД: " + ex.Message);
            }
        }
    }
}
