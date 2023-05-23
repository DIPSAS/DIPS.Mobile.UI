using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Components.Resources.LocalizedStrings;
using Components.ResourcesSamples.Colors;
using DIPS.Mobile.UI.Resources.Sizes;
using DIPS.Mobile.UI.Sizes.Sizes;
using Enum = DIPS.Mobile.UI.Extensions.Enum;

namespace Components.ResourcesSamples.Sizes;

public partial class SizesSamples
{
    private Dictionary<string, int> m_sizes;
    private Dictionary<string, int> m_allSizes;

    public SizesSamples()
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
                sizes.Add(sizePair.Key, size);
            }
        }
        return sizes;
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