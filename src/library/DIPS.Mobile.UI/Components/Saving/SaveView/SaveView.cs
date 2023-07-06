using DIPS.Mobile.UI.Components.CheckBoxes;
using Colors = Microsoft.Maui.Graphics.Colors;
using Label = DIPS.Mobile.UI.Components.Labels.Label;
using VerticalStackLayout = DIPS.Mobile.UI.Components.Lists.VerticalStackLayout;

namespace DIPS.Mobile.UI.Components.Saving.SaveView;

public partial class SaveView : ContentView
{
    private readonly Label m_stateLabel;

    public SaveView()
    {
        var filledCheckBox = new FilledCheckBox
        {
            HeightRequest = 120,
            WidthRequest = 120,
            CornerRadius = 60,
            VerticalOptions = LayoutOptions.Center,
        };
        
        filledCheckBox.SetBinding(FilledCheckBox.IsCheckedProperty, new Binding(nameof(IsSavingCompleted), source: this));
        filledCheckBox.SetBinding(FilledCheckBox.IsProgressingProperty, new Binding(nameof(IsSaving), source: this));
        filledCheckBox.SetBinding(FilledCheckBox.CompletedCommandProperty, new Binding(nameof(SavingCompletedCommand), source: this));

        m_stateLabel = new Label
        {
            FontSize = Sizes.GetSize(SizeName.size_5),
            VerticalOptions = LayoutOptions.Start,
            HorizontalTextAlignment = TextAlignment.Center,
            FontAttributes = FontAttributes.Bold
        };

        var content = new VerticalStackLayout
        {
            BackgroundColor = Colors.Transparent,
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center,
            Spacing = Sizes.GetSize(SizeName.size_12),
            Children = { filledCheckBox, m_stateLabel }
        };

        Content = content;
    }

    protected override void OnHandlerChanged()
    {
        base.OnHandlerChanged();

        m_stateLabel.Text = SavingText;
    }

    private void SetSavingCompletedText()
    {
        m_stateLabel.Text = SavingCompletedText;
    }

    private static void OnIsSavingCompletedChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if(bindable is not SaveView saveView)
            return;

        if (newValue is true)
        {
            saveView.SetSavingCompletedText();
        }
    }

    private static void OnIsSavingChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if(bindable is not SaveView saveView)
            return;
        
        if(newValue is true)
            saveView.OnSavingStarted?.Invoke();
    }
}