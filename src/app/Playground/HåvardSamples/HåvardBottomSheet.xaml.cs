using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DIPS.Mobile.UI.Components.BottomSheets;

namespace Playground.HåvardSamples;

public partial class HåvardBottomSheet : BottomSheet
{
    public HåvardBottomSheet()
    {
        InitializeComponent();
        CloseItCommand = new Command(() => Close());
    }

    public static readonly BindableProperty CloseItCommandProperty = BindableProperty.Create(
        nameof(CloseItCommand),
        typeof(ICommand),
        typeof(HåvardBottomSheet));

    public ICommand CloseItCommand
    {
        get => (ICommand)GetValue(CloseItCommandProperty);
        set => SetValue(CloseItCommandProperty, value);
    }
    private void MenuItem_OnClicked(object sender, EventArgs e)
    {
        this.Close();
    }

    private void CloseAllBottomSheets(object sender, EventArgs e)
    {
        BottomSheetService.CloseAll();
    }

    private void Button_OnClicked(object sender, EventArgs e)
    {
        new BottomSheet()
        {
            Title = "Second bottom sheet",
            Content = new Button() {Text = "Close all", Command = new Command(() => BottomSheetService.CloseAll())}
        }.Open();
    }

    private void Switch_OnToggled(object sender, ToggledEventArgs e)
    {
        if (Positioning == Positioning.Fit)
        {
            Positioning = Positioning.Medium;
        }
        else
        {
            Positioning = Positioning.Fit;

        }
    }
}