using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DIPS.Mobile.UI.Components.ListItems;
using DIPS.Mobile.UI.Resources.Icons;

namespace Components.ComponentsSamples.ListItems;

public partial class ListItemsSamples
{
    public ListItemsSamples()
    {
        InitializeComponent();
    }

    private void VisualElement_OnLoaded(object? sender, EventArgs e)
    {
        if (sender is ListItem listItem)
        {
            listItem.Icon = Icons.GetIcon(IconName.image_fill);
        }
    }
}