using ClinicSystem.DataType.Base;
using ClinicSystem.DataType.Base.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ClinicSystem.DataType.Services.DialogService.EditorDialog
{
    public class EditorDialogViewModel : ViewModelBase
    {
        public event Action OnSave;
        public event Action OnCancel;

        private UIElement _contentControl;
        public UIElement ContentControl
        {
            get { return _contentControl; }
            set { _contentControl = value; OnPropertyChanged(); }
        }

        public ICommand SaveCommand => new RelayCommand(Save);
        public ICommand CancelCommand => new RelayCommand(Cancel);

        private void Save()
        {
            OnSave?.Invoke();
        }

        private void Cancel()
        {
            OnCancel?.Invoke();
        }
    }
}
