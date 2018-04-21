using ClinicSystem.DataType.Base;
using ClinicSystem.DataType.Interfaces;
using ClinicSystem.Model;
using ClinicSystem.Model.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicSystem.WPF.Components.PatientInformaiton
{
    public class PatientInformationViewModel : ViewModelBase<PatientInformationView>, IPatientInformation
    {
        public IPatientRepository PatientRepository { get; }
        public PatientInformationViewModel()
        {

        }
        public PatientInformationViewModel(IPatientRepository patientRepository)
        {
            if (IsDesignMode())
            {

            }

            PatientRepository = patientRepository;

            Initialize();
        }

        private void Initialize()
        {
            Genders = Gender.LoadDefaults();
        }

        private int _personId;

        private string _firstName;
        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; OnPropertyChanged(); }
        }

        private string _lastName;
        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; OnPropertyChanged(); }
        }

        private DateTime _birthDate;
        public DateTime BirthDate
        {
            get { return _birthDate.Date; }
            set { _birthDate = value.Date; OnPropertyChanged(); }
        }

        private List<Gender> _gender;
        public List<Gender> Genders
        {
            get { return _gender; }
            set { _gender = value; OnPropertyChanged(); }
        }

        private Gender _selectedGender;
        public Gender SelectedGender
        {
            get { return _selectedGender; }
            set { _selectedGender = value; OnPropertyChanged(); }
        }

        private DateTime _registrationDate;
        public DateTime RegistrationDate
        {
            get { return _registrationDate.Date; }
            set { _registrationDate = value.Date; OnPropertyChanged(); }
        }

        private string _address;
        public string Address
        {
            get { return _address; }
            set { _address = value; OnPropertyChanged(); }
        }

        private string _phoneNumber;
        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set { _phoneNumber = value; OnPropertyChanged(); }
        }


        public void ViewPatientDetail(Patient patient, OperationType operation)
        {
            this._personId = patient.PersonId;

            this.FirstName = patient.FirstName;
            this.LastName = patient.LastName;
            this.BirthDate = patient.BirthDate;

            this.Genders = Gender.LoadDefaults();
            this.SelectedGender = Genders[patient.Gender ? 1 : 0];

            this.RegistrationDate = patient.RegistrationDate;
            this.Address = patient.Address;
            this.PhoneNumber = patient.PhoneNumber;

            Operation = operation;
        }

        public async Task<SaveResult<Patient>> Save()
        {
            try
            {
                var patient = new Patient()
                {
                    PersonId = this._personId,
                    FirstName = FirstName,
                    LastName = LastName,
                    BirthDate = BirthDate,
                    Gender = SelectedGender.Id == 0 ? false : true,
                    RegistrationDate = RegistrationDate,
                    Address = Address,
                    PhoneNumber = PhoneNumber,
                    MedicalPointId = 0
                    //Globals.LoggedMedicalPoint.Id
                };

                Patient result = null;

                if (Operation == OperationType.Add)
                    result = await PatientRepository.InsertItem(patient);

                else if (Operation == OperationType.Edit)
                    result = await PatientRepository.UpdateItem(patient);

                return new SaveResult<Patient>(true, result, Operation);
            }
            catch (Exception ex)
            {
                // Log error
                return new SaveResult<Patient>(false, null, Operation);
            }
        }
    }
}
