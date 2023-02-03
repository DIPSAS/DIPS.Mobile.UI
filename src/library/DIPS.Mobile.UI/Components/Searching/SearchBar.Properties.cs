using System.Linq.Expressions;
using Xamarin.Forms;

namespace DIPS.Mobile.UI.Components.Searching
{
    public partial class SearchBar
    {
        public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(
            nameof(CornerRadius),
            typeof(double),
            typeof(SearchBar));

        /// <summary>
        /// Sets the corner radius of the background of the searchbar.
        /// </summary>
        public double CornerRadius
        {
            get => (double)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }
    }
}