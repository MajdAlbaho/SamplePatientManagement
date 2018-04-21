using Microsoft.Practices.Unity;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace ClinicSystem.DataType.Base.Localization
{
    class BoundLocalizationObject
    {
        public object Item { get; set; }
        public PropertyInfo PropertyInfo { get; set; }
        public Dictionary<string, string> Values { get; set; }

        public void SetValue(object value)
        {
            PropertyInfo.SetValue(Item, value);
        }
    }


    public class LocalizationManager : ILocalizationManager
    {
        private static LocalizationManager _defaultLocalizationManager;

        public void ConfigureLocalization(string defaultCluture, string resourceManagerFullPath, string resourceManagerAssembly)
        {
            _defaultLocalizationManager = this;
            SetCulture(defaultCluture);
            DefaultCulture = defaultCluture;
            var values = new Dictionary<string, Dictionary<string, string>>();
            var res = new ResourceManager(resourceManagerFullPath,Assembly.LoadFrom(Path.Combine(AppDomain.CurrentDomain.BaseDirectory,resourceManagerAssembly)));
            var supportedCultures = new[] { AvailableCulture.Arabic, AvailableCulture.English };
            foreach (var culture in supportedCultures)
            {
                foreach (DictionaryEntry val in res.GetResourceSet(new CultureInfo(culture), true, true))
                {
                    if (!values.ContainsKey(val.Key.ToString()))
                        values[val.Key.ToString()] = new Dictionary<string, string>();
                    values[val.Key.ToString()][culture] = val.Value.ToString();
                }
            }
            AddValues(values);
        }


        public static LocalizationManager DefaultLocalizationManager => _defaultLocalizationManager ?? (_defaultLocalizationManager = new LocalizationManager());

        public event EventHandler<string> CultureChanged;
        public static string DefaultCulture { get; set; }
        private readonly Dictionary<string, Dictionary<string, string>> _localziationValues;
        private readonly ConcurrentDictionary<string, List<BoundLocalizationObject>> _boundObjects;
        private readonly ConcurrentBag<BoundLocalizationObject> _customBoundObjects;


        public static void SetDefaultLocalizationManager(LocalizationManager manager)
        {
            _defaultLocalizationManager = manager;
        }

        public LocalizationManager()
        {
            _localziationValues = new Dictionary<string, Dictionary<string, string>>();
            _boundObjects = new ConcurrentDictionary<string, List<BoundLocalizationObject>>();
            _customBoundObjects = new ConcurrentBag<BoundLocalizationObject>();
        }



        public string this[string key]
        {
            get { return GetValue(key); }
        }
        public void SetCulture(string newCulture)
        {
            DefaultCulture = newCulture;
            UpdateBoundObjects();
            CultureChanged?.Invoke(this, newCulture);
        }

        private void UpdateBoundObjects()
        {
            foreach (var boundObject in _boundObjects)
            {
                var newValue = GetValue(boundObject.Key);
                foreach (var boundLocalizationObject in boundObject.Value)
                {
                    boundLocalizationObject.SetValue(newValue);
                }
            }
            foreach (var boundObject in _customBoundObjects)
            {
                if (boundObject.Values != null)
                {
                    boundObject.SetValue(GetLocalValue(boundObject, DefaultCulture));
                }
            }
        }

        private string GetLocalValue(BoundLocalizationObject boundObject, string culture)
        {
            if (boundObject.Values != null)
            {
                if (boundObject.Values.ContainsKey(culture))
                    return boundObject.Values[culture];
                if (boundObject.Values.Values != null && boundObject.Values.Values.Any())
                    return boundObject.Values.Values.FirstOrDefault();
            }
            return null;
        }

        public string BindValue<T>(T item, Expression<Func<T, object>> property, string key, string culture = null)
        {
            var currentValue = GetValue(key, culture);
            var propertyName = ((MemberExpression)property.Body).Member.Name;
            var boundValue = new BoundLocalizationObject
            {
                Item = item,
                PropertyInfo = item.GetType().GetTypeInfo().GetProperty(propertyName)
            };

            if (!_boundObjects.ContainsKey(key))
                _boundObjects.TryAdd(key, new List<BoundLocalizationObject>());
            List<BoundLocalizationObject> objects;
            if (_boundObjects.TryGetValue(key, out objects))
                objects.Add(boundValue);
            boundValue.SetValue(currentValue);
            return currentValue;
        }

        public string BindCustomValue<T>(T item,
                                         string property,
                                         Dictionary<string, string> values,
                                         string culture = null)
        {
            culture = culture ?? DefaultCulture;
            var boundValue = new BoundLocalizationObject
            {
                Item = item,
                PropertyInfo = item.GetType().GetTypeInfo().GetProperty(property),
                Values = values
            };
            var currentValue = GetLocalValue(boundValue, culture);
            _customBoundObjects.Add(boundValue);
            boundValue.SetValue(currentValue);
            return currentValue;
        }

        public void Initialize(IUnityContainer container)
        {
        }

        public void AddValues(Dictionary<string, Dictionary<string, string>> values)
        {
            foreach (var value in values)
            {
                if (!_localziationValues.ContainsKey(value.Key))
                {
                    _localziationValues[value.Key] = new Dictionary<string, string>();
                }
                var existedDictionary = _localziationValues[value.Key];
                foreach (var localizationPair in value.Value)
                {
                    existedDictionary[localizationPair.Key] = localizationPair.Value;
                }
            }
        }

        public string GetValue(string key, string culture = null)
        {
            if (string.IsNullOrEmpty(culture))
                culture = DefaultCulture;

            if (!_localziationValues.ContainsKey(key) || !_localziationValues[key].ContainsKey(culture))
            {
                return key;
            }

            return _localziationValues[key][culture];
        }
    }

    public class AvailableCulture
    {
        public const string Arabic = "ar-SY";
        public const string English = "en-US";
    }
}
