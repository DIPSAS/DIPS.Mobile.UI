using DIPS.Mobile.UI.API.Vibration;
using DIPS.Mobile.UI.Components.CheckBoxes;
using Colors = Microsoft.Maui.Graphics.Colors;
using Label = DIPS.Mobile.UI.Components.Labels.Label;
using VerticalStackLayout = DIPS.Mobile.UI.Components.Lists.VerticalStackLayout;

namespace DIPS.Mobile.UI.Components.Saving.SaveView;

public partial class SaveView : ContentView
{
    private readonly Label m_stateLabel;
    private readonly FilledCheckBox m_filledCheckBox;

    public SaveView()
    {
        m_filledCheckBox = new FilledCheckBox {VerticalOptions = LayoutOptions.Center,};

        m_filledCheckBox.SetBinding(FilledCheckBox.IsCheckedProperty,
            new Binding(nameof(IsSavingCompleted), source: this));
        m_filledCheckBox.SetBinding(FilledCheckBox.IsProgressingProperty, new Binding(nameof(IsSaving), source: this));
        m_filledCheckBox.SetBinding(FilledCheckBox.CompletedCommandProperty,
            new Binding(nameof(SavingCompletedCommand), source: this));

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
            Children = {m_filledCheckBox, m_stateLabel}
        };
    }

    private void OnAnimationFinished(object? sender, EventArgs e)
    {
        VibrationService.SelectionChanged();
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
            VibrationService.SelectionChanged();
        }
    }

}