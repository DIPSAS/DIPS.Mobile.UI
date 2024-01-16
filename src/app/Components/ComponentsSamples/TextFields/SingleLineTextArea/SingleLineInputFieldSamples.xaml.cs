
namespace Components.ComponentsSamples.TextFields.SingleLineTextArea;

public partial class SingleLineInputFieldSamples
{
    public SingleLineInputFieldSamples()
    {
        InitializeComponent();
        
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        SingleLineEntry.Focus();
    }

    private void Button_OnClicked(object? sender, EventArgs e)
    {
        TextArea.Unfocus();
    }
}