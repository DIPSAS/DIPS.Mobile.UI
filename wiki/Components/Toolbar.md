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

Because the toolbar lives in `ContentPage.BottomToolbar` (a separate XAML scope), use the scrollable view's `Loaded` event to set the reference from code-behind:

```xml
<dui:ContentPage.BottomToolbar>
    <dui:Toolbar x:Name="bottomToolbar" />
</dui:ContentPage.BottomToolbar>

<dui:ScrollView x:Name="scrollView" Loaded="OnScrollViewLoaded">
    ...
</dui:ScrollView>
```

```csharp
private void OnScrollViewLoaded(object? sender, EventArgs e)
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

## Busy state

Set `IsBusy` on a `ToolbarButton` to replace it with a spinner. This is useful for async operations where you want to indicate progress before the button disappears:

```xml
<dui:ToolbarButton Title="Sign"
                   IsVisible="{Binding IsSignVisible}"
                   IsBusy="{Binding IsSignBusy}"
                   Command="{Binding SignCommand}" />
```

```csharp
public Command SignCommand => new(async () =>
{
    IsSignBusy = true;
    await PerformSigningAsync();
    IsSignVisible = false;
    IsSignBusy = false;
});
```

> **NOTE:** The button respects both `IsVisible` and `IsBusy` — a busy-but-hidden button does not show a spinner.

# Properties

Inspect the [components properties class](https://github.com/DIPSAS/DIPS.Mobile.UI/blob/main/src/library/DIPS.Mobile.UI/Components/Toolbar/Toolbar.cs) to further customise and use it.

## ToolbarButton

Inspect the [components properties class](https://github.com/DIPSAS/DIPS.Mobile.UI/blob/main/src/library/DIPS.Mobile.UI/Components/Toolbar/ToolbarButton.Properties.cs) to further customise and use it.
