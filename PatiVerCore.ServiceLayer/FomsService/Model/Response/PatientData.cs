using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PatiVer
{
    [Serializable]
    public class PatientData
    {
        [XmlElement(IsNullable = false)]
        public string FomsId;

        [XmlElement(IsNullable = false)]
        public string ENP;

        [XmlElement(IsNullable = false)]
        public string Surname;

        [XmlElement(IsNullable = false)]
        public string Sex;

        [XmlElement(IsNullable = false)]
        public string Name;

        [XmlElement(IsNullable = true)]
        public string Patronymic;

        [XmlElement(IsNullable = true)]
        public DateTime? BirthDate;

        [XmlElement(IsNullable = true)]
        public string Snils;

        [XmlElement(IsNullable = true)]
        public string BirthPlace;

        [XmlElement(IsNullable = true)]
        public string Citizenship;

        [XmlElement(IsNullable = true)]
        public string DocumentType;

        [XmlElement(IsNullable = true)]
        public string DocumentSeries;

        [XmlElement(IsNullable = true)]
        public string DocumentNumber;

        [XmlElement(IsNullable = true)]
        public string DocumentOrg;

        [XmlElement(IsNullable = true)]
        public Nullable<DateTime> DocumentDate;

        [XmlElement(IsNullable = true)]
        public Address RegAddress;

        [XmlElement(IsNullable = true)]
        public string Phone;




        public PatientData()
        {
        }

        public PatientData(Foms.PersonData data)
        {
            this.FomsId = data.PersonId;
            this.ENP = data.PersonENP;
            this.Surname = data.PersonSurname;
            this.Name = data.PersonFirstname;
            this.Patronymic = data.PersonSecname;
            this.Sex = data.PersonSex;

            DateTime date;
            if (DateTime.TryParse(data.PersonBirthday, out date))
                this.BirthDate = date;
            this.Snils = data.PersonSNILS;

            this.BirthPlace = data.PersonBirthplace;
            this.Citizenship = data.PersonCitizenship;

            this.DocumentType = data.DocumentType;
            this.DocumentSeries = data.DocumentSer;
            this.DocumentNumber = data.DocumentNum;
            if (DateTime.TryParse(data.DocumentDate, out date))
                this.DocumentDate = date;

            this.RegAddress = Address.FromFomsData(data.personAddress);
            this.DocumentOrg = data.DocumentOrg;

            this.Phone = data.PersonPhone;
        }

        static public PatientData FromFomsData(Foms.PersonData data)
        {
            return (null == data) ? null : new PatientData(data);
        }
    }
}
