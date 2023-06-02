using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DIPS.Mobile.UI.Components.FloatingActionButtons.FloatingNavigationButton;
using DIPS.Mobile.UI.Resources.Icons;

namespace Components.ComponentsSamples.FloatingActionButtons;

public partial class FloatingNavigationButtonSamples 
{
    public static readonly string Button1Identifier = "button1";
    public static readonly string Button2Identifier = "button2";
    
    public FloatingNavigationButtonSamples()
    {
        InitializeComponent();
    }
    
    protected override void OnAppearing()
    {
        base.OnAppearing();
        
        FloatingNavigationButtonService.AddFloatingNavigationButton(config =>
        {
            config.AddNavigationButton(Button1Identifier, "Button 1", IconName.arrow_right_s_line, new Command(() =>
            {
                if (!FloatingNavigationButtonService.ContainsNavigationMenuButton(Button2Identifier))
                {
                    FloatingNavigationButtonService.AddNavigationMenuButton(Button2Identifier, "Button 2", index: 1);
                }
                else
                {
                    FloatingNavigationButtonService.RemoveNavigationMenuButton(Button2Identifier);
                }
            }));
            config.AddNavigationButton(Button2Identifier, "Button 2", IconName.ascending_fill, new Command(() => { }));
            config.AddNavigationButton(string.Empty, "Button 3",  IconName.ascending_fill, new Command(() => { }));
            config.AddNavigationButton(string.Empty, "Button 4", IconName.descending_fill, new Command(() => { }));
            config.AddNavigationButton(string.Empty, "Button 5", IconName.descending_fill, new Command(() => { }));
            config.AddNavigationButton(string.Empty, "Button 6", IconName.descending_fill, new Command(() => { }));

        });
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();

        FloatingNavigationButtonService.Hide();
    }
}