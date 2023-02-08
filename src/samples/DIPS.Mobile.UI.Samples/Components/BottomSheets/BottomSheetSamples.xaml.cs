using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DIPS.Mobile.UI.Components.BottomSheets;
using DIPS.Mobile.UI.Samples.Components.BottomSheets.Sheets;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DIPS.Mobile.UI.Samples.Components.BottomSheets
{
    [ComponentSample("BottomSheets")]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BottomSheetSamples
    {
        public BottomSheetSamples()
        {
            InitializeComponent();
        }

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            if (propertyName == "Padding")
            {
                
            }
        }
    }
}