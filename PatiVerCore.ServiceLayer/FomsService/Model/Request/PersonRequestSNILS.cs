using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatiVerCore.ServiceLayer.FomsService.Model.Request
{
    public class PersonRequestSNILS
    {
        public string MoId { get; set; }

        public string Snils { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public bool IsIPRAfirst { get; set; }

        public int MIS { get; set; }
    }
}
