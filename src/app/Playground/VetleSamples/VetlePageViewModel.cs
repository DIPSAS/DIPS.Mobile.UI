using System.Collections.ObjectModel;
using System.Windows.Input;
using DIPS.Mobile.UI.Components.Alerting.Dialog;
using DIPS.Mobile.UI.MVVM;

namespace Playground.VetleSamples;

public class VetlePageViewModel : ViewModel
{
    private bool m_isChecked = true;
    private bool m_isProgressing = true;
    private bool m_isError;
    private bool m_isToggled;
    private LayoutOptions m_horizontalOptions;
    private bool m_isEllipsized;
    private int m_maxLines = 3;

    public VetlePageViewModel()
    {
        Navigate = new Command(Navigatee);
        Test = new Command(async () =>
        {
            IsChecked = true;
            await Task.Delay(1000);
            IsChecked = false;
        });

        CompletedCommand = new Command(() => DialogService.ShowMessage("Test", "test", "ok"));

        SetMaxLinesCommand = new Command<string>(s => MaxLines = int.Parse(s));

        _ = Test2();
    }

    public string TestString { get; } = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum";

    public bool IsEllipsized
    {
        get => m_isEllipsized;
        set => RaiseWhenSet(ref m_isEllipsized, value);
    }

    public int MaxLines
    {
        get => m_maxLines;
        set => RaiseWhenSet(ref m_maxLines, value);
    }

    public ICommand SetMaxLinesCommand { get; }

    private async Task Test2()
    {
        IsProgressing = true;
        await Task.Delay(2000);
        IsError = true;
        IsProgressing = false;
        await Task.Delay(2000);
        IsError = false;
    }


    private void Navigatee()
    {
        var page = new VetleTestPage1();
        Shell.Current.Navigation.PushAsync(page);
    }

    public List<string> TestStrings { get; } = new()
    {
        "Test",
        "Test2",
        "Test2",
        "Test2",
        "Test2",
        "Test2",
        "Test2",
        "Test2",
        "Test2",
        "Test2",
        "Test2",
        "Test2",
        "Test2",
        "Test2",
        "Test2",
        "Test2",
        "Test2",
        "Test2",
        "Test2",
                       "Test2",
                       "Test2",
                       "Test2",
                       "Test2",
                       "Test2",
                       "Test2",
                       "Test2",
                       "Test2",
                       "Test2",
                       "Test2",
                       "Test2",
                       "Test2",
                       "Test2",
                       "Test2",
                       "Test2",
                       "Test2",
                       "Test2",
                       "Test2",
                       "Test2",
                       "Test2",
                       "Test2",
                       "Test2",
                       "Test2",
                       "Test2",
                       "Test2",
                       "Test2",
                       "Test2",
                       "Test2",
                       "Test2",
                       "Test2",
                       "Test2"
    };
    
    public ICommand Navigate { get; }
    public ICommand Test { get; }
    
    public ICommand CompletedCommand { get; }

    public bool IsChecked
    {
        get => m_isChecked;
        set => RaiseWhenSet(ref m_isChecked, value);
    }

    public bool IsError
    {
        get => m_isError;
        set => RaiseWhenSet(ref m_isError, value);
    }

    public bool IsProgressing
    {
        get => m_isProgressing;
        set => RaiseWhenSet(ref m_isProgressing, value);
    }

    public LayoutOptions HorizontalOptions
    {
        get => m_horizontalOptions;
        set => RaiseWhenSet(ref m_horizontalOptions, value);
    }

    public bool IsToggled
    {
        get => m_isToggled;
        set
        {
            m_isToggled = value;
            if (value)
            {
                HorizontalOptions = LayoutOptions.Center;
            }
            else
            {
                HorizontalOptions = LayoutOptions.End;
            }
        }
    }
}