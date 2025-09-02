using DIPS.Mobile.UI.API.Vibration;
using DIPS.Mobile.UI.Components.CheckBoxes;
using DIPS.Mobile.UI.Resources.Styles;
using DIPS.Mobile.UI.Resources.Styles.Label;
using Colors = Microsoft.Maui.Graphics.Colors;
using Label = DIPS.Mobile.UI.Components.Labels.Label;

namespace DIPS.Mobile.UI.Components.Saving.SaveView;

public partial class SaveView : ContentView
{
    private readonly Label m_stateLabel;
    private readonly FilledCheckBox m_filledCheckBox;

    public SaveView()
    {
        m_filledCheckBox = new FilledCheckBox {VerticalOptions = LayoutOptions.Center};
        
        m_filledCheckBox.SetBinding(FilledCheckBox.IsNotCheckedOpacityProperty, static (SaveView saveView) => saveView.IsNotCheckedOpacity, source: this);
        m_filledCheckBox.SetBinding(FilledCheckBox.IsCheckedProperty, static (SaveView saveView) => saveView.IsSavingCompleted, source: this);
        m_filledCheckBox.SetBinding(FilledCheckBox.IsProgressingProperty, static (SaveView saveView) => saveView.IsSaving, source: this);
        m_filledCheckBox.SetBinding(FilledCheckBox.CompletedCommandProperty, static (SaveView saveView) => saveView.SavingCompletedCommand, source: this);

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
        
        GestureRecognizers.Add(new TapGestureRecognizer
        {
            Command = new Command(() =>
            {
                Command?.Execute(CommandParameter);
                DidTapToSave = true;
            })
        });
    }
    
    private bool DidTapToSave { get; set; }

    protected override void OnHandlerChanging(HandlerChangingEventArgs args)
    {
        base.OnHandlerChanging(args);

        if (args.NewHandler is null)
            return;
        
        SetSavingText();
    }

    private void SetSavingText()
    {
        if (string.IsNullOrEmpty(SavingText))
        {
            m_stateLabel.IsVisible = false;
            return;
        }
        m_stateLabel.IsVisible = true;
        m_stateLabel.Text = SavingText;
    }
    
    

    private void SetSavingCompletedText()
    {
        if (string.IsNullOrEmpty(SavingCompletedText))
        {
            m_stateLabel.IsVisible = false;
            return;
        }

        m_stateLabel.IsVisible = true;
        
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
}