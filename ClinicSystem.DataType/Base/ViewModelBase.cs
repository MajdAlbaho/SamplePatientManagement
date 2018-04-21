using ClinicSystem.DataType.Base.Localization;
using ClinicSystem.DataType.Interfaces;
using Microsoft.AspNet.SignalR.Client;
using Microsoft.Practices.Unity;
using System;
using System.ComponentModel;
using System.Windows;

namespace ClinicSystem.DataType.Base
{
    public class ViewModelBase : BindableBase
    {
        public static IUnityContainer Container;
        protected static IHubProxy _hub;

        public bool SignalRConnected { get; set; }

        protected bool IsDesignMode()
        {
            return DesignerProperties.GetIsInDesignMode(new DependencyObject());
        }

        public static string CurrentCulture { get; set; }

        public void SubscribeToServer()
        {
            try
            {
                string url = @"http://localhost:8080/";
                var connection = new HubConnection(url);
                _hub = connection.CreateHubProxy("GlobalHub");
                connection.Start().Wait();
                SignalRConnected = true;
            }
            catch (Exception)
            {
                // Log exception
                SignalRConnected = false;
            }
        }

        private OperationType _operation;
        public OperationType Operation
        {
            get { return _operation; }
            set { _operation = value; OnPropertyChanged(); }
        }


        public FlowDirection Direction => GetDirection();
        public FlowDirection GetDirection()
        {
            if (LocalizationManager.DefaultCulture == AvailableCulture.English)
                return FlowDirection.LeftToRight;
            else
                return FlowDirection.RightToLeft;
        }
    }

    public class ViewModelBase<TView> : ViewModelBase, IPresentable
                        where TView : new()
    {
        protected TView _view;

        public virtual UIElement GetView()
        {
            if (_view == null)
            {
                _view = new TView();
                var Element = _view as FrameworkElement;
                if (Element != null)
                    Element.DataContext = this;
            }
            return (_view as UIElement);
        }
    }
}
