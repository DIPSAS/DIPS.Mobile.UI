using DIPS.Mobile.UI.Exceptions;
using Microsoft.Maui.Handlers;


namespace DIPS.Mobile.UI.Components.Searching;

internal partial class  SearchBarHandler : ViewHandler<SearchBar, Microsoft.Maui.Controls.View>
{
    public SearchBarHandler(IPropertyMapper mapper, CommandMapper? commandMapper = null) : base(mapper, commandMapper) => throw new Only_Here_For_UnitTests();

    protected override View CreatePlatformView() => throw new Only_Here_For_UnitTests();
}