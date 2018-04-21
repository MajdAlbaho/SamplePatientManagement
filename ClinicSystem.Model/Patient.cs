using ClinicSystem.DataType.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicSystem.Model
{
    public class Patient : Person, IUniqueObject<int>
    {
        private int personId;
        public int PersonId
        {
            get { return personId; }
            set { personId = Id = value; }
        }

        public DateTime RegistrationDate { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public int MedicalPointId { get; set; }

        public new int GetKey() => PersonId;

        public string FullName => $"{FirstName} {LastName}";
    }
}
