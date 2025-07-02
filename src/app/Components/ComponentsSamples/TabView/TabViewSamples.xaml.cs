namespace Components.ComponentsSamples.TabView;

public partial class TabViewSamples
{
    public TabViewSamples()
    {
        InitializeComponent();
    }
    
    private string m_tab1Title = "Tab 1";
    private int m_tab1Counter = 5;
    private string m_tab2Title = "Tab 2";
    private int m_tab2Counter = 10;

    public string Tab1Title
    {
        get => m_tab1Title;
        set
        {
            m_tab1Title = value;
            OnPropertyChanged();
        }
    }

    public int Tab1Counter
    {
        get => m_tab1Counter;
        set
        {
            m_tab1Counter = value;
            OnPropertyChanged();
        }
    }

    public string Tab2Title
    {
        get => m_tab2Title;
        set
        {
            m_tab2Title = value;
            OnPropertyChanged();
        }
    }

    public int Tab2Counter
    {
        get => m_tab2Counter;
        set
        {
            m_tab2Counter = value;
            OnPropertyChanged();
        }
    }
}