using System.Windows.Input;

namespace MemoryLeakTests.Tests;

public partial class ListItemPage
{
    public ListItemPage()
    {
        InitializeComponent();
    }

    private void ListItemPage_OnUnloaded(object? sender, EventArgs e)
    {
    }
}