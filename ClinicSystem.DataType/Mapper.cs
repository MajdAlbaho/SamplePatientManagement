using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ClinicSystem.DataType
{
    public static class Mapper
    {
        static Mapper()
        {
            MappingDictionary = new Dictionary<Type, PropertyInfo[]>();
        }

        public static Dictionary<Type, PropertyInfo[]> MappingDictionary { set; get; }

        public static TTarget Map<TTarget>(object source, TTarget target)
        {
            RegisterForDictionary(source, target);

            var sourceType = source.GetType();
            var targetType = target.GetType();

            var sourceProperties = MappingDictionary.FirstOrDefault(e => e.Key == sourceType).Value;
            var targetPropertis = MappingDictionary.FirstOrDefault(e => e.Key == targetType).Value;

            foreach (var sourceProperty in sourceProperties)
            {
                var targetProperty = targetPropertis.FirstOrDefault(prop =>
                    prop.Name == sourceProperty.Name && prop.SetMethod != null && prop.PropertyType == sourceProperty.PropertyType);
                if (targetProperty != null)
                    targetProperty.SetValue(target, sourceProperty.GetValue(source));
            }
            return target;
        }

        public static TTarget Map<TTarget>(object source) where TTarget : new()
        {
            var target = new TTarget();
            RegisterForDictionary(source, target);

            return Map(source, target);
        }

        public static IEnumerable<TTarget> MapCollection<TTarget>(IEnumerable<object> sources) where TTarget : new()
        {
            foreach (var source in sources)
                yield return Map(source, new TTarget());
        }

        static void RegisterForDictionary(object source)
        {
            if (!MappingDictionary.ContainsKey(source.GetType()))
                MappingDictionary.Add(source.GetType(), source.GetType().GetProperties());
        }
        static void RegisterForDictionary(object source, object target)
        {
            RegisterForDictionary(source);
            RegisterForDictionary(target);
        }
    }
}
