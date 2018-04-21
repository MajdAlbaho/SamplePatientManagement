using ClinicSystem.DataType.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicSystem.Model
{
    public class VisitConsultation : IUniqueObject<int>
    {
        public int Id { get; set; }
        public int PatientVisitId { get; set; }
        public int ClinicId { get; set; }
        public int DoctorId { get; set; }

        public int GetKey() => Id;
    }
}
