using DIPS.Mobile.UI.Resources.Sizes;

namespace Components.ResourcesSamples.Sizes;

public partial class SizesAsVisualBoxes
{
    private Dictionary<string, int> m_sizes;
    private Dictionary<string, int> m_allSizes;
    public SizesAsVisualBoxes()
    {
        InitializeComponent();
    }
    
    protected override void OnAppearing()
    {
        base.OnAppearing();
        Sizes = SizeResources.Sizes;
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
