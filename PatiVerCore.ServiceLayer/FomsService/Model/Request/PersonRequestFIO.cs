using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatiVerCore.ServiceLayer.FomsService.Model.Request
{
    public class PersonRequestFIO
    {
        public string MoId { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        public string Surname { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        public string Firstname { get; set; }

        /// <summary>
        /// Отчество
        /// </summary>
        public string Patronymic { get; set; }

        public string Birthday { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public bool IsIPRAfirst { get; set; }

        public int MIS { get; set; }
    }
}
