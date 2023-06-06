using System.Windows.Input;
using DIPS.Mobile.UI.MVVM;
using DIPS.Mobile.UI.Resources.Colors;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;
using Enum = System.Enum;
using FloatingNavigationButtonService = DIPS.Mobile.UI.Components.Navigation.FloatingNavigationButton.FloatingNavigationButtonService;

namespace Components.ComponentsSamples.Navigation;

public class FloatingNavigationButtonSamplesViewModel : ViewModel
{
    private double m_badgeCount;

    public FloatingNavigationButtonSamplesViewModel()
    {
        ChangeBadgeColorCommand = new Command<string>(ChangeBadgeColor);
        RemoveNavigationMenuButtonCommand = new Command(RemoveNavigationMenuButton);
        AddNavigationMenuButtonCommand = new Command(AddNavigationMenuButton);
        ToggleCommand = new Command(Toggle);

        ColorList = new(DIPS.Mobile.UI.Extensions.Enum.ToList<ColorName>().Select(c => c.ToString()));
    }

    private void Toggle()
    {
        var availability =
            FloatingNavigationButtonService.CheckButtonAvailability(FloatingNavigationButtonSamples.Button1Identifier);
        
        if(availability == null)
            return;
        
        FloatingNavigationButtonService.SetButtonAvailability(FloatingNavigationButtonSamples.Button1Identifier, (bool)!availability);
    }

    public double BadgeCount
    {
        get => m_badgeCount;
        set
        {
            RaiseWhenSet(ref m_badgeCount, value);
            FloatingNavigationButtonService.SetNavigationMenuBadgeCount(FloatingNavigationButtonSamples.Button1Identifier, (int)value);
            FloatingNavigationButtonService.SetNavigationMenuBadgeCount(FloatingNavigationButtonSamples.Button2Identifier, (int)value);
        }
    }

    private void AddNavigationMenuButton()
    {
        FloatingNavigationButtonService.AddNavigationMenuButton(title: "A new button");
    }
    
    private void RemoveNavigationMenuButton()
    {
        FloatingNavigationButtonService.RemoveNavigationMenuButton(FloatingNavigationButtonSamples.Button1Identifier);
    }

    private void ChangeBadgeColor(string selectedColor)
    {
        if(!Enum.TryParse<ColorName>(selectedColor, out var color))
            return;
        
        FloatingNavigationButtonService.ChangeNavigationMenuButtonBadgeColor(FloatingNavigationButtonSamples.Button1Identifier,
            Colors.GetColor(color));
        FloatingNavigationButtonService.ChangeNavigationMenuButtonBadgeColor(FloatingNavigationButtonSamples.Button2Identifier,
            Colors.GetColor(color));
        FloatingNavigationButtonService.ChangeBadgeColor(
            Colors.GetColor(color));
    }

    public ICommand ChangeBadgeColorCommand { get; }
    public ICommand RemoveNavigationMenuButtonCommand { get; }
    public ICommand AddNavigationMenuButtonCommand { get; }

    public List<string> ColorList { get; }
    public ICommand ToggleCommand { get; }
}