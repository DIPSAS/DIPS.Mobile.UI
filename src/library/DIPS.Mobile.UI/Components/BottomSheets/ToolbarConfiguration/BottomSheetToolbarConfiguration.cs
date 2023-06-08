using System.Windows.Input;

namespace DIPS.Mobile.UI.Components.BottomSheets.ToolbarConfiguration;

public class BottomSheetToolbarConfiguration : BindableObject
{
    public static readonly BindableProperty TitleProperty = BindableProperty.Create(
        nameof(Title),
        typeof(string),
        typeof(BottomSheetToolbarConfiguration));

    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    public static readonly BindableProperty BackButtonTextProperty = BindableProperty.Create(
        nameof(BackButtonText),
        typeof(string),
        typeof(BottomSheetToolbarConfiguration));

    public string BackButtonText
    {
        get => (string)GetValue(BackButtonTextProperty);
        set => SetValue(BackButtonTextProperty, value);
    }

    public static readonly BindableProperty BackButtonCommandProperty = BindableProperty.Create(
        nameof(BackButtonCommand),
        typeof(ICommand),
        typeof(BottomSheetToolbarConfiguration));

    public ICommand BackButtonCommand
    {
        get => (ICommand)GetValue(BackButtonCommandProperty);
        set => SetValue(BackButtonCommandProperty, value);
    }
   
    
    public static readonly BindableProperty RightToolbarItemsProperty = BindableProperty.Create(
        nameof(RightToolbarItems),
        typeof(List<BottomSheetActionButton>),
        typeof(BottomSheetToolbarConfiguration),
        defaultValue:new List<BottomSheetActionButton>());

    public List<BottomSheetActionButton> RightToolbarItems
    {
        get => (List<BottomSheetActionButton>)GetValue(RightToolbarItemsProperty);
        set => SetValue(RightToolbarItemsProperty, value);
    }
}