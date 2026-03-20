A bottom action bar for contextual actions on a page. Use it when people need quick access to primary and secondary actions related to the content they are viewing. On iOS it renders as a native `UIToolbar` with Liquid Glass styling (iOS 26+). On Android it renders as a Material 3 floating pill with elevation.

# Usage

Attach a toolbar to any `ContentPage` via the `BottomToolbar` property:

```xml
<dui:ContentPage.BottomToolbar>
    <dui:Toolbar HorizontalAlignment="End">
        <dui:ToolbarGroup>
            <dui:ToolbarButton Title="Sign" Command="{Binding SignCommand}" />
        </dui:ToolbarGroup>
        <dui:ToolbarGroup>
            <dui:ToolbarButton Icon="{dui:Icons edit_line}" Title="Edit" Command="{Binding EditCommand}" />
            <dui:ToolbarButton Icon="{dui:Icons tab_more_fill}" Title="More actions">
                <dui:ToolbarButton.Menu>
                    <dui:ContextMenu>
                        <dui:ContextMenuItem Title="Copy" Command="{Binding CopyCommand}" />
                        <dui:ContextMenuItem Title="Share" Command="{Binding ShareCommand}" />
                    </dui:ContextMenu>
                </dui:ToolbarButton.Menu>
            </dui:ToolbarButton>
        </dui:ToolbarGroup>
    </dui:Toolbar>
</dui:ContentPage.BottomToolbar>
```

## Groups

Groups visually separate buttons with spacing (iOS) or a separator line (Android). Use multiple groups to distinguish between different categories of actions.

## Button types

Buttons can be **text-only** (set `Title`), **icon-only** (set `Icon`, use `Title` for accessibility), or **menu buttons** (set `Menu` with `ContextMenuItem`s to show a popup on tap).

# Tips and tricks

## Hide on scroll

Set `HidesOnScrollFor` to a scrollable view to automatically hide the toolbar when people scroll down and show it when they scroll up.

Because the toolbar lives in `ContentPage.BottomToolbar` (a separate XAML scope), use the scrollable view's `HandlerChanged` event to set the reference from code-behind:

```xml
<dui:ContentPage.BottomToolbar>
    <dui:Toolbar x:Name="bottomToolbar" />
</dui:ContentPage.BottomToolbar>

<dui:ScrollView x:Name="scrollView" HandlerChanged="OnScrollViewHandlerChanged">
    ...
</dui:ScrollView>
```

```csharp
private void OnScrollViewHandlerChanged(object? sender, EventArgs e)
{
    bottomToolbar.HidesOnScrollFor = scrollView;
}
```

> **NB!** `x:Reference` does not work because the toolbar is in a different XAML name scope. Always set `HidesOnScrollFor` from code-behind.

## Showing and hiding programmatically

Call `Show()` and `Hide()` directly on the toolbar:

```csharp
bottomToolbar.Show();
bottomToolbar.Hide();
```

## Task buttons

Use `ToolbarTaskButton` instead of `ToolbarButton` when the button triggers an async task that has busy, finished, and error states.

### Busy state

Set `IsBusy` to replace the button with a spinner while work is in progress:

```xml
<dui:ToolbarTaskButton Title="Sign"
                       IsBusy="{Binding IsSignBusy}"
                       Command="{Binding SignCommand}" />
```

### Finished state

Set `IsFinished` to replace the button with a checkmark icon for a few seconds to confirm success:

```xml
<dui:ToolbarTaskButton Title="Sign"
                       IsBusy="{Binding IsSignBusy}"
                       IsFinished="{Binding IsSignFinished}"
                       Command="{Binding SignCommand}" />
```

### Error state

Attach a `ToolbarTaskError` to show an error icon when the task fails. People can tap the error icon to learn what went wrong:

```xml
<dui:ToolbarTaskButton Title="Sign"
                       IsBusy="{Binding IsSignBusy}"
                       Command="{Binding SignCommand}">
    <dui:ToolbarTaskButton.Error>
        <dui:ToolbarTaskError HasError="{Binding HasSignError}"
                              ErrorTappedCommand="{Binding SignErrorTappedCommand}" />
    </dui:ToolbarTaskButton.Error>
</dui:ToolbarTaskButton>
```

```csharp
public Command SignErrorTappedCommand => new(async () =>
{
    await DialogService.ShowMessage("Sign failed",
        "The signing service is currently unavailable. Please try again.", "OK");
    HasSignError = false;  // Reset so the user can retry
});
```

> **NB!** When `HasError` is true, the button shows an error icon. Use `ErrorTappedCommand` to present a dialog explaining the error, then set `HasError = false` to restore the button so the user can try again.

### Full task lifecycle example

```csharp
public Command SignCommand => new(async () =>
{
    IsSignBusy = true;
    try
    {
        await PerformSigningAsync();
        IsSignBusy = false;
        IsSignFinished = true;
        await Task.Delay(2000);
        IsSignFinished = false;
        IsSignVisible = false;
    }
    catch
    {
        IsSignBusy = false;
        HasSignError = true;
    }
});
```

> **NOTE:** State priority is **Error > Busy > Finished**. If both `IsBusy` and `HasError` are true, the error icon is shown.

> **NOTE:** The button respects `IsVisible` — a busy-but-hidden button does not show a spinner.

# Properties

Inspect the [components properties class](https://github.com/DIPSAS/DIPS.Mobile.UI/blob/main/src/library/DIPS.Mobile.UI/Components/Toolbar/Toolbar.cs) to further customise and use it.

## ToolbarButton

Inspect the [components properties class](https://github.com/DIPSAS/DIPS.Mobile.UI/blob/main/src/library/DIPS.Mobile.UI/Components/Toolbar/ToolbarButton.Properties.cs) to further customise and use it.

## ToolbarTaskButton

Inspect the [components properties class](https://github.com/DIPSAS/DIPS.Mobile.UI/blob/main/src/library/DIPS.Mobile.UI/Components/Toolbar/ToolbarTaskButton.Properties.cs) to further customise and use it.
