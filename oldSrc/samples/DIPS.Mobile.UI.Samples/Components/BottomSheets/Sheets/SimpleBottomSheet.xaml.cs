using System;
using Xamarin.Forms.Xaml;

namespace DIPS.Mobile.UI.Samples.Components.BottomSheets.Sheets
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SimpleBottomSheetView
    {
        public SimpleBottomSheetView()
        {
            InitializeComponent();
        }

        private void FirstBottomSheetView_OnWillClose(object sender, EventArgs e)
        {
            
        }

        private void FirstBottomSheetView_OnDidClose(object sender, EventArgs e)
        {
            
        }
    }
}