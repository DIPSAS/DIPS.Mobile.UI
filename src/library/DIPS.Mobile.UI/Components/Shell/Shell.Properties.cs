using DIPS.Mobile.UI.API.Library;

namespace DIPS.Mobile.UI.Components.Shell;

public partial class Shell
{
    public static readonly BindableProperty ShouldGarbageCollectPreviousPageWhenPoppedProperty =
        BindableProperty.Create(
            nameof(ShouldGarbageCollectPreviousPage),
            typeof(bool),
            typeof(Shell));

    /// <summary>
    /// Will start monitoring previous page when popped, popped to root or if the page was removed. It will  print if it was/was not garbage collected the Console.
    /// </summary>
    /// <remarks>This will only run when <see cref="DUI.IsDebug"/></remarks>
    public bool ShouldGarbageCollectPreviousPage
    {
        get => (bool)GetValue(ShouldGarbageCollectPreviousPageWhenPoppedProperty);
        set => SetValue(ShouldGarbageCollectPreviousPageWhenPoppedProperty, value);
    }
}