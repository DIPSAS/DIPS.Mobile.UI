namespace DIPS.Mobile.UI.Components.Lists
{
    public partial class ListView : Microsoft.Maui.Controls.ListView
    {
        public ListView()
        {
            BackgroundColor = Colors.Transparent;
            //Adds a extra space in the bottom to make sure the last item is not placed at the very bottom of the page, this makes the last item more accessible for people.
            Footer = new BoxView() {HeightRequest = 96}; //TODO: Use DesignSystem size
            SelectionMode = ListViewSelectionMode.None;
        }
    }
}