using Xamarin.Forms;

namespace DIPS.Mobile.UI.Components.Lists
{
    public partial class ListView
    {
        public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(
            nameof(CornerRadius),
            typeof(double),
            typeof(ListView));
        
        /// <summary>
        /// Sets the corner radius for the background of the listview.
        /// </summary>
        public double CornerRadius
        {
            get => (double)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }
    }
}