using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playground.VetleSamples;

public partial class SegmentedControl
{
    private const string BoldCharacter = "𝐁";
    private const string UnderlineCharacter = "U̲";
    private const string ItalicCharacter = "𝘐";

    public SegmentedControl()
    {
        InitializeComponent();

        ItemsSource = new List<string> { BoldCharacter, UnderlineCharacter, ItalicCharacter };
    }

}