using System.Security.Cryptography;
using Xamarin.Forms;

namespace DIPS.Mobile.UI.Components.Lists
{
    public partial class ListView : Xamarin.Forms.ListView
    {
        public ListView()
        {
            BackgroundColor = Color.Transparent;

            var size_0 = 0;
            var size_1 = 4;
            var size_2 = 8;
            var size_3 = 12;
            var size_4 = 24;
            Margin = new Thickness(size_2,size_2,size_2, size_0); //<---- Margin for List
            Footer = new BoxView() {HeightRequest = 64};
        }
    }
}