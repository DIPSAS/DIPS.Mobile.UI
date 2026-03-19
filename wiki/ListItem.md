![image](https://github.com/DIPSAS/DIPS.Mobile.UI/assets/2527084/7f83d0df-e968-44ca-8661-9f2dbb031d4a)

List item is a component designed to be used in a vertical list. It contains the following visual elements:
1. **Container**
    - The container that holds the view elements.
2. **Icon**
    - The icon for the list item.
3. **Title**
    - The title of the list item.
4. **Subtitle**
    - The sub title of the list item.
5. **InLineContent**
    - A container that holds the content that resides in-line with the title and the subtitle.
6. **UnderlyingContent**
    - A container that holds the content that resides below the Title, Subtitle, Icon and InlineContent.

> Every visual element inside the container is optional.

## Usage
In this example, the `ListItem` contains an `ItemPicker`. The `ItemPicker` will be placed in the `InLineContent` container.
```xml
<dui:ListItem Title="Person">
    <dui:ItemPicker Mode="ContextMenu"
                    Placeholder="Select Person"
                    SelectedItem="{Binding SelectedPerson}"
                    ItemsSource="{Binding People}"
                    ItemDisplayProperty="DisplayName">
    </dui:ItemPicker>
</dui:ListItem>
```

## Customising a ListItem
The container of the `ListItem` is a using a grid system. This means that each visual part has preset width and height constraints. These and more properties that controls the appearance and the behavior of each visual element can be found in the following `ListItem` properties and classes: 

- [TitleOptions](https://github.com/DIPSAS/DIPS.Mobile.UI/blob/main/src/library/DIPS.Mobile.UI/Components/ListItems/Options/Title/TitleOptions.Properties.cs)
- [SubtitleOptions](https://github.com/DIPSAS/DIPS.Mobile.UI/blob/main/src/library/DIPS.Mobile.UI/Components/ListItems/Options/Subtitle/SubtitleOptions.Properties.cs)
- [IconOptions](https://github.com/DIPSAS/DIPS.Mobile.UI/blob/main/src/library/DIPS.Mobile.UI/Components/ListItems/Options/Icon/IconOptions.Properties.cs)
- [InLineContentOptions](https://github.com/DIPSAS/DIPS.Mobile.UI/blob/main/src/library/DIPS.Mobile.UI/Components/ListItems/Options/InLineContent/InLineContentOptions.Properties.cs)
- [DividerOptions](https://github.com/DIPSAS/DIPS.Mobile.UI/blob/main/src/library/DIPS.Mobile.UI/Components/ListItems/Options/ContextMenu/ContextMenuOptions.Properties.cs)
- [ContextMenuOption](https://github.com/DIPSAS/DIPS.Mobile.UI/blob/main/src/library/DIPS.Mobile.UI/Components/ListItems/Options/Dividers/DividersOptions.Properties.cs)
- [ListItem Properties Class](https://github.com/DIPSAS/DIPS.Mobile.UI/blob/main/src/library/DIPS.Mobile.UI/Components/ListItems/ListItem.Properties.cs)

These can be set like so:
```xml
<dui:ListItem>
    <dui:ListItem.IconOptions>
        <dui:IconOptions ... />
    </dui:ListItem.IconOptions>

    <dui:ListItem.TitleOptions>
        <dui:TitleOptions ... />
    </dui:ListItem.TitleOptions>
    
    <dui:ListItem.SubtitleOptions>
        <dui:SubtitleOptions ... />
    </dui:ListItem.SubtitleOptions>

    <dui:ListItem.InLineContentOptions>
        <dui:InLineContentOptions ... />
    </dui:ListItem.InLineContentOptions>

    <dui:ListItem.DividersOptions>
        <dui:DividersOptions ... />
    </dui:ListItem.DividersOptions>
    
    <dui:ListItem.ContextMenuOptions>
        <dui:ContextMenuOptions ... />
    </dui:ListItem.ContextMenuOptions>
</dui:ListItem>
```

### Example
In this example, `InLineContent` is set to be horizontally at the start. This makes the gap between the `Title` and the `InLineContent` smaller.

```xml
<dui:ListItem Title="Test">
    <dui:ListItem.InLineContentOptions>
        <inLineContent:Options HorizontalOptions="Start" />
    </dui:ListItem.InLineContentOptions>
    
    <dui:ItemPicker Mode="ContextMenu"
                    Placeholder="Person"
                    SelectedItem="{Binding SelectedPerson}"
                    ItemsSource="{Binding People}"
                    ItemDisplayProperty="DisplayName" />
</dui:ListItem>
```

## Tips and tricks
A `TypeConverter` is in place when setting the `InLineContent` and `UnderlyingContent` directly on the `ListItem`. In this example a label will be automatically generated in the `InLineContent` container:

```xml
<dui:ListItem Title="Test"
              InLineContent="InLineContent" />
```

## List Item Extensions
We have made extensions to `ListItem` to remove boilerplate code for something that the consumers often has use of. 

### NavigationListItem
`NavigationListItem` is derived from `ListItem`, and the purpose of the component is to automatically place a navigation icon in the `InLineContent` container. However, if you explicitly set the `InLineContent`, it will be placed alongside the navigation icon.

#### Usage
```xml
  <dui:NavigationListItem Title="Navigate"
                          Command="{Binding NavigationCommand}"
                          InLineContent="I am with navigation icon" />
```

#### Properties
Inspect the [NavigationListItem Properties Class](https://github.com/DIPSAS/DIPS.Mobile.UI/blob/main/src/library/DIPS.Mobile.UI/Components/ListItems/Extensions/NavigationListItem.Properties.cs) to further customise and use it.


### LoadableListItem
`LoadableListItem`'s purpose is to display a ListItem that has a built-in spinner for something to load. It also has the capacity to let users know that something went wrong. The `InLineContent` will be displayed only when the `LoadableListItem` is not busy or and nothing went wrong.

#### Usage
This is an example where the InLineContent will be displayed when both IsBusy and IsError is false.
```xml
<dui:LoadableListItem IsBusy="{Binding IsBusy}"
                      IsError="{Binding IsError}"
                      Title="LoadableListItem"
                      InLineContent="All systems working!" />
```

#### Properties
Inspect the [LoadableListItem Properties Class](https://github.com/DIPSAS/DIPS.Mobile.UI/blob/36e9e0455a47492bd440a5b6c8672188b76be481/src/library/DIPS.Mobile.UI/Components/ListItems/Extensions/LoadableListItem.Properties.cs#L13) to further customise and use it.

## Accessibility with Interactive Content

When a `ListItem` contains interactive controls (like `Switch`, `Button`, or `Entry`), screen readers may focus on the ListItem's title and subtitle before reaching the interactive element. This creates unnecessary navigation steps for users with screen readers.

To improve accessibility in these scenarios, use the `DisableInternalAccessibility` property:

```xml
<dui:ListItem Title="Enable notifications"
              Subtitle="Receive alerts when new messages arrive"
              DisableInternalAccessibility="True">
    <dui:Switch SemanticProperties.Description="Enable notifications. Receive alerts when new messages arrive" />
</dui:ListItem>
```

**Important:** When using `DisableInternalAccessibility="True"`, always set `SemanticProperties.Description` on the interactive element with descriptive text that includes the context from the title/subtitle. This ensures screen reader users get the full information when they reach the control.

For more details and best practices, see the [ListItem with Interactive Content](Accessibility#listitem-with-interactive-content) section in the Accessibility documentation.
