using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DIPS.Mobile.UI.Components.Pickers.ItemPicker;

namespace Playground.VetleSamples;

public partial class ViewCellTest : ContentView
{
    public ViewCellTest()
    {
        InitializeComponent();
    }

    public static readonly BindableProperty TestCommandProperty = BindableProperty.Create(
        nameof(TestCommand),
        typeof(ICommand),
        typeof(ViewCellTest));

    public ICommand TestCommand
    {
        get => (ICommand)GetValue(TestCommandProperty);
        set => SetValue(TestCommandProperty, value);
    }

    public static readonly BindableProperty TestBoolProperty = BindableProperty.Create(
        nameof(TestBool),
        typeof(bool),
        typeof(ViewCellTest));

    public bool TestBool
    {
        get => (bool)GetValue(TestBoolProperty);
        set => SetValue(TestBoolProperty, value);
    }

    private void ItemPicker_OnDidSelectItem(object sender, object e)
    {
        if(BindingContext is not TimePlanningViewModel timePlanningViewModel)
            return;

        if(sender is not ItemPicker itemPicker)
            return;

        if (itemPicker.SelectedItem == "Test2")
        {
            timePlanningViewModel.Update();
        }
    }
}