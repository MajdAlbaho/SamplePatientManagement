using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace ClinicSystem.DataType.Base.Localization
{
    public class Localize : UpdatableMarkupExtension
    {
        public static LocalizationManager Localization => LocalizationManager.DefaultLocalizationManager;

        public Localize(object key)
        {
            Key = key;
            Localization.CultureChanged += (sender, args) =>
            {
                UpdateValue(Localization.GetValue((string)Key));
            };
        }

        [ConstructorArgument("key")]
        public object Key { get; set; }

        public override object ProvideValue()
        {
            return Key != null ? Localization.GetValue((string)Key) : null;
        }
    }
}
