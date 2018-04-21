using ClinicSystem.DataType.Base;
using ClinicSystem.DataType.Base.Command;
using ClinicSystem.DataType.Services.DialogService;
using ClinicSystem.Model.RepositoryInterface;
using ClinicSystem.WPF.Components.PatientInformaiton;
using ClinicSystem.WPF.Components.PatientsList;
using ClinicSystem.WPF.Components.Settings;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace ClinicSystem.WPF
{
    public class ShellViewModel : ViewModelBase
    {
        public IPatientRepository PatientRepository;

        public ShellViewModel()
        {
            
        }

        public ShellViewModel(IPatientsList patientsList, IPatientRepository patientRepository
            , IPatientInformation patientInformation, IUnityContainer container)
        {
            if (IsDesignMode())
            {
                MainWindowVisible = false;
                SettingVisible = true;
            }
            PatientRepository = patientRepository;

            MainWindowVisible = true;
            SettingVisible = false;

            InitializePatientsList(patientsList);
            PatientInformaiton = (UserControl)patientInformation.GetView();
            patientsList.OnSelectPatient += (sender, e) =>
            {
                patientInformation.ViewPatientDetail(e, DataType.Interfaces.OperationType.View);
            };
        }


        private void InitializePatientsList(IPatientsList patientsList)
        {
            var patients = PatientRepository.GetItems().ContinueWith(e =>
            {
                patientsList.LoadPatients(e.Result.ToList());
            });

            PatientsListUC = (UserControl)patientsList.GetView();
        }


        private UserControl _patientsListUC;
        public UserControl PatientsListUC
        {
            get { return _patientsListUC; }
            set { _patientsListUC = value; OnPropertyChanged(); }
        }

        private UserControl _patientInformaiton;
        public UserControl PatientInformaiton
        {
            get { return _patientInformaiton; }
            set { _patientInformaiton = value; OnPropertyChanged(); }
        }

        private UserControl _settingUC;
        public UserControl SettingUC
        {
            get { return _settingUC; }
            set { _settingUC = value; OnPropertyChanged(); }
        }

        private bool _mainWindowVisible;
        public bool MainWindowVisible
        {
            get { return _mainWindowVisible; }
            set { _mainWindowVisible = value; OnPropertyChanged(); }
        }

        private bool _settingVisible;
        public bool SettingVisible
        {
            get { return _settingVisible; }
            set { _settingVisible = value; OnPropertyChanged(); }
        }


        public ICommand ExitCommand => new RelayCommand(() => App.Current.Shutdown());
        public ICommand SettingCommand => new RelayCommand(() =>
        {
            SettingVisible = true;
            MainWindowVisible = false;
            SettingUC = (UserControl)Container.Resolve<ISetting>().GetView();
        });
        public ICommand HomeCommand => new RelayCommand(() =>
        {
            MainWindowVisible = true;
            SettingVisible = false;
        });

    }
}
