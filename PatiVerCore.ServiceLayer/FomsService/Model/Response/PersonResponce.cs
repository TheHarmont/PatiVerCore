using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PatiVer //Установил такое ПИ, чтобы обеспечить гладкий переход со старого PatiVer
{
    [Serializable]
    //[DataContract(Namespace = Constants.Namespace)]
    public class PersonResponse
    {
        [XmlElement(IsNullable = false)]
        public string SearchResult;

        [XmlElement(IsNullable = true)]
        public PatientData PatientData;

        [XmlElement(IsNullable = true)]
        public PatientAttachment AttachmentData;

        [XmlElement(IsNullable = true)]
        public Polis PolisData;

        [XmlElement(IsNullable = true)]
        public string MessageData;

        [XmlIgnore]
        [NonSerialized]
        public DateTime CreateDate;
    }
}
