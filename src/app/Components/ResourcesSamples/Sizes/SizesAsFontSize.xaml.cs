namespace Components.ResourcesSamples.Sizes;

public partial class SizesAsFontSize
{
    private Dictionary<string, int> m_sizes;
    private Dictionary<string, int> m_allSizes;

    public SizesAsFontSize()
    {
        InitializeComponent();
    }
    
    protected override void OnAppearing()
    {
        base.OnAppearing();
        Sizes = GetSizes();
        m_allSizes = Sizes;
    }
    
    public Dictionary<string, int> Sizes
    {
        get => m_sizes;
        private set
        {
            m_sizes = value;
            OnPropertyChanged();
        }
    }

    private static Dictionary<string, int> GetSizes()
    {
        var theSize = new DIPS.Mobile.UI.Resources.Sizes.Sizes();
        var sizes = new Dictionary<string, int>();
        foreach (var sizePair in theSize)
        {
            if (sizePair.Value is int size)
            {
                if (size == 0) continue;
                sizes.Add(sizePair.Key, size);
            }
        }
        return sizes;
    }
}