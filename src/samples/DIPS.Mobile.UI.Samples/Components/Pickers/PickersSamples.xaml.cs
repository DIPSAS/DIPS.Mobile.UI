using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DIPS.Mobile.UI.Samples.Components.Pickers
{
    [ComponentSample("Pickers")]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PickersSamples
    {
        public PickersSamples()
        {
            InitializeComponent();
        }

        private void OnPersonSelected(object sender, object e)
        {
            
        }

        protected override void OnAppearing()
        {
            if (BindingContext is PickerSamplesViewModel pickerSamplesViewModel)
            {
                pickerSamplesViewModel.Initialize();
            }
            base.OnAppearing();
        }
    }
}