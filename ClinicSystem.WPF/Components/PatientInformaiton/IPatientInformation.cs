using ClinicSystem.DataType.Interfaces;
using ClinicSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicSystem.WPF.Components.PatientInformaiton
{
    public interface IPatientInformation : IPresentable
    {
        void ViewPatientDetail(Patient patient, OperationType operation);
    }
}
