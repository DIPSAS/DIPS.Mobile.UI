using MemoryLeakTests.Tests;

namespace MemoryLeakTests;

public partial class MainPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private void PushListItemPage(object? sender, EventArgs e)
    {
        Navigation.PushAsync(new ListItemPage());
    }

    private void SwapLeakyPage(object? sender, EventArgs e)
    {
        Application.Current.MainPage = new ListItemPage();
    }
    
}