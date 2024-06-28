using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PatiVer
{
    [Serializable]
    public class Polis
    {
        [XmlElement(IsNullable = false)]
        public string Num;

        [XmlElement(IsNullable = false)]
        public string Type;

        [XmlElement(IsNullable = false)]
        public DateTime BeginDate;

        [XmlElement(IsNullable = true)]
        public Nullable<DateTime> EndDate;

        [XmlElement(IsNullable = true)]
        public Nullable<DateTime> CloseDate;

        [XmlElement(IsNullable = false)]
        public string SMO;

        [XmlElement(IsNullable = true)]
        public string CloseReason;

        public Polis()
        {
        }

        public Polis(Foms.PolisData data)
        {
            this.Num = data.PolisNum;
            this.Type = data.PolisType;
            this.SMO = data.PolisSMO;
            this.CloseReason = data.PolisCloseReason;

            DateTime date;

            if (DateTime.TryParse(data.PolisBeginDate, out date))
                this.BeginDate = date;
            if (DateTime.TryParse(data.PolisEndDate, out date))
                this.EndDate = date;
            if (DateTime.TryParse(data.PolisCloseDate, out date))
                this.CloseDate = date;
        }

        static public Polis FromFomsData(Foms.PolisData data)
        {
            return (null == data) ? null : new Polis(data);
        }
    }
}
