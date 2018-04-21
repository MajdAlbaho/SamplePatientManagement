using ClinicSystem.DataType.Base;
using ClinicSystem.DataType.Base.Command;
using ClinicSystem.DataType.Extenstions;
using ClinicSystem.DataType.Services.DialogService;
using ClinicSystem.Model;
using ClinicSystem.Model.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using System.Windows.Input;
using Microsoft.Practices.Unity;
using ClinicSystem.WPF.Components.PatientInformaiton;
using System.Windows.Controls;
using ClinicSystem.SignalR;
using Microsoft.AspNet.SignalR.Client;
using Newtonsoft.Json;
using ClinicSystem.DataType.Base.Localization;

namespace ClinicSystem.WPF.Components.PatientsList
{
    public class PatientListViewModel : ViewModelBase<PatientListView>, IPatientsList
    {
        public PatientListViewModel(IPatientRepository patientRepository)
        {
            SubscribeToServer();

            AddPatientCommand = new RelayCommand(AddPatient);
            EditPatientCommand = new RelayCommand(EditPatient);
            DeletePatientCommand = new RelayCommand(DeletePatient);
            SearchCommand = new RelayCommand(SearchPatients);

            PatientRepository = patientRepository;

            _hub.On(ClientMethods.AddPatient, serializedPatient =>
            {
                Patient deSerializedPatient = JsonConvert.DeserializeObject<Patient>(serializedPatient);
                App.Current.Dispatcher.Invoke(() => PatientsList.Add(deSerializedPatient));
            });
            _hub.On(ClientMethods.DeletePatient, patientId =>
            {
                App.Current.Dispatcher.Invoke(() => PatientsList.Remove(PatientsList.First(e => e.Id == patientId)));
            });
            _hub.On(ClientMethods.EditPatient, serializedPatient =>
            {
                Patient deSerializedPatient = JsonConvert.DeserializeObject<Patient>(serializedPatient);
                int index = PatientsList.IndexOf(PatientsList.First(e => e.Id == deSerializedPatient.Id));

                App.Current.Dispatcher.Invoke(() =>
                {
                    PatientsList.RemoveAt(index);
                    PatientsList.Insert(index, deSerializedPatient);
                });
            });
        }

        public event EventHandler<Patient> OnSelectPatient;


        private ObservableCollection<Patient> _patientsList;
        public ObservableCollection<Patient> PatientsList
        {
            get { return _patientsList; }
            set
            {
                _patientsList = value; OnPropertyChanged();
                App.Current.Dispatcher.Invoke(() => PatientsListView = CollectionViewSource.GetDefaultView(value));
            }
        }

        private ICollectionView _patientsListView;
        public ICollectionView PatientsListView
        {
            get { return _patientsListView; }
            set { _patientsListView = value; OnPropertyChanged(); }
        }

        private Patient _selectedPatient;
        public Patient SelectedPatient
        {
            get { return _selectedPatient; }
            set
            {
                _selectedPatient = value; OnPropertyChanged();
                if (value != null)
                    OnSelectPatient?.Invoke(this, value);
            }
        }

        private string _textSearch;
        public string SearchText
        {
            get { return _textSearch; }
            set { _textSearch = value; OnPropertyChanged(); }
        }



        public void LoadPatients(IList<Patient> patients)
        {
            PatientsList = new ObservableCollection<Patient>(patients);
        }

        public async void AddPatient(Patient patient)
        {
            if (PatientsList.IsNullOrEmpty())
                PatientsList = new ObservableCollection<Patient>();
            else
            {
                if (PatientsList.Any(e => e.PersonId == patient.PersonId))
                {
                    // Log patient exist
                    return;
                }
            }

            PatientsList.Add(patient);

            if (SignalRConnected)
                await _hub.Invoke(ClientMethods.AddPatient, JsonConvert.SerializeObject(patient));
        }

        public void RemovePatient(Patient patient)
        {
            if (PatientsList.IsNullOrEmpty() || !PatientsList.Any(e => e.PersonId == patient.PersonId))
            {
                // Log patient not exist
                return;
            }

            RemovePatient(patient.PersonId);

        }

        public void RemovePatient(int patientId)
        {
            PatientsList.Remove(PatientsList.First(e => e.PersonId == patientId));
            if (SignalRConnected)
                _hub.Invoke(ClientMethods.DeletePatient, patientId);
        }

        public void UpdatePatient(Patient patient)
        {
            RemovePatient(patient);

            AddPatient(patient);

            if (SignalRConnected)
                _hub.Invoke(ClientMethods.EditPatient, JsonConvert.SerializeObject(patient));
        }

        public ICommand AddPatientCommand { get; }
        public ICommand EditPatientCommand { get; }
        public ICommand DeletePatientCommand { get; }
        public ICommand SearchCommand { get; set; }
        public IPatientRepository PatientRepository { get; }


        public void AddPatient()
        {
            var infoDialog = Container.Resolve<PatientInformationViewModel>();
            Container.Resolve<IDialogService>().ShowEditorDialog(((UserControl)infoDialog.GetView()), async () =>
             {
                 var result = await infoDialog.Save();
                 if (result.Successed)
                     AddPatient(result.Item);

                 return result.Successed;
             });
        }

        public void EditPatient()
        {
            if (SelectedPatient != null)
            {
                try
                {
                    var infoDialog = Container.Resolve<PatientInformationViewModel>();
                    infoDialog.ViewPatientDetail(SelectedPatient, DataType.Interfaces.OperationType.Edit);

                    Container.Resolve<IDialogService>().ShowEditorDialog(((UserControl)infoDialog.GetView()), async () =>
                    {
                        var result = await infoDialog.Save();
                        if (result.Successed)
                            UpdatePatient(result.Item);

                        return result.Successed;
                    });
                }
                catch (Exception ex)
                {
                    // Log exception
                    System.Windows.Forms.MessageBox.Show(ex.Message);
                }
            }
        }

        public void DeletePatient()
        {
            if (SelectedPatient != null)
            {
                try
                {
                    PatientRepository.DeleteItem(SelectedPatient);
                    RemovePatient(SelectedPatient);
                }
                catch (Exception ex)
                {
                    // Log exception
                    System.Windows.Forms.MessageBox.Show(ex.Message);
                }
            }
        }

        public void SearchPatients()
        {
            if (SearchText == null)
                return;

            PatientsListView.Filter = item =>
            {
                var result = (Patient)item;
                return result != null && (result.FirstName.ToLower().Contains(SearchText) ||
                           result.LastName.ToLower().Contains(SearchText));
            };
        }
    }
}
