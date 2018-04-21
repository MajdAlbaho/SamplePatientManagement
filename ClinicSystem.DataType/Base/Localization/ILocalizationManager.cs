using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;


namespace ClinicSystem.DataType.Base.Localization
{
    public interface ILocalizationManager
    {
        string this[string key] { get; }

        event EventHandler<string> CultureChanged;

        void AddValues(Dictionary<string, Dictionary<string, string>> values);
        string GetValue(string key, string culture = null);
        string BindValue<T>(T item, Expression<Func<T, object>> property, string key, string culture = null);
        void Initialize(IUnityContainer container);
        void SetCulture(string newCulture);
        void ConfigureLocalization(string defaultCluture, string resourceManagerFullPath, string resourceManagerAssembly);
    }
}
