using System;
using System.Windows.Markup;

namespace PresentationModule.Converters
{
    /// <summary>
    /// A markup extension to simplify the use of converters in XAML.
    /// </summary>
    /// <remarks>
    /// Converters inheriting this can be used in XAML without having to be declared as a resource.
    /// </remarks>
    public class ConverterMarkupExtension : MarkupExtension
    {
        /// <summary>
        /// Returns the converter inheriting this class to be provided as the value of the target property for this markup extension.
        /// </summary>
        /// <param name="serviceProvider">A service provider helper that can provide services for the markup extension. (Not used.)</param>
        /// <returns>The converter to set on the property where the extension is applied.</returns>
        public sealed override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
