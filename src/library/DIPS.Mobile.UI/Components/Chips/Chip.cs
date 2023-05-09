namespace DIPS.Mobile.UI.Components.Chips;

public partial class Chip : Frame
{
    public Chip()
    {
        
    }

    protected override void OnHandlerChanged()
    {
        base.OnHandlerChanged();
        
        UpdateCornerRadius();
    }

    partial void UpdateCornerRadius();
}