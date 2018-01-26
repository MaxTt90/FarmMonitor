using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace PresentationModule.Converters
{
    /// <summary>
    /// The base class for converters that only convert one way.
    /// </summary>
    public abstract class OneWayConverter : IValueConverter
    {
        /// <summary>
        /// Converts a value from the binding source to a value for the binding target.
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>
        /// A converted value. If the method returns null, the valid null value is used.
        /// </returns>
        public abstract object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture);

        /// <summary>
        /// Converts a value from the binding target to a value for the binding source.  Always throws a NotImplementedException.
        /// </summary>
        /// <param name="value">The value that is produced by the binding target.</param>
        /// <param name="targetType">The type to convert to.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>
        /// A converted value. If the method returns null, the valid null value is used.
        /// </returns>
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
