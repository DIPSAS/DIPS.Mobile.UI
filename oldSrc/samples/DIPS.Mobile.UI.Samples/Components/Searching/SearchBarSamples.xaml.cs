using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DIPS.Mobile.UI.Samples.Components.Searching
{
    [ComponentSample("SearchBar")]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchBarSamples
    {
        public SearchBarSamples()
        {
            InitializeComponent();
        }

        private void InputView_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (BindingContext is SearchBarSamplesViewModel searchBarSamplesViewModel)
            {
                searchBarSamplesViewModel.FilterItems(e.NewTextValue);
            }
        }
    }
}