using System.Collections.ObjectModel;
using System.Windows.Input;
using DIPS.Mobile.UI.MVVM;

namespace Components.ComponentsSamples.CollectionView;

public class FlatCollectionViewSampleViewModel : ViewModel
{
    private int m_counter;

    private bool m_autoCornerRadius = true;
    private bool m_autoHideLastDivider = true;

    public FlatCollectionViewSampleViewModel()
    {
        AddFirstCommand = new Command(() =>
        {
            Items.Insert(0, $"Item {++m_counter} (added first)");
        });

        AddMiddleCommand = new Command(() =>
        {
            var middle = Items.Count / 2;
            Items.Insert(middle, $"Item {++m_counter} (added middle)");
        });

        AddLastCommand = new Command(() =>
        {
            Items.Add($"Item {++m_counter} (added last)");
        });

        RemoveFirstCommand = new Command(() =>
        {
            if (Items.Count > 0)
                Items.RemoveAt(0);
        });

        RemoveLastCommand = new Command(() =>
        {
            if (Items.Count > 0)
                Items.RemoveAt(Items.Count - 1);
        });

        ClearCommand = new Command(() =>
        {
            Items.Clear();
        });

        ResetCommand = new Command(Reset);

        Reset();
    }

    public ObservableCollection<string> Items { get; } = [];

    public bool AutoCornerRadius
    {
        get => m_autoCornerRadius;
        set => RaiseWhenSet(ref m_autoCornerRadius, value);
    }

    public bool AutoHideLastDivider
    {
        get => m_autoHideLastDivider;
        set => RaiseWhenSet(ref m_autoHideLastDivider, value);
    }

    public ICommand AddFirstCommand { get; }
    public ICommand AddMiddleCommand { get; }
    public ICommand AddLastCommand { get; }
    public ICommand RemoveFirstCommand { get; }
    public ICommand RemoveLastCommand { get; }
    public ICommand ClearCommand { get; }
    public ICommand ResetCommand { get; }

    private void Reset()
    {
        Items.Clear();
        m_counter = 0;

        for (var i = 1; i <= 8; i++)
        {
            Items.Add($"Item {++m_counter}");
        }
    }
}
