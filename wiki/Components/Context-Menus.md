A context menu is a pop-up menu that provides shortcuts for actions related to the tapped item.

# Inspiration
- Android: [Menus](https://developer.android.com/develop/ui/views/components/menus)
- iOS: [Context menus](https://developer.apple.com/design/human-interface-guidelines/context-menus)

# Usage
A context menu can be attached to any `Element`. In this example a context menu is attached to a `Button` with one `ContextMenuItem`. The `ItemClickedCommand` is bound to a `Command`, thus, when any item is tapped, the `Command` will be executed. 
```xml
 <dui:Button Text="Open context menu">
    <dui:ContextMenuEffect.Menu>
        <dui:ContextMenu ItemClickedCommand="{Binding ItemClickedCommand}">
            <dui:ContextMenuItem Title="Tap me" />
        </dui:ContextMenu>
    </dui:ContextMenuEffect.Menu> 
</dui:Button>
```

# Modes
There are two context menu-modes:
- Pressed (default)
- LongPressed

## Pressed
This mode requires people to tap to show a context menu.

```xml
 <dui:Button Text="Open context menu" dui:ContextMenuEffect.Mode="Pressed">
    <dui:ContextMenuEffect.Menu>
        ...
    </dui:ContextMenuEffect.Menu> 
</dui:Button>
```

## LongPressed 
This mode requires people to long-tap the `Element` to show the context menu.

> On iOS a "preview" of the `Element` will be displayed.

```xml
 <dui:Button Text="Open context menu" dui:ContextMenuEffect.Mode="LongPressed">
    <dui:ContextMenuEffect.Menu>
        ...
    </dui:ContextMenuEffect.Menu> 
</dui:Button>
```

# Global Callbacks
Click events on `ContextMenuItem`s can be subscribed to inside `UseDIPSUI()` in `MauiProgram.cs`:

```csharp
...
var builder = MauiApp.CreateBuilder();
builder
    .UseMauiApp<App>()
    .UseDIPSUI(options =>
    {
       options.SetContextMenuItemClickedCallback(OnContextMenuItemClicked);
    });
...

private static void OnContextMenuItemClicked(ContextMenuItem item)
{
    Console.WriteLine($"Clicked context menu item with title {item.Title}!");
}
...
```
# Attaching a Context Menu to a ToolbarItem
To attach a Context Menu to a `ToolbarItem` we have created a `ContextMenuToolbarItem`, which has a `ContextMenu` property. 

## Usage
```xml
<dui:ContentPage.ToolbarItems>
    <dui:ContextMenuToolbarItem Text="Tap">
        <dui:ContextMenuToolbarItem.ContextMenu>
            <dui:ContextMenu Title="Context menu">
                <dui:ContextMenuItem Title="Item 1" />
            </dui:ContextMenu>
        </dui:ContextMenuToolbarItem.ContextMenu>
    </dui:ContextMenuToolbarItem>
</dui:ContentPage.ToolbarItems>
```

> **NB!** Attaching a Context Menu on a `ToolbarItem` is only supported when the Application is run on `Shell`.

## Known Limitations
* Long-Pressed Mode are not supported (yet) on ToolbarItems

## ContextMenu BindingContext Issue
In MAUI, off-tree objects like ContextMenu cannot reliably use BindingContext="{Binding ...}" in XAML, because the binding is evaluated immediately at creation, when the BindingContext is null. It does not re-evaluate when the parent view’s BindingContext is later set.

**Does NOT work**
```xml
<Grid WidthRequest="100" HeightRequest="100"
      BackgroundColor="Red"
      dui:ContextMenuEffect.Mode="LongPressed">
    <dui:ContextMenuEffect.Menu>
        <dui:ContextMenu BindingContext="{Binding ContextMenuViewModel}">
            <dui:ContextMenuItem Title="{Binding Name}" />
        </dui:ContextMenu>
    </dui:ContextMenuEffect.Menu>
</Grid>
```
> ContextMenuViewModel is never resolved because the menu is off-tree and its BindingContext is null when created.

**Works with new property**
```xml
<Grid WidthRequest="100" HeightRequest="100"
      BackgroundColor="Red"
      dui:ContextMenuEffect.Mode="LongPressed"
      dui:ContextMenuEffect.MenuBindingContext="{Binding ContextMenuViewModel}">
    <dui:ContextMenuEffect.Menu>
        <dui:ContextMenu>
            <dui:ContextMenuItem Title="{Binding Name}" />
        </dui:ContextMenu>
    </dui:ContextMenuEffect.Menu>
</Grid>
```
> MenuBindingContext is set in code by the library when the menu is attached.

All child bindings Title="{Binding Name}" now evaluates correctly.

# Properties
Inspect the [components properties class](https://github.com/DIPSAS/DIPS.Mobile.UI/blob/main/src/library/DIPS.Mobile.UI/Components/ContextMenus/ContextMenu.Properties.cs) to further customize and use it.