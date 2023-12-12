using DIPS.Mobile.UI.API.Vibration;
using DIPS.Mobile.UI.Components.CheckBoxes;
using DIPS.Mobile.UI.Effects.Touch;
using DIPS.Mobile.UI.Resources.Styles;
using DIPS.Mobile.UI.Resources.Styles.Label;
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
            Style = Styles.GetLabelStyle(LabelStyle.Header500),
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

        Content = content;
        
        Touch.SetCommand(m_filledCheckBox, new Command(() =>
        {
            Command?.Execute(CommandParameter);
            DidTapToSave = true;
        }));
        
        // The SaveView should default to not being tappable, only when Command is set should the view be tappable
        Touch.SetIsEnabled(m_filledCheckBox, false);
    }
    
    private bool DidTapToSave { get; set; }

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
            if (saveView.DidTapToSave)
            {
                VibrationService.Success();
                saveView.DidTapToSave = false;
            }
        }
    }

    private void OnCommandChanged()
    {
        Touch.SetIsEnabled(m_filledCheckBox, Command is not null);
    }
}