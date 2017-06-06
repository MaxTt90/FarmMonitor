using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace FarmMonitor.Infrastructure.Converters
{
    /// <summary>
    /// Performs value conversion between bool and Visibility types.  Unlike the BooleanToVisibilityConverter in
    /// System.Windows.Controls, this converter allows the user to decide whether to set the visibility to collapsed or hidden
    /// for false values.
    /// </summary>
    [ValueConversion(typeof(bool), typeof(Visibility))]
    public class BooleanToVisibilityConverter : EqualsParameterConverter
    {
        #region Fields

        private Visibility _visibilityWhenTrue;
        private Visibility _visibilityWhenFalse;
        
        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BooleanToVisibilityConverter"/> class.  The visibility when true will
        /// be visible; the visibility when false will be collapsed.
        /// </summary>
        public BooleanToVisibilityConverter()
            : this(Visibility.Visible, Visibility.Collapsed)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BooleanToVisibilityConverter"/> class.
        /// </summary>
        /// <param name="visibilityWhenTrue">The visibility when true.</param>
        /// <param name="visibilityWhenFalse">The visibility when false.</param>
        public BooleanToVisibilityConverter(Visibility visibilityWhenTrue, Visibility visibilityWhenFalse)
            : base()
        {
            _visibilityWhenTrue = visibilityWhenTrue;
            _visibilityWhenFalse = visibilityWhenFalse;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the visibility when true.
        /// </summary>
        public Visibility VisibilityWhenTrue
        {
            get { return _visibilityWhenTrue; }
            set { _visibilityWhenTrue = value; }
        }

        /// <summary>
        /// Gets or sets the visibility when false.
        /// </summary>
        public Visibility VisibilityWhenFalse
        {
            get { return _visibilityWhenFalse; }
            set { _visibilityWhenFalse = value; }
        }

        #endregion

        #region EqualsParameterConverter Overrides

        /// <summary>
        /// Converts a value.
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>
        /// A converted value. If the method returns null, the valid null value is used.
        /// </returns>
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool boolValue;
            if (value == null)
            {
                boolValue = false;
            }
            else
            {
                boolValue = (bool)value;
            }
            if (InvertResult)
            {
                boolValue = !boolValue;
            }
            return boolValue ? _visibilityWhenTrue : _visibilityWhenFalse;
        }

        #endregion
    }
}
