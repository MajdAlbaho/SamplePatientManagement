using ClinicSystem.DataType.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ClinicSystem.DataType.Services.DialogService
{
    public class DialogHostViewModel : ViewModelBase
    {
        private UIElement _content;
        public UIElement Content
        {
            get { return _content; }
            set { _content = value; OnPropertyChanged(); }
        }
    }
}
