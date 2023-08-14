using DIPS.Mobile.UI.Extensions;
using DIPS.Mobile.UI.Resources.Colors;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Components.Chips;

public partial class Chip : View
{
    public void SendTapped()
    {
        Command?.Execute(CommandParameter);
        Tapped?.Invoke(this, EventArgs.Empty);
    }

    public void SendCloseTapped()
    {
        CloseCommand?.Execute(CloseCommandParameter);
        CloseTapped?.Invoke(this, EventArgs.Empty);
    }
}