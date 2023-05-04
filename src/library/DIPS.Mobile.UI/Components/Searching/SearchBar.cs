using DIPS.Mobile.UI.Extensions;
using DIPS.Mobile.UI.Resources.Colors;
using DIPS.Mobile.UI.Resources.LocalizedStrings.LocalizedStrings;
using Colors = Microsoft.Maui.Graphics.Colors;
using ContentView = Microsoft.Maui.Controls.ContentView;
#if __ANDROID__
using IndeterminateProgressBar = DIPS.Mobile.UI.Components.Progress.Android.IndeterminateProgressBar;
#endif
namespace DIPS.Mobile.UI.Components.Searching
{
    public partial class SearchBar : ContentView
    {
        private readonly InternalSearchBar m_internalSearchBar;
        private readonly Buttons.Button? m_cancelButton;

        public SearchBar()
        {
            var grid = new Grid()
            {
                ColumnDefinitions = new ColumnDefinitionCollection() {new() {Width = GridLength.Star}},
                RowDefinitions = new RowDefinitionCollection() {new() {Height = GridLength.Star}},
                RowSpacing = 0,
                ColumnSpacing = 0
            };

            m_internalSearchBar = new InternalSearchBar();

            m_internalSearchBar.SetBinding(InternalSearchBar.BarColorProperty, new Binding(nameof(BarColor), source: this));
           // m_internalSearchBar.SetBinding(InternalSearchBar.BackgroundProperty, new Binding(nameof(BarColor), source: this));
            m_internalSearchBar.SetBinding(Microsoft.Maui.Controls.SearchBar.TextColorProperty,
                new Binding(nameof(TextColor), source: this));
            m_internalSearchBar.SetBinding(InternalSearchBar.TextFieldColorProperty,
                new Binding(nameof(TextFieldColor), source: this));
            m_internalSearchBar.SetBinding(InternalSearchBar.IsBusyProperty, new Binding(nameof(IsBusy), source: this));
            m_internalSearchBar.SetBinding(PlaceholderColorProperty,
                new Binding(nameof(PlaceholderColor), source: this));
            m_internalSearchBar.SetBinding(Microsoft.Maui.Controls.SearchBar.CancelButtonColorProperty,
                new Binding(nameof(CancelButtonColor), source: this));
            m_internalSearchBar.SetBinding(InternalSearchBar.IconsColorProperty,
                new Binding(nameof(IconsColor), source: this));
            m_internalSearchBar.SetBinding(Microsoft.Maui.Controls.SearchBar.PlaceholderProperty,
                new Binding(nameof(Placeholder), source: this));
            m_internalSearchBar.SetBinding(Microsoft.Maui.Controls.SearchBar.TextProperty,
                new Binding(nameof(Text), source: this));
            m_internalSearchBar.SetBinding(InternalSearchBar.CornerRadiusProperty,
                new Binding(nameof(CornerRadius), source: this));
            m_internalSearchBar.SetBinding(Microsoft.Maui.Controls.SearchBar.SearchCommandProperty,
                new Binding(nameof(SearchCommand), source: this));
            m_internalSearchBar.SetBinding(InternalSearchBar.CancelCommandProperty,
                new Binding(nameof(CancelCommand), source: this));
            m_internalSearchBar.SetBinding(InternalSearchBar.CancelCommandParameterProperty,
                new Binding(nameof(CancelCommandParameterProperty), source: this));
            m_internalSearchBar.SetBinding(InternalSearchBar.ShowsCancelButtonProperty,
                new Binding(nameof(HasCancelButton), source: this));
            m_internalSearchBar.SetBinding(InternalSearchBar.HasBusyIndicationProperty,
                new Binding(nameof(HasBusyIndication), source: this));

            this.SetAppThemeColor(IconsColorProperty, ColorName.color_neutral_60);
            this.SetAppThemeColor(TextColorProperty, ColorName.color_neutral_60);
            this.SetAppThemeColor(CancelButtonColorProperty, ColorName.color_neutral_60);
            this.SetAppThemeColor(BarColorProperty, ColorName.color_neutral_05);
            this.SetAppThemeColor(TextFieldColorProperty, ColorName.color_neutral_05);

            grid.Children.Add(m_internalSearchBar);
            
            //On Android, progressbar and cancel button needs to be added
#if __ANDROID__
            m_internalSearchBar.BackgroundColor = Colors.Red;
                grid.SetBinding(BackgroundColorProperty, new Binding(nameof(BarColor), source:this));
                //Add extra rows and columns
                grid.RowDefinitions.Add(new() {Height = GridLength.Auto});
                grid.ColumnDefinitions.Add(new() {Width = GridLength.Auto});

                //Add progressbar
                var androidProgressBar = new IndeterminateProgressBar();
                grid.RowDefinitions.Add(new() {Height = GridLength.Auto});
                androidProgressBar.SetBinding(IndeterminateProgressBar.IsRunningProperty,
                    new Binding(nameof(IsBusy), source: this));
                androidProgressBar.SetBinding(IsVisibleProperty,
                    new Binding(nameof(HasBusyIndication), source: this));
                //Add progressbar to grid
                Grid.SetColumn(androidProgressBar, 0);
                Grid.SetRow(androidProgressBar, grid.RowDefinitions.Count - 1); //Last row
                Grid.SetColumnSpan(androidProgressBar, 2);
                grid.Children.Add(androidProgressBar);

                //Add cancelbutton
                m_cancelButton = new Buttons.Button { Text = DUILocalizedStrings.Cancel };
                m_cancelButton.BackgroundColor = Colors.Transparent;
                m_cancelButton.SetBinding(Button.TextColorProperty,
                    new Binding(nameof(InternalSearchBar.TextColor), source: m_internalSearchBar));
                m_cancelButton.SetBinding(Button.CommandProperty,
                    new Binding(nameof(InternalSearchBar.CancelCommand), source: m_internalSearchBar));
                m_cancelButton.SetBinding(Button.CommandParameterProperty,
                    new Binding(nameof(CancelCommandParameter), source: m_internalSearchBar));
                m_cancelButton.SetBinding(IsVisibleProperty,
                    new Binding(nameof(InternalSearchBar.ShowsCancelButton), source: m_internalSearchBar));
                
                //Add cancel button to grid
                Grid.SetRow(m_cancelButton, 0);
                Grid.SetColumn(m_cancelButton, grid.ColumnDefinitions.Count - 1); //Last column
                grid.Children.Add(m_cancelButton);
#endif
            
            Content = grid;
        }

        public new void Focus()
        {
            m_internalSearchBar.Focus();
        }
    }
}