using System.ComponentModel;
using Android.Graphics.Drawables;
using Android.Views;
using Android.Widget;
using DIPS.Mobile.UI.Droid.Extensions;
using Org.Apache.Http.Protocol;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Color = Android.Graphics.Color;
using DUISearchBar = DIPS.Mobile.UI.Components.Searching.SearchBar;
using DUISearchBarRenderer = DIPS.Mobile.UI.Droid.Components.Searching.SearchBarRenderer;
using SearchView = AndroidX.AppCompat.Widget.SearchView;
using View = Android.Views.View;

[assembly: ExportRenderer(typeof(DUISearchBar), typeof(DUISearchBarRenderer))]

namespace DIPS.Mobile.UI.Droid.Components.Searching
{
    public class SearchBarRenderer : Xamarin.Forms.Platform.Android.SearchBarRenderer
    {
        private DUISearchBar? m_searchBar;

        protected override void OnElementChanged(ElementChangedEventArgs<SearchBar> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement != null)
            {
                if (e.NewElement is DUISearchBar searchBar)
                {
                    m_searchBar = searchBar;
                    UpdateBackground();
                    RemoveHorizontalLineUnderSearchEditTextView();
                }
            }
        }

        private void RemoveHorizontalLineUnderSearchEditTextView()
        {
            var editText = Control.FindChildView<EditText>();
            if (editText is AutoCompleteTextView autoCompleteTextView)
            {
                if (editText.Parent is View parentView)
                {
                    parentView.SetBackgroundColor(Color.Transparent);
                }
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            switch (e.PropertyName)
            {
                case nameof(DUISearchBar.CornerRadius):
                case nameof(VisualElement.BackgroundColorProperty):
                    UpdateBackground();
                    break;
            }
        }

        private void UpdateBackground()
        {
            if (m_searchBar == null) return;
            Control.SetRoundedRectangularBackground(m_searchBar.CornerRadius, m_searchBar.BackgroundColor);
        }
    }
}