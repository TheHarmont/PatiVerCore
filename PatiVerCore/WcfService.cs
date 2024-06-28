using CoreWCF;
using CoreWCF.Channels;
using PatiVerCore.Abstract;
using PatiVerCore.DataLayer.Abstract;
using PatiVerCore.ServiceLayer.FomsService.Model.Request;
using PatiVer;

namespace PatiVerCore
{
    //[ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    //[ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    public class WcfService(IPatiVerManager patiVerManager) : IWcfService
    {
        public PersonResponse GetPersonInfo_FIO(string moId, string surname, string firstname, string patronymic, string birthday, string username, string password, bool isIPRAfirst, int MIS)
        {
            return patiVerManager.GetPersonByFio(new PersonRequestFIO { MoId = moId, Surname = surname, Firstname = firstname, Patronymic = patronymic, Birthday = birthday, Username = username, Password = password, IsIPRAfirst = isIPRAfirst, MIS = MIS });
        }

        public PersonResponse GetPersonInfo_SNILS(string moId, string snils, string username, string password, bool isIPRAfirst, int MIS)
        {
            return patiVerManager.GetPersonBySnils(new PersonRequestSNILS { MoId = moId, Snils = snils, Username = username, Password = password, IsIPRAfirst = isIPRAfirst, MIS = MIS });
        }

        public PersonResponse GetPersonInfo_Polis(string moId, string polis, string username, string password, bool isIPRAfirst, int MIS)
        {
            return patiVerManager.GetPersonByPolis(new PersonRequestPolis { MoId = moId, Polis = polis, Username = username, Password = password, IsIPRAfirst = isIPRAfirst, MIS = MIS });
        }
    }
}
