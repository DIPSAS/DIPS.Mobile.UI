using DIPS.Mobile.UI.Components.Progress;
using DIPS.Mobile.UI.Extensions;
using DIPS.Mobile.UI.Resources.Colors;
using DIPS.Mobile.UI.Resources.LocalizedStrings.LocalizedStrings;
using ProgressBar = DIPS.Mobile.UI.Components.Progress.ProgressBar;
using Xamarin.Forms;

namespace DIPS.Mobile.UI.Components.Searching
{
    public partial class SearchBar : ContentView
    {
        private readonly InternalSearchBar m_searchBar;
        private readonly Buttons.Button m_cancelButton;

        public SearchBar()
        {
            var grid = new Grid()
            {
                ColumnDefinitions = new ColumnDefinitionCollection() {new() {Width = GridLength.Star}},
                RowDefinitions = new RowDefinitionCollection() {new() {Height = GridLength.Star}},
                RowSpacing = 0,
                ColumnSpacing = 0
            };

            m_searchBar = new InternalSearchBar();

            m_searchBar.SetBinding(BackgroundColorProperty, new Binding {Path = nameof(BarColor), Source = this});
            m_searchBar.SetBinding(Xamarin.Forms.SearchBar.TextColorProperty,
                new Binding {Path = nameof(TextColor), Source = this});
            m_searchBar.SetBinding(Xamarin.Forms.SearchBar.CancelButtonColorProperty,
                new Binding {Path = nameof(CancelButtonColor), Source = this});
            m_searchBar.SetBinding(InternalSearchBar.IconsColorProperty,
                new Binding {Path = nameof(IconsColor), Source = this});
            m_searchBar.SetBinding(Xamarin.Forms.SearchBar.PlaceholderProperty,
                new Binding {Path = nameof(Placeholder), Source = this});
            m_searchBar.SetBinding(Xamarin.Forms.SearchBar.TextProperty,
                new Binding {Path = nameof(Text), Source = this});
            m_searchBar.SetBinding(InternalSearchBar.CornerRadiusProperty,
                new Binding {Path = nameof(CornerRadius), Source = this});
            m_searchBar.SetBinding(Xamarin.Forms.SearchBar.SearchCommandProperty,
                new Binding {Path = nameof(SearchCommand), Source = this});
            m_searchBar.SetBinding(InternalSearchBar.CancelCommandProperty,
                new Binding {Path = nameof(CancelCommand), Source = this});
            m_searchBar.SetBinding(InternalSearchBar.CancelCommandParameterProperty,
                new Binding {Path = nameof(CancelCommandParameterProperty), Source = this});
            m_searchBar.SetBinding(InternalSearchBar.ShowsCancelButtonProperty,
                new Binding {Path = nameof(ShowsCancelButton), Source = this});

            this.SetAppThemeColor(IconsColorProperty, ColorName.color_neutral_60);
            this.SetAppThemeColor(TextColorProperty, ColorName.color_neutral_60);
            this.SetAppThemeColor(CancelButtonColorProperty, ColorName.color_neutral_60);
            this.SetAppThemeColor(BarColorProperty, ColorName.color_neutral_05);
            CornerRadius = 8; //TODO: Use DesignSystem

            grid.Children.Add(m_searchBar, 0, 0);
            if (Device.RuntimePlatform == Device.Android) //On Android, progressbar and cancel button needs to be added
            {
                //Add extra rows and columns
                grid.RowDefinitions.Add(new() {Height = GridLength.Auto});
                grid.ColumnDefinitions.Add(new(){Width = GridLength.Auto});
                
                //Add progressbar
                var progressBar = new ProgressBar();
                progressBar.Mode = ProgressBarMode.Indeterminate;
                grid.RowDefinitions.Add(new() {Height = GridLength.Auto});
                //Add progressbar to grid
                Grid.SetColumn(progressBar, 0);
                Grid.SetRow(progressBar, grid.RowDefinitions.Count-1); //Last row
                Grid.SetColumnSpan(progressBar, 2);
                grid.Children.Add(progressBar);

                //Deal with corner radius
                this.CornerRadius = 8;
                // APPEN KRASJER PÅ ANDROID ETTER VI LA TIL CORNER RADIUS PÅ SEARCH BAR OG CANCEL BUTTON
                
                
                //Add cancelbutton
                m_cancelButton = new Buttons.Button {Text = DUILocalizedStrings.Cancel};
                m_cancelButton.SetBinding(BackgroundColorProperty,
                    new Binding(nameof(BackgroundColor), source: m_searchBar));
                m_cancelButton.SetBinding(Button.TextColorProperty,
                    new Binding(nameof(InternalSearchBar.TextColor), source: m_searchBar));
                m_cancelButton.SetBinding(Button.CommandProperty,
                    new Binding(nameof(InternalSearchBar.CancelCommand), source: m_searchBar));
                m_cancelButton.SetBinding(Button.CommandParameterProperty,
                    new Binding(nameof(CancelCommandParameterProperty), source: this));
                m_cancelButton.SetBinding(Button.IsVisibleProperty,
                    new Binding(nameof(ShowsCancelButton), source: m_searchBar));
                //Add cancel button to grid
                Grid.SetRow(m_cancelButton, 0);
                Grid.SetColumn(m_cancelButton, grid.ColumnDefinitions.Count -1); //Last column
                grid.Children.Add(m_cancelButton);
            }

            Content = grid;
        }

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            if (propertyName.Equals(nameof(ShowsCancelButton)))
            {
                var previousCornerRadius = CornerRadius;
                m_cancelButton.CornerRadius = new CornerRadius(0, previousCornerRadius.TopRight, 0, previousCornerRadius.BottomRight);
                m_searchBar.CornerRadius = new CornerRadius(previousCornerRadius.TopLeft, 0, previousCornerRadius.BottomLeft, 0);
            }
        }
    }
}