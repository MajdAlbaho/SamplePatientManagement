using ClinicSystem.DataType.Base;
using ClinicSystem.DataType.Base.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ClinicSystem.DataType.Services.DialogService.ConfirmationDialog
{
    public class ConfirmationDialogViewModel : ViewModelBase
    {
        private string _message;

        public string Message
        {
            get { return _message; }
            set { _message = value; OnPropertyChanged(); }
        }

        public event Action OnYes;
        public event Action OnNo;

        public ICommand YesCommand => new RelayCommand(Yes);
        public ICommand NoCommand => new RelayCommand(No);

        private void No()
        {
            OnNo?.Invoke();
        }

        private void Yes()
        {
            OnYes?.Invoke();
        }
    }
}
