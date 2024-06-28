using CoreWCF;
using PatiVer;

namespace PatiVerCore.Abstract
{
    //[ServiceContract(SessionMode = SessionMode.Required)]
    [ServiceContract]
    public interface IWcfService
    {
        [OperationContract]
        public PersonResponse GetPersonInfo_FIO(string moId, string surname, string firstname, string patronymic, string birthday, string username, string password, bool isIPRAfirst, int MIS);

        [OperationContract]
        public PersonResponse GetPersonInfo_SNILS(string moId, string snils, string username, string password, bool isIPRAfirst, int MIS);

        [OperationContract]
        public PersonResponse GetPersonInfo_Polis(string moId, string polis, string username, string password, bool isIPRAfirst, int MIS);
    }
}
