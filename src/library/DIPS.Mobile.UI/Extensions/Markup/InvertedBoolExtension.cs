using DIPS.Mobile.UI.Converters.ValueConverters;

namespace DIPS.Mobile.UI.Extensions.Markup
{
    /// <summary>
    /// This markup extension lets you use <see cref="InvertedBoolConverter"/> when you are referring a static property from some class.
    /// </summary>
    [ContentProperty(nameof(Input))]
    public class InvertedBoolExtension : IMarkupExtension
    {
        /// <summary>
        /// The boolean input to invert
        /// </summary>
        public bool Input { get; set; }

        /// <inheritdoc cref="IMarkupExtension"/>
        public object ProvideValue(IServiceProvider serviceProvider)
        {
            return new InvertedBoolConverter().Convert(Input, null, null, null);
        }
    }
}