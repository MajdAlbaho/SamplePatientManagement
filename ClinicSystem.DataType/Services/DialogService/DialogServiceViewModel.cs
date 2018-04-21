using ClinicSystem.DataType.Base;
using ClinicSystem.DataType.Base.Localization;
using ClinicSystem.DataType.Services.DialogService.ConfirmationDialog;
using ClinicSystem.DataType.Services.DialogService.EditorDialog;
using ClinicSystem.DataType.Services.DialogService.InformationDialog;
using System;
using System.Collections.ObjectModel;
using Microsoft.Practices.Unity;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ClinicSystem.DataType.Services.DialogService
{
    public class DialogServiceViewModel : ViewModelBase, IDialogService
    {
        public DialogServiceViewModel()
        {
            Dialogs = new ObservableCollection<UserControl>();
        }

        private ObservableCollection<UserControl> _dialogs;
        public ObservableCollection<UserControl> Dialogs
        {
            get { return _dialogs; }
            set { _dialogs = value; OnPropertyChanged(); }
        }

        public void ShowInformationMessage(string message, Action Ok = null)
        {
            var dContext = Container.Resolve<InformationDialogViewModel>();
            dContext.Message = message;
            dContext.OnOK += () =>
            {
                Ok?.Invoke();
                Dialogs.Remove(Dialogs.Last());
            };
            var dialogHost = Container.Resolve<DialogHostViewModel>();
            var informationDialogVM = Container.Resolve<InformationDialogView>(); ;
            informationDialogVM.DataContext = dContext;
            dialogHost.Content = informationDialogVM;

            ViewDialog(dialogHost);
        }

        public void ShowConfirmationMessage(string message, Action Yes, Action No = null)
        {
            var dContext = Container.Resolve<ConfirmationDialogViewModel>();
            dContext.Message = message;
            dContext.OnNo += () =>
            {
                No?.Invoke();
                Dialogs.Remove(Dialogs.Last());
            };
            dContext.OnYes += () =>
            {
                Yes?.Invoke();
                Dialogs.Remove(Dialogs.Last());
            };
            var dialogHost = Container.Resolve<DialogHostViewModel>();
            var confirmationDialogView = Container.Resolve<ConfirmationDialogView>();
            confirmationDialogView.DataContext = dContext;
            dialogHost.Content = confirmationDialogView;

            ViewDialog(dialogHost);
        }



        public void ShowEditorDialog(UserControl control, Func<Task<bool>> Save, Action Cancel = null)
        {
            var dContext = Container.Resolve<EditorDialogViewModel>();
            dContext.ContentControl = control;
            dContext.OnCancel += () =>
            {
                ShowConfirmationMessage(Container.Resolve<ILocalizationManager>().GetValue("SureCancel"), () =>
                {
                    Cancel?.Invoke();
                    Dialogs.Remove(Dialogs.Last());
                });
            };
            dContext.OnSave += async () =>
            {
                if (await Save.Invoke())
                    Dialogs.Remove(Dialogs.Last());
            };

            var dialogHost = Container.Resolve<DialogHostViewModel>();
            var editorDialog = Container.Resolve<EditorDialogView>();
            editorDialog.DataContext = dContext;
            dialogHost.Content = editorDialog;

            ViewDialog(dialogHost);
        }

        public void ViewDialog(DialogHostViewModel dialogHostViewModel)
        {
            Dialogs.Add(new DialogHostView() { DataContext = dialogHostViewModel });
        }
    }
    public enum MessageType
    {
        Information,
        Confirmation
    }
}
