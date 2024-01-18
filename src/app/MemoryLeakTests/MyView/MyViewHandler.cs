using Microsoft.Maui.Handlers;

namespace MemoryLeakTests;

public partial class MyViewHandler : ContentViewHandler
{
    internal partial void Connect();
    internal partial void Disconnect();

}