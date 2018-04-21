using ClinicSystem.WPF.Components.PatientsList;
using ClinicSystem.DataType.Base.Localization;
using ClinicSystem.Linq.Repositories;
using ClinicSystem.Model.RepositoryInterface;
using Microsoft.Practices.Unity;
using System.Windows;
using Telerik.Windows.Controls;
using System.Windows.Media;
using ClinicSystem.WPF.Components.PatientInformaiton;
using ClinicSystem.DataType.Services.DialogService;
using Prism.Unity;
using Prism.Modularity;
using ClinicSystem.WPF.Components.Settings;

namespace ClinicSystem.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            SetApplicationTheme();
            new ClinicSystemBootStrapper().Run();
        }

        private void SetApplicationTheme()
        {
            Resources.Add(SystemColors.HighlightBrushKey, new SolidColorBrush(Colors.DarkGray));
            Resources.Add(typeof(SystemColors).GetProperty("InactiveSelectionHighlightBrushKey").GetValue(null, null), new SolidColorBrush(Colors.DarkGray));
            Resources.Add(typeof(SystemColors).GetProperty("InactiveSelectionHighlightTextBrushKey").GetValue(null, null), new SolidColorBrush(Colors.White));

            StyleManager.ApplicationTheme = new Windows8Theme();
            Windows8Palette.Palette.AccentColor = (Color)ColorConverter.ConvertFromString("#FF006838");
            Windows8Palette.Palette.MainColor = (Color)ColorConverter.ConvertFromString("#FFFFFFFF");
            Windows8Palette.Palette.BasicColor = (Color)ColorConverter.ConvertFromString("#FFCCCCCC");
            Windows8Palette.Palette.StrongColor = (Color)ColorConverter.ConvertFromString("#FF434343");
            Windows8Palette.Palette.MarkerColor = (Color)ColorConverter.ConvertFromString("#FF2F2F2F");
        }

        public class ClinicSystemBootStrapper : UnityBootstrapper
        {
            protected override void ConfigureModuleCatalog()
            {
                base.ConfigureModuleCatalog();
                var moduleCatalog = (ModuleCatalog)ModuleCatalog;
                moduleCatalog.AddModule(typeof(DialogServiceModule));
            }

            protected override void InitializeShell()
            {
                shell.Show();
            }

            protected override DependencyObject CreateShell()
            {
                Container.RegisterType<IPatientsList, PatientListViewModel>();
                Container.RegisterType<IPatientInformation, PatientInformationViewModel>();
                Container.RegisterType<ISetting, SettingViewModel>();

                Container.RegisterType<IPatientRepository, PatientRepository>();
                Container.RegisterType<IClinicRepository, ClinicRepository>();
                Container.RegisterType<IDiagnosisRepository, DiagnosisRepository>();
                Container.RegisterType<IDoctorRepository, DoctorRepository>();
                Container.RegisterType<IMedicalPointRepository, MedicalPointRepository>();
                Container.RegisterType<IPatientVisitRepository, PatientVisitRepository>();
                Container.RegisterType<IPersonRepository, PersonRepository>();
                Container.RegisterType<IVisitConsultationRepository, VisitConsultationRepository>();
                Container.RegisterType<ICultureRepository, CultureRepository>();

                Container.RegisterInstance<ILocalizationManager>(new DataType.Base.Localization.LocalizationManager());

                DataType.Base.ViewModelBase.CurrentCulture = Container.Resolve<CultureRepository>().GetCurrentCulture().Result;
                Container.Resolve<ILocalizationManager>().ConfigureLocalization(DataType.Base.ViewModelBase.CurrentCulture, "ClinicSystem.WPF.Properties.Resources", "ClinicSystem.WPF.exe");

                DataType.Base.ViewModelBase.Container = Container;

                shell = new Shell() { DataContext = Container.Resolve<ShellViewModel>() };
                return shell;
            }

            Shell shell;
        }
    }
}
