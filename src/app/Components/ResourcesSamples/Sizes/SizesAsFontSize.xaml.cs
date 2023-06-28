using DIPS.Mobile.UI.Resources.Sizes;

namespace Components.ResourcesSamples.Sizes;

public partial class SizesAsFontSize
{
    private Dictionary<string, int> m_sizes;

    public SizesAsFontSize()
    {
        InitializeComponent();
    }
    
    protected override void OnAppearing()
    {
        base.OnAppearing();
        Sizes = SizeResources.Sizes.Where(pair => pair.Value > 0).ToDictionary(pair => pair.Key, pair => pair.Value);
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

 
}
