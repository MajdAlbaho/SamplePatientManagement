using ClinicSystem.DataType.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicSystem.Model
{
    public class Culture : IUniqueObject<int>
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public bool IsCurrent { get; set; }

        public string CultureName => Code == "ar-SY" ? "العربية" : "English";

        public int GetKey() => Id;
    }
}
