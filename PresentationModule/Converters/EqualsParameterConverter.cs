using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PresentationModule.Converters
{
    /// <summary>
    /// Performs value conversions from objects of one type to objects of another type based on the results of an equality 
    /// check against a provided object.
    /// </summary>
    public class EqualsParameterConverter : OneWayConverter
    {
        private bool _invertResult;
        private object _trueResult = true;
        private object _falseResult = false;

        /// <summary>
        /// Gets or sets a value indicating whether to [invert result].
        /// </summary>
        /// <value><c>true</c> if [invert result]; otherwise, <c>false</c>.</value>
        public bool InvertResult
        {
            get { return _invertResult; }
            set { _invertResult = value; }
        }

        /// <summary>
        /// Gets or sets the object to return if the equality check is true.
        /// </summary>
        /// <value>The object to return if the equality check is true.</value>
        public object TrueResult
        {
            get { return _trueResult; }
            set { _trueResult = value; }
        }

        /// <summary>
        /// Gets or sets the object to return if the equality check is false.
        /// </summary>
        /// <value>The object to return if the equality check is false.</value>
        public object FalseResult
        {
            get { return _falseResult; }
            set { _falseResult = value; }
        }

        /// <summary>
        /// Converts a value.
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>A converted value. If the method returns null, the valid null value is used.</returns>
        public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            object trueResult = TrueResult;
            object falseResult = FalseResult;

            if (InvertResult)
            {
                trueResult = FalseResult;
                falseResult = TrueResult;
            }

            if (parameter == null)
            {
                return value == null ? trueResult : falseResult;
            }

            return parameter.Equals(value) ? trueResult : falseResult;
        }
    }
}
