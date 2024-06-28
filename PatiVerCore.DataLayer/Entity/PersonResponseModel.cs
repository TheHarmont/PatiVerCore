using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatiVerCore.DataLayer.Entity
{
    public class PersonResponseModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime dateAdd { get; set; }

        public string? SearchResult { get; set; }

        #region patientData
        public string? FomsId { get; set; }

        public string? ENP { get; set; }

        public string? Surname { get; set; }

        public string? Sex { get; set; }

        public string? Name { get; set; }

        public string? Patronymic { get; set; }

        public DateTime? BirthDate { get; set; }

        public string? Snils { get; set; }

        public string? BirthPlace { get; set; }

        public string? Citizenship { get; set; }

        public string? DocumentType { get; set; }

        public string? DocumentSeries { get; set; }

        public string? DocumentNumber { get; set; }

        public string? DocumentOrg { get; set; }

        public DateTime? DocumentDate { get; set; }

        #region Adress
        public string? Kladr { get; set; }

        public string? Region { get; set; }

        public string? SubRegion { get; set; }

        public string? City { get; set; }

        public string? Street { get; set; }

        public string? House { get; set; }

        public string? Corpus { get; set; }

        public string? Flat { get; set; }
        #endregion Adress

        #endregion patientData

        #region patientAtachment
        public string? CodeMO { get; set; }

        public string? Sector { get; set; }

        public string? SectorName { get; set; }

        public string? SectorType { get; set; }

        public string? Type { get; set; }

        public DateTime? BeginDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string? Reason { get; set; }

        public string? DetachReason { get; set; }

        public string? DoctorSnils { get; set; }
        public string? Phone { get; set; }
        #endregion patientAtachment

        #region polis
        public string? PolisNum { get; set; }

        public string? PolisType { get; set; }

        public DateTime PolisBeginDate { get; set; }

        public DateTime? PolisEndDate { get; set; }

        public DateTime? PolisCloseDate { get; set; }

        public string? PolisSMO { get; set; }

        public string? PolisCloseReason { get; set; }
        #endregion polis

        public string? MessageData { get; set; }

    }
}
