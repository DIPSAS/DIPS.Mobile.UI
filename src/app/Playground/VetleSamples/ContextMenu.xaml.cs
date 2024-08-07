using System.Windows.Input;

namespace Playground.VetleSamples;

public partial class ContextMenuTest
{
    public ContextMenuTest()
    {
        InitializeComponent();
    }

    public static readonly BindableProperty TestCommandProperty = BindableProperty.Create(
        nameof(TestCommand),
        typeof(ICommand),
        typeof(ContextMenuTest));

    public ICommand TestCommand
    {
        get => (ICommand)GetValue(TestCommandProperty);
        set => SetValue(TestCommandProperty, value);
    }

    public static readonly BindableProperty TestBoolProperty = BindableProperty.Create(
        nameof(TestBool),
        typeof(bool),
        typeof(ContextMenuTest));

    public bool TestBool
    {
        get => (bool)GetValue(TestBoolProperty);
        set => SetValue(TestBoolProperty, value);
    }
}