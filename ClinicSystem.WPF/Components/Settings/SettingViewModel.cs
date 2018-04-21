using ClinicSystem.DataType.Base;
using ClinicSystem.DataType.Base.Command;
using ClinicSystem.DataType.Base.Localization;
using ClinicSystem.Model;
using ClinicSystem.Model.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ClinicSystem.WPF.Components.Settings
{
    public class SettingViewModel : ViewModelBase<SettingView>, ISetting
    {
        public SettingViewModel(ICultureRepository cultureRepository)
        {
            CultureRepository = cultureRepository;

            LanguagesList = cultureRepository.GetItems().Result.ToList();
            SelectedLanguage = LanguagesList.First(e => e.IsCurrent);
        }

        private List<Culture> _languagesList;
        public List<Culture> LanguagesList
        {
            get { return _languagesList; }
            set { _languagesList = value; OnPropertyChanged(); }
        }


        private Culture _selectedLanguage;
        public Culture SelectedLanguage
        {
            get { return _selectedLanguage; }
            set { _selectedLanguage = value; OnPropertyChanged(); }
        }

        public ICultureRepository CultureRepository { get; }

        public ICommand SaveCommand => new RelayCommand(async () =>
        {
            await CultureRepository.SetCurrentCulture(SelectedLanguage);
        });
    }
}
