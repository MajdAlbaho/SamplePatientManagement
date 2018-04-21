using ClinicSystem.DataType.Base;
using ClinicSystem.DataType.Interfaces;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicSystem.DataType.Services.DialogService
{
    public class DialogServiceModule : IModule
    {
        IUnityContainer _container;
        private IRegionManager _regionManager;

        public DialogServiceModule(IUnityContainer container, IRegionManager regionManager)
        {
            _container = container;
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            _container.RegisterType<IDialogService, DialogServiceViewModel>(new ContainerControlledLifetimeManager());
            _regionManager.RegisterViewWithRegion(Regions.DialogRegion, typeof(DialogServiceView));
        }
    }
}
