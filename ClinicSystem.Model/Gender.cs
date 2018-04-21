using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicSystem.Model
{
    public class Gender
    {
        public int Id { get; set; }
        public string EnName { get; set; }
        public string ArName { get; set; }

        public static List<Gender> LoadDefaults()
        {
            return new List<Gender>()
            {
                new Gender() { Id = 0, EnName = "FeMale", ArName = "انثى"},
                new Gender() { Id = 1, EnName = "Male", ArName = "ذكر"}
            };
        }
    }
}
