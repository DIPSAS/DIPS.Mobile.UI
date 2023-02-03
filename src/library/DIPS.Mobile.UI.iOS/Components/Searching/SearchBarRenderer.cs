using System;
using System.ComponentModel;
using DIPS.Mobile.UI.Resources.Colors;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using DUISearchBar = DIPS.Mobile.UI.Components.Searching.SearchBar;
using DUISearchBarRenderer = DIPS.Mobile.UI.iOS.Components.Searching.SearchBarRenderer;

[assembly: ExportRenderer(typeof(DUISearchBar), typeof(DUISearchBarRenderer))]

namespace DIPS.Mobile.UI.iOS.Components.Searching
{
    public class SearchBarRenderer : Xamarin.Forms.Platform.iOS.SearchBarRenderer
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
                    Control.SearchBarStyle = UISearchBarStyle.Minimal;
                    UpdateBackground();
                }
            }
        }

        private void UpdateBackground()
        {
            if (m_searchBar == null) return;
            
            Control.Layer.CornerRadius = new nfloat(m_searchBar.CornerRadius);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            switch (e.PropertyName)
            {
                case nameof(SearchBar.Text):
                    Control.ShowsCancelButton = false;
                    break;
                case nameof(DUISearchBar.CornerRadius):
                    UpdateBackground();
                    break;
            }
        }
    }
}