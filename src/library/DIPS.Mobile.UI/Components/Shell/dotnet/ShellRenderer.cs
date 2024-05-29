using DIPS.Mobile.UI.Exceptions;
using Microsoft.Maui.Handlers;

namespace DIPS.Mobile.UI.Components.Shell;

public partial class ShellRenderer : ViewHandler<View, Only_Here_For_UnitTests>
{
    public ShellRenderer(IPropertyMapper mapper, CommandMapper? commandMapper = null) : base(mapper, commandMapper)
    {
    }

    protected override Only_Here_For_UnitTests CreatePlatformView()
    {
        return new Only_Here_For_UnitTests();
    }
}