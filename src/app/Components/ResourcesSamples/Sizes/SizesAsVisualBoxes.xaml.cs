using DIPS.Mobile.UI.Resources.Sizes;

namespace Components.ResourcesSamples.Sizes;

public partial class SizesAsVisualBoxes
{
    private Dictionary<string, double> m_sizes;
    private Dictionary<string, double> m_allSizes;
    public SizesAsVisualBoxes()
    {
        InitializeComponent();
    }
    
    protected override void OnAppearing()
    {
        base.OnAppearing();
        Sizes = SizeResources.Sizes.Where(pair => pair.Value > 0).ToDictionary(pair => pair.Key, pair => pair.Value);
        m_allSizes = Sizes;
    }
    
    public Dictionary<string, double> Sizes
    {
        get => m_sizes;
        private set
        {
            m_sizes = value;
            OnPropertyChanged();
        }
    }

    private void InputView_OnTextChanged(object sender, TextChangedEventArgs e)
    {
        if (string.IsNullOrEmpty(e.NewTextValue))
        {
            Sizes = m_allSizes;
        }
        else
        {
            var matchingSizes = m_allSizes.Where(c => c.Key.ToLower().Contains(e.NewTextValue.ToLower()));
            Sizes = matchingSizes.ToDictionary(matchingSize => matchingSize.Key,
                matchingSize => matchingSize.Value);
        }
    }
}
