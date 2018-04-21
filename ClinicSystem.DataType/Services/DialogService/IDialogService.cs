using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ClinicSystem.DataType.Services.DialogService
{
    public interface IDialogService
    {
        void ShowInformationMessage(string message, Action Ok = null);
        void ShowConfirmationMessage(string message, Action Yes, Action No = null);
        void ShowEditorDialog(UserControl control, Func<Task<bool>> Save, Action Cancel = null);
    }
}
