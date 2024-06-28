using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PatiVer
{
    [Serializable]
    //[DataContract(Namespace = Constants.Namespace)]
    public class Address
    {
        [XmlElement(IsNullable = true)]
        public string Kladr;

        [XmlElement(IsNullable = true)]
        public string Region;

        [XmlElement(IsNullable = true)]
        public string SubRegion;

        [XmlElement(IsNullable = true)]
        public string City;

        [XmlElement(IsNullable = true)]
        public string Street;

        [XmlElement(IsNullable = true)]
        public string House;

        [XmlElement(IsNullable = true)]
        public string Corpus;

        [XmlElement(IsNullable = true)]
        public string Flat;

        public Address()
        {
        }


        public Address(Foms.PersonAddress data)
        {
            this.Kladr = data.Address;
            this.Region = data.Region;
            this.SubRegion = data.SubRegion;
            this.City = data.City;
            this.Street = data.Street;
            this.House = data.House;
            this.Corpus = data.Corpus;
            this.Flat = data.Flat;
        }

        static public Address FromFomsData(Foms.PersonAddress data)
        {
            return (null == data) ? null : new Address(data);
        }
    }
}
