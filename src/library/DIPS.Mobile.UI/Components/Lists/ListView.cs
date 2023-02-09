using System.Security.Cryptography;
using Xamarin.Forms;

namespace DIPS.Mobile.UI.Components.Lists
{
    public partial class ListView : Xamarin.Forms.ListView
    {
        public ListView()
        {
            BackgroundColor = Color.Transparent;
            Footer = new BoxView() {HeightRequest = 32}; //TODO: Use DesignSystem size
        }
    }
}