using DIPS.Mobile.UI.API.Camera.Gallery.BottomSheet.ObserverInterfaces;
using DIPS.Mobile.UI.API.Camera.ImageCapturing.Observers;
using DIPS.Mobile.UI.API.Camera.ImageCapturing.Views.BottomToolbar;
using DIPS.Mobile.UI.API.Camera.ImageCapturing.Views.BottomToolbar.EditState;
using DIPS.Mobile.UI.Resources.LocalizedStrings.LocalizedStrings;

namespace DIPS.Mobile.UI.API.Camera.Gallery.BottomSheet.BottomToolbar;

internal class GalleryBottomSheetBottomToolbar : Grid
{
    public GalleryBottomSheetBottomToolbar()
    {
        Margin = new Thickness(Sizes.GetSize(SizeName.size_5), 0, Sizes.GetSize(SizeName.size_5), 0);
    }

    public void GoToEditState(IImageEditStateObserver observer)
    {
        Clear();

        var editStateBottomView = new EditStateBottomView(observer);
        
        Add(editStateBottomView);
    }
    
    public void GoToDefaultState(IGalleryDefaultStateObserver observer)
    {
        Clear();
        
        var removeButtonWithText = new ButtonWithText(DUILocalizedStrings.Delete, Icons.GetIcon(IconName.delete_line), observer.RemoveImage)
        {
            VerticalOptions = LayoutOptions.Center,
            HorizontalOptions = LayoutOptions.Start
        };
        
        var doneButtonWithText = new ButtonWithText(DUILocalizedStrings.Done, Icons.GetIcon(IconName.check_line), () => observer.Close())
        {
            VerticalOptions = LayoutOptions.Center,
            HorizontalOptions = LayoutOptions.End
        };
        
        Add(removeButtonWithText);
        Add(doneButtonWithText);   
    }
}