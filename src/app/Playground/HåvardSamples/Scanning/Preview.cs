using DIPS.Mobile.UI.API.Library;
using DIPS.Mobile.UI.Resources.Colors;

namespace Playground.HåvardSamples.Scanning;

public class Preview : Grid
{
    internal ContentView PreviewView { get; }
    public Preview()
    {
        PreviewView = new ContentView();
        Add(PreviewView);
    }
}