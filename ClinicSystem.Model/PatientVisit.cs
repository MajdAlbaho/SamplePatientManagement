using ClinicSystem.DataType.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicSystem.Model
{
    public class PatientVisit : IUniqueObject<int>
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public DateTime Date { get; set; }

        public int GetKey() => Id;
    }
}
