using ClinicSystem.DataType.Base;
using ClinicSystem.DataType.Base.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ClinicSystem.DataType.Services.DialogService.InformationDialog
{
    class InformationDialogViewModel : ViewModelBase
    {
        private string _message;
        public string Message
        {
            get { return _message; }
            set { _message = value; OnPropertyChanged(); }
        }

        public event Action OnOK;

        public ICommand OkCommand => new RelayCommand(Ok);

        public void Ok()
        {
            OnOK?.Invoke();
        }
    }
}
