using DIPS.Mobile.UI.Components.Alerting.SystemMessage;
using DIPS.Mobile.UI.Resources.Icons;
using DIPS.Mobile.UI.Resources.Styles.SystemMessage;

namespace Playground.SystemMessageSamples;

public partial class SystemMessageReadonlySample
{
    private const string ReadonlyMessage = "Databasen er satt i lesemodus. Du kan fortsette å arbeide, men kun med lesetilgang inntil systemet er satt tilbake i normal drift igjen.";
    private const string NormalModeMessage = "Databasen er ikke lenger i lesemodus.Du kan nå arbeide videre som før.";

    public SystemMessageReadonlySample()
    {
        InitializeComponent();
    }

    private void ShowReadonlySystemMessage(object sender, EventArgs e)
    {
        SystemMessageService.Display(config =>
        {
            config.Text = ReadonlyMessage;
            config.Style = SystemMessageStyle.Warning;
            config.Icon = Icons.GetIcon(IconName.edit_off);
        });
    }

    private void ShowNormalModeSystemMessage(object sender, EventArgs e)
    {
        SystemMessageService.Display(config =>
        {
            config.Text = NormalModeMessage;
            config.Style = SystemMessageStyle.Success;
            config.Icon = Icons.GetIcon(IconName.pencil_line);
        });
    }
}