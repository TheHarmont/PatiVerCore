using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PatiVer
{
    [Serializable]
    public class PatientAttachment
    {

        [XmlElement(IsNullable = false)]
        public string CodeMO;

        [XmlElement(IsNullable = true)]
        public string Sector;

        [XmlElement(IsNullable = true)]
        public string SectorName;

        [XmlElement(IsNullable = true)]
        public string SectorType;

        [XmlElement(IsNullable = false)]
        public string Type;

        [XmlElement(IsNullable = false)]
        public DateTime? BeginDate;

        [XmlElement(IsNullable = true)]
        public Nullable<DateTime> EndDate;

        [XmlElement(IsNullable = false)]
        public string Reason;

        [XmlElement(IsNullable = true)]
        public string DetachReason;

        [XmlElement(IsNullable = true)]
        public string DoctorSnils;

        public PatientAttachment()
        {
        }

        public PatientAttachment(Foms.AttachmentData data)
        {
            this.CodeMO = data.CodeMO;
            this.Sector = data.Region;
            this.SectorName = data.RegionName;
            this.SectorType = data.RegionType;
            this.Type = data.AttachType;
            this.Reason = data.AttachReason;
            this.DetachReason = data.DetachReason;
            this.DoctorSnils = data.DoctorSnils;

            DateTime date;

            if (DateTime.TryParse(data.AttachBeginDate, out date))
                this.BeginDate = date;
            if (DateTime.TryParse(data.AttachEndDate, out date))
                this.EndDate = date;
        }

        static public PatientAttachment FromFomsData(Foms.AttachmentData data)
        {
            return (null == data) ? null : new PatientAttachment(data);
        }
    }
}
