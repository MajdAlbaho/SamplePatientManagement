using ClinicSystem.DataType.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicSystem.Model
{
    public class Diagnosis : IUniqueObject<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }


        public int GetKey() => Id;
    }
}
