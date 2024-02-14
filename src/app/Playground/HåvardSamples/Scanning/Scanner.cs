using Android.Media;

namespace Playground.HåvardSamples.Scanning;

public partial class Scanner
{
    public partial Task<string> Start(Preview preview);
    public partial void Stop();
}