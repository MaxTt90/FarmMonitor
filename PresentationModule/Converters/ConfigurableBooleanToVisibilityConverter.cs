// Copyright (c) 2016-2017, Elekta AB

using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Globalization;
using Elekta.Desktop.GuiComponents.Properties;

namespace PresentationModule.Converters
{
    /// <summary>
    /// A configurable converter to convert boolean values to and from Visibility enumeration values.
    /// </summary>
    [ValueConversion(typeof(bool), typeof(Visibility))]
    public class ConfigurableBooleanToVisibilityConverter : ConverterMarkupExtension, IValueConverter
    {
        public ConfigurableBooleanToVisibilityConverter()
        {
            TrueValue = Visibility.Visible;
            FalseValue = Visibility.Collapsed;
        }

        /// <summary>
        /// Gets or sets the Visibility value to use when value to convert is true.
        /// </summary>
        [DefaultValue(Visibility.Visible)]
        public Visibility TrueValue { get; set; }

        /// <summary>
        /// Gets or sets the Visibility value to use when value to convert is false.
        /// </summary>
        [DefaultValue(Visibility.Collapsed)]
        public Visibility FalseValue { get; set; }

        /// <summary>
        /// Converts a boolean value to a Visibility enumeration value.
        /// The value to return can be configured by setting the <see cref="TrueValue"/> and <see cref="FalseValue"/> respectively.
        /// </summary>
        /// <param name="value">The boolean value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property. (Not used.)</param>
        /// <param name="parameter">The converter parameter. (Not used.)</param>
        /// <param name="culture">The culture to use in the converter. (Not used.)</param>
        /// <returns>If the value to convert is true <see cref="TrueValue"/> is returned, otherwise <see cref="FalseValue"/> is returned.</returns>
        /// <exception cref="ArgumentException">The value is not of type bool.</exception>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is bool))
            {
                throw new ArgumentException("Value is not type of bool.");
            }

            return (bool)value ? TrueValue : FalseValue;
        }

        /// <summary>
        /// Converts a Visibility enum value to a boolean.
        /// The value to return depends on the current value of the <see cref="TrueValue"/> and <see cref="FalseValue"/> respectively.
        /// </summary>
        /// <param name="value">The Visibility value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property. (Not used.)</param>
        /// <param name="parameter">The converter parameter. (Not used.)</param>
        /// <param name="culture">The culture to use in the converter. (Not used.)</param>
        /// <returns>
        /// True if the value to convert is equal to <see cref="TrueValue"/>,
        /// false if the value to convert is equal to <see cref="FalseValue"/>,
        /// or null if the value to convert doesn't match either.</returns>
        /// <exception cref="ArgumentException">The value is not of type Visibility.</exception>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is Visibility))
            {
                throw new ArgumentException("Value is not type of Visibility.");
            }

            if (Equals(value, TrueValue))
            {
                return true;
            }

            if (Equals(value, FalseValue))
            {
                return false;
            }

            return null;
        }
    }
}
