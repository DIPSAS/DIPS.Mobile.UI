using System.ComponentModel;
using Android.Graphics;
using Android.Widget;
using DIPS.Mobile.UI.Droid.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Color = Android.Graphics.Color;
using DUISearchBar = DIPS.Mobile.UI.Components.Searching.SearchBar;
using DUISearchBarRenderer = DIPS.Mobile.UI.Droid.Components.Searching.SearchBarRenderer;
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
                    UpdateForegroundColors();
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
                case nameof(DUISearchBar.TextColor):
                case nameof(DUISearchBar.IconsColor):
                    UpdateForegroundColors();
                    break;
            }
        }

        private void RemoveHorizontalLineUnderSearchEditTextView()
        {
            var editText = Control.FindChildView<EditText>();
            if (editText is AutoCompleteTextView)
            {
                if (editText.Parent is View parentView)
                {
                    parentView.SetBackgroundColor(Color.Transparent);
                }
            }
        }

        private new void UpdateBackground()
        {
            if (m_searchBar == null) return;

            Control.SetRoundedRectangularBackground(m_searchBar.CornerRadius, m_searchBar.BackgroundColor);
        }

        private void UpdateForegroundColors()
        {
            if (m_searchBar == null) return;

            //Change color of cursor on the search text edit to the same color as the text color
            var editText = Control.FindChildView<EditText>();
            if (editText is AutoCompleteTextView autoCompleteTextView)
            {
#pragma warning disable CS0618
                autoCompleteTextView.TextCursorDrawable?.SetColorFilter(m_searchBar.TextColor.ToAndroid(),
                    PorterDuff.Mode.SrcIn); //Bindable property!
#pragma warning restore CS0618
            }

            //Set color of icons in the search bar
            foreach (var view in Control.GetFlatViewHierarchyCollection())
            {
                if (view is ImageView imageView)
                {
                    imageView.SetColorFilter(m_searchBar.IconsColor.ToAndroid());
                }
            }
        }
    }
}