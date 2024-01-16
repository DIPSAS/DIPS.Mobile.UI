using DIPS.Mobile.UI.Components.Alerting.SystemMessage;

namespace Playground.EirikSamples;

public partial class EirikPage
{
    public EirikPage()
    {
        InitializeComponent();
    }

    private void ContextMenu_OnBindingContextChanged(object sender, EventArgs e)
    {
        SystemMessageService.Display(config: configurator => configurator.Text = "CONTEXT MENU binding context changed!");
    }

    private void ListItemOnBindingContextChanged(object sender, EventArgs e)
    {
        SystemMessageService.Display(config: configurator => configurator.Text = "LIST ITEM binding context changed!");
    }
}