using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Components.ResourcesSamples.Icons;

public partial class IconBottomSheet
{
    public IconBottomSheet(string iconName)
    {
        BindingContext = iconName;
        InitializeComponent();
    }
}