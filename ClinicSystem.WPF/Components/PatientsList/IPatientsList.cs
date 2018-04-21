using ClinicSystem.DataType.Interfaces;
using ClinicSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicSystem.WPF.Components.PatientsList
{
    public interface IPatientsList : IPresentable
    {
        void LoadPatients(IList<Patient> patients);
        void AddPatient(Patient patient);
        void RemovePatient(Patient patient);
        void RemovePatient(int patientId);
        void UpdatePatient(Patient patient);

        event EventHandler<Patient> OnSelectPatient;
    }
}
