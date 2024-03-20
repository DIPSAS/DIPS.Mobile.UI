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
        m_chip = new DIPS.Mobile.UI.Components.Chips.Chip()
        {
            Style = Styles.GetChipStyle(ChipStyle.EmptyInput)
        };
        
        return (Chip)m_chip.ToPlatform(MauiContext!);
    }

    protected override void ConnectHandler(Chip platformView)
    {
        base.ConnectHandler(platformView);
        
        m_chip.Title = VirtualView.PlaceholderText;
        platformView.Click += PlatformViewOnClick;
    }
    
    private static partial void MapSelectedIndex(ScrollPickerHandler handler, ScrollPicker scrollPicker)
    {
        if(scrollPicker.SelectedIndex == -1)
            return;
        
        handler.m_chip.Style = Styles.GetChipStyle(ChipStyle.Input);
        handler.m_chip.Title = scrollPicker.ItemsSource.Cast<object>().ElementAt(scrollPicker.SelectedIndex).ToString()!;
    }

    private void PlatformViewOnClick(object? sender, EventArgs e)
    {
        var fragment = new MaterialScrollPickerFragment(VirtualView);
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

