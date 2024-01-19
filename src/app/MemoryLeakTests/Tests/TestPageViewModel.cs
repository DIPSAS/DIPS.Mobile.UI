using System.Windows.Input;
using DIPS.Mobile.UI.MVVM;

namespace MemoryLeakTests.Tests;

public class TestPageViewModel : ViewModel, IDisposable
{
    private List<string> m_items;
    private string m_title;

    public TestPageViewModel()
    {
        Title = nameof(TestPageViewModel);
        Command = new Command(() => { });

        var items = new List<string>();
        for (int i = 0; i < 100; i++)
        {
            items.Add($"Item {i}");
        }

        Items = items;
    }

    public string Title
    {
        get => m_title;
        set => RaiseWhenSet(ref m_title, value);
    }

    public List<string>? Items
    {
        get => m_items;
        set => RaiseWhenSet(ref m_items, value);
    }

    public ICommand Command { get; }

    public void Dispose()
    {
        Items = null;
    }
}