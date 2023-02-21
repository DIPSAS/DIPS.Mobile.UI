using System;
using System.ComponentModel;
using CoreAnimation;
using CoreGraphics;
using DIPS.Mobile.UI.iOS.Extensions;
using DIPS.Mobile.UI.Resources.Colors;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using DUISearchBar = DIPS.Mobile.UI.Components.Searching.InternalSearchBar;
using DUISearchBarRenderer = DIPS.Mobile.UI.iOS.Components.Searching.SearchBarRenderer;

[assembly: ExportRenderer(typeof(DUISearchBar), typeof(DUISearchBarRenderer))]

namespace DIPS.Mobile.UI.iOS.Components.Searching
{
    public class SearchBarRenderer : Xamarin.Forms.Platform.iOS.SearchBarRenderer
    {
        private DUISearchBar? m_searchBar;
        private UIActivityIndicatorView? m_activityIndicatorView;
        private UIImageView? m_magnifierIcon;
        private UISearchTextField? m_searchTextField;

        protected override void OnElementChanged(ElementChangedEventArgs<SearchBar> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement != null)
            {
                if (e.NewElement is DUISearchBar searchBar)
                {
                    m_searchBar = searchBar;
                    Control.SearchBarStyle = UISearchBarStyle.Minimal;

                    if (Control.SearchTextField.LeftView is UIImageView uiImageView) //Magnifier icon on the left
                    {
                        m_magnifierIcon = uiImageView;
                    }

                    m_activityIndicatorView = new UIActivityIndicatorView();
                    m_searchTextField = Control.SearchTextField;
                    
                    UpdateBackground();
                    UpdateForeground();
                    UpdateIsBusy();
                    UpdateCancelButton();
                    SubscribeToEvents();
                }
            }
        }

        public override void Draw(CGRect rect)
        {
            if (m_searchBar != null)
            {
                Control.AddCornerRadius(m_searchBar.CornerRadius, m_searchBar.BackgroundColor);    
            } 
            
            base.Draw(rect);
        }

        protected override void Dispose(bool disposing)
        {
            UnSubscribeToEvents();
            base.Dispose(disposing);
        }
        
        private void SubscribeToEvents()
        {
            Control.CancelButtonClicked += OnCancelButtonTouchDown;
        }
        
        private void UnSubscribeToEvents()
        {
            Control.CancelButtonClicked -= OnCancelButtonTouchDown;
        }

        private void OnCancelButtonTouchDown(object sender, EventArgs e)
        {
            if (m_searchBar == null) return;
            m_searchBar.SendCancelTapped();
        }

        private void UpdateForeground()
        {
            if (m_searchBar == null || m_magnifierIcon == null) return;

            m_magnifierIcon.TintColor = m_searchBar.IconsColor.ToUIColor();
        }

        private void UpdateIsBusy()
        {
            if (m_activityIndicatorView == null || m_searchBar == null || m_searchTextField == null || m_magnifierIcon == null) return;

            if (m_searchBar.HasBusyIndication)
            {
                if (m_searchBar.IsBusy)
                {
                    m_activityIndicatorView.Color = m_searchBar.IconsColor.ToUIColor();
                    m_activityIndicatorView.StartAnimating();
                    var leftViewSize = m_magnifierIcon.Frame.Size;
                    m_activityIndicatorView.Center = new CGPoint(x:
                        leftViewSize.Width / 2, y: leftViewSize.Height / 2);
                    m_searchTextField.LeftView = m_activityIndicatorView;
                }
                else
                {
                    m_activityIndicatorView.RemoveFromSuperview();
                    m_searchTextField.LeftView = m_magnifierIcon;
                }    
            }
        }

        private void UpdateBackground()
        {
            if (m_searchBar == null) return;
            
            var textField = Control.FindChildView<UITextField>();
            if (textField != null)
            {
                textField.BackgroundColor = Colors.GetColor(ColorName.color_neutral_20).ToUIColor();

            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            switch (e.PropertyName)
            {
                case nameof(SearchBar.Text):
                    UpdateCancelButton();
                    break;
                case nameof(UI.Components.Searching.SearchBar.HasCancelButton):
                case nameof(UI.Components.Searching.SearchBar.CancelButtonColor):
                    UpdateCancelButton();
                    break;
                case nameof(DUISearchBar.CornerRadius):
                    UpdateBackground();
                    break;
                case nameof(DUISearchBar.IconsColor):
                    UpdateForeground();
                    break;
                case nameof(DUISearchBar.IsBusy):
                    UpdateIsBusy();
                    break;
            }
        }

        public override void UpdateCancelButton()
        {
            if (m_searchBar == null) return;
            
            Control.ShowsCancelButton = m_searchBar.ShowsCancelButton;
            var cancelButton = Control.FindChildView<UIButton>();
            if (cancelButton != null)
            {
                cancelButton.SetTitleColor(Element.CancelButtonColor.ToUIColor(), UIControlState.Normal);
            }
        }
    }
}