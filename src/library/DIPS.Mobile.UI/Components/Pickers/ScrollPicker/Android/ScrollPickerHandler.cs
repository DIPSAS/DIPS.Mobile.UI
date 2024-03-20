using DIPS.Mobile.UI.Resources.Styles;
using DIPS.Mobile.UI.Resources.Styles.Chip;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using Chip = Google.Android.Material.Chip.Chip;

namespace DIPS.Mobile.UI.Components.Pickers.ScrollPicker;

public partial class ScrollPickerHandler : ViewHandler<ScrollPicker, Chip>
{
    private Chips.Chip m_chip;

    protected override Chip CreatePlatformView()
    {
        m_chip = new DIPS.Mobile.UI.Components.Chips.Chip
        {
            Style = Styles.GetChipStyle(ChipStyle.Input)
        };
        
        return (Chip)m_chip.ToPlatform(MauiContext!);
    }

    protected override void ConnectHandler(Chip platformView)
    {
        base.ConnectHandler(platformView);
        
        platformView.Click += PlatformViewOnClick;
        
        SetChipTitle();
    }
    
    private void SetChipTitle()
    {
        var componentCount = VirtualView.ViewModel.GetComponentCount();
        var texts = new string[componentCount];
        for (var i = 0; i < VirtualView.ViewModel.GetComponentCount(); i++)
        {
            var selectedIndexForComponent = VirtualView.ViewModel.SelectedIndexForComponent(i);
            texts[i] = VirtualView.ViewModel.GetTextForRowInComponent(selectedIndexForComponent, i);
        }
        
        m_chip.Title = texts.Length == 1 ? texts[0] : string.Join(VirtualView.SeparatorText, texts);
    }

    private void PlatformViewOnClick(object? sender, EventArgs e)
    {
        var fragment = new MaterialScrollPickerFragment(VirtualView, SetChipTitle);
        var activity = Platform.CurrentActivity;
        var fragmentManager = activity?.GetFragmentManager();
        fragment.Show(fragmentManager!, nameof(ScrollPicker));
    }

    protected override void DisconnectHandler(Chip platformView)
    {
        base.DisconnectHandler(platformView);
        
        platformView.Click -= PlatformViewOnClick;
    }
}

