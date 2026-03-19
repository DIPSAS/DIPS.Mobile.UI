A `Picker` is a component where you can select something. We have several different Pickers in our library, which are found below.

# Item Picker
An item picker should be used by you to let people pick an item from a list of items. The item picker is an in-line button with a title that people tap to start picking from the items. When an item is picked, the item will be displayed as a part of the in-line button. 

## Usage
In the following example, the item picker is populated by a list of `Person` named `People` , where `Person` have a a property `DisplayName` to be used for people to see when picking the item from the item picker.

```xml
 <dui:ItemPicker Title="Person"
                 Mode="BottomSheet"
                 ItemsSource="{Binding People}"
                 SelectedItem="{Binding PickedPerson}"
                 ItemDisplayProperty="DisplayName"/>
```

## UX Considerations
You should select a proper `Mode` for the item picker depending on the size of each item and the length of the items to pick from. `ContextMenu` mode is preferred if the number of items are short and the length of each item to display is short, otherwise `BottomSheet` is preferred. 

## Customise each item
You can customise how the item for people to pick look. This can only be done when the item picker is presented as a bottom sheet.

```xml
<dui:ItemPicker Mode="BottomSheet"
                SelectedItem="{Binding SelectedPerson}"
                ItemsSource="{Binding People}"
                ItemDisplayProperty="DisplayName">
    <dui:ItemPicker.BottomSheetPickerConfiguration>
        <dui:BottomSheetPickerConfiguration>
            <dui:BottomSheetPickerConfiguration.SelectableItemTemplate>
                <ControlTemplate>
                    <dui:Label Text="{TemplateBinding Item.FirstName}"
                               BackgroundColor="{TemplateBinding IsSelected, Converter={dui:BoolToObjectConverter 
                                                   TrueObject={dui:Colors color_success_light}, 
                                                   FalseObject={dui:Colors color_error_light}}}" />
                </ControlTemplate>
            </dui:BottomSheetPickerConfiguration.SelectableItemTemplate>
        </dui:BottomSheetPickerConfiguration>
    </dui:ItemPicker.BottomSheetPickerConfiguration>
</dui:ItemPicker>
```
This is achieved by using `ControlTemplate` and `TemplateBinding`. Template bindings let you bind to the library properties. The properties you can bind to is:
- `Item` - Your object in the list of items to pick from. The items is the ones you set in `ItemPicker.ItemsSource`.
- `IsSelected` - A boolean property that determines if people have selected your object or not. Use this to customise your view so people know that the item is selected or not.

## Sizing
The `ItemPicker` supports two sizes: `Small` (default) and `Large`. When set to `Large`, the picker will expand to take up the full horizontal width and display an icon on the right side.

To set the size, use the `Size` property:

```xml
<dui:ItemPicker Title="Person"
                Size="Large"
                ItemsSource="{Binding People}"
                SelectedItem="{Binding PickedPerson}"
                ItemDisplayProperty="DisplayName"/>
```

Use the `Large` size when you want the picker to be more prominent or to match the width of other large input controls.

## AdditionalContextMenuItem
You can add an additional context menu item to the picker using the `AdditionalContextMenuItem` property. This is useful if you want to provide an action in the context menu that is not a selectable item (for example, to add a new item or trigger a custom command).

```xml
<dui:ItemPicker Title="Person"
                Mode="ContextMenu"
                ItemsSource="{Binding People}"
                SelectedItem="{Binding PickedPerson}"
                ItemDisplayProperty="DisplayName">
    <dui:ItemPicker.AdditionalContextMenuItem>
        <dui:ContextMenuItem Text="Add new person"
                             Command="{Binding AddPersonCommand}" />
    </dui:ItemPicker.AdditionalContextMenuItem>
</dui:ItemPicker>
```

The additional context menu item will appear at the bottom of the context menu and can execute a command or action when tapped. Note that this item is not selectable as a picker value.

## Add a footer
You can add a footer to the selection list by setting the `FooterTemplate` property on the `BottomSheetPickerConfiguration`.

```xml
<dui:ItemPicker Mode="BottomSheet"
                SelectedItem="{Binding SelectedPerson}"
                ItemsSource="{Binding People}"
                ItemDisplayProperty="DisplayName">
    <dui:ItemPicker.BottomSheetPickerConfiguration>
        <dui:BottomSheetPickerConfiguration>
            <dui:BottomSheetPickerConfiguration.FooterTemplate>
                <DataTemplate>
                    <dui:AlertView Style="{dui:Styles Alert=Information}"
                                   Title="Here you can write an alert for your user."/>
                </DataTemplate>
            </dui:BottomSheetPickerConfiguration.FooterTemplate>
        </dui:BottomSheetPickerConfiguration>
    </dui:ItemPicker.BottomSheetPickerConfiguration>
</dui:ItemPicker>
```

## Properties
Inspect the [components properties class](https://github.com/DIPSAS/DIPS.Mobile.UI/blob/main/src/library/DIPS.Mobile.UI/Components/Pickers/ItemPicker/ItemPicker.Properties.cs) to further customise and use it.

<br />

# Multi Items Picker
A multi items picker should be used by you to let people pick an multiple items from a list of items. The item picker is an in-line button with a placeholder that people tap to start picking from the items. When an item is picked, the items will be displayed as a horizontal list of items. When the max size of the parents width is reached, the items will be merged together to a single number of items.

>**NB!** The `SelectedItems` property must be bound to a `List<object>`. Otherwise only the changes will only occur from ViewModel -> ChipGroup. And not both ways.

## Usage
In this example, people can select people to send a message to. When people pick items the `SelectedItemsCommand` property will be executed with the list of items. You can also use the `SelectedItems` property to pre-set or get updates when people select items.
```xml
<dui:MultiItemsPicker Placeholder="{x:Static localizedStrings:LocalizedStrings.To}"
                      ItemsSource="{x:Static sampleData:SampleDataStorage.People}"
                      SelectedItemsCommand="{Binding YourCommand}">
</dui:MultiItemsPicker>
```

## Customise each item
This can be done the exact same way as with [ItemPicker](https://github.com/DIPSAS/DIPS.Mobile.UI/wiki/Pickers#customise-each-item).

## Resetting selected items
You can set the `ResetBehaviour` property on the `MultiItemsPicker` to allow people to reset their selected items. This will display a button which will by default clear all selected items when pressed. The `ResetBehaviour.Command` property can optionally be set to override how the list should be reset. This is for example useful if the picker should have a default selection.

```xml
<dui:MultiItemsPicker ItemsSource="{Binding Items}"
                      SelectedItems="{Binding SelectedItems}">
    <dui:MultiItemsPicker.ResetBehaviour>
        <dui:ResetBehaviour Command="{Binding ResetCommand}"/>
    </dui:MultiItemsPicker.ResetBehaviour>
</dui:MultiItemsPicker>
```

```csharp

// Example for setting default values on reset.
ResetCommand = new Command(() => SelectedItems = /* Initialize a list of default selections here */);

```


## Add a footer
This can be done the exact same way as with [ItemPicker](https://github.com/DIPSAS/DIPS.Mobile.UI/wiki/Pickers#add-a-item).

## Properties
Inspect the [components properties class](https://github.com/DIPSAS/DIPS.Mobile.UI/blob/mainsrc/library/DIPS.Mobile.UI/Components/Pickers/MultiItemsPicker/MultiItemsPicker.Properties.cs) to further customise and use it.


# Date/Time Pickers
There are three types of date/time pickers:
- [DatePicker](#date-picker)
- [TimePicker](#time-picker)
- [DateAndTimePicker](#dateandtimepicker)

## Inspiration
- Android: [Material Design 2 - Date Pickers](https://m2.material.io/components/date-pickers/android#using-date-pickers) & [Material Design 2 - Time Pickers](https://m2.material.io/components/time-pickers)
- iOS: [Pickers - Human Interface Guidelines](https://developer.apple.com/design/human-interface-guidelines/components/selection-and-input/pickers/)

<br />

## Date Picker
A date picker should be used by you to let people pick a date. The date picker is an in-line button with a title that people tap to start picking the date. When a date is picked, the date will be displayed as a part of the in-line button.

### Usage
```xml
<dui:DatePicker SelectedDate="{Binding SelectedBirthday}"/>
```

### Properties
Inspect the [components properties class](https://github.com/DIPSAS/DIPS.Mobile.UI/blob/main/src/library/DIPS.Mobile.UI/Components/Pickers/DatePicker/DatePicker.Properties.cs) to further customise and use it.

<br />

## HorizontaInlineDatePicker
If you need to display a horizontal line that people can swipe to select dates, you can use the `HorizontalInlineDatePicker`. People can tap the selected date to get a native `DatePicker` to select the date from.

### Usage
```xml
<dui:HorizontaInlineDatePicker SelectedDate="{Binding SelectedBirthday}"/>
```

### Properties
Inspect the [components properties class](https://github.com/DIPSAS/DIPS.Mobile.UI/blob/main/src/library/DIPS.Mobile.UI/Components/Pickers/DatePicker/HorizontalInLine/HorizontalInlineDatePicker.Properties.cs) to further customise and use it.

## Time Picker
A `TimePicker` should be used by you to let people pick a time. The `TimePicker` is an in-line button with a title that people tap to start picking the time. When a time is picked, the time will be displayed as part of the in-line button.

### Usage
```xml
<dui:DateAndTimePicker SelectedTime="{Binding SelectedGroceryShopping}" />
```

### Properties
Inspect the [components properties class](https://github.com/DIPSAS/DIPS.Mobile.UI/blob/main/src/library/DIPS.Mobile.UI/Components/Pickers/TimePicker/TimePicker.Properties.cs) to further customize and use it.

<br />

## DateAndTime Picker
A `DateAndTimePicker` should be used by you to let people pick both a date -and time. The `DateAndTimePicker` is two in-line buttons with a title that people tap to start picking the date or time. When date is picked, the date or time will be displayed as part of the in-line button.

### Usage
```xml
<dui:DateAndTimePicker SelectedDateTime="{Binding SelectedDeadline}"/>
```

### Properties
Inspect the [components properties class](https://github.com/DIPSAS/DIPS.Mobile.UI/blob/main/src/library/DIPS.Mobile.UI/Components/Pickers/DateAndTimePicker/DateAndTimePicker.Properties.cs) to further customize and use it.

# SegmentedControl
SegmentedControl lets people pick a single or multiple item(s) from a in line horizontal scrollable list of item. When people select an item the item will be marked as checked. 

## Usage
```xml
<dui:SegmentedControl ItemsSource="{Binding People}" SelectedItem="{Binding PickedPerson}" ItemDisplayProperty="DisplayName" SelectionMode="Single" />
```

## Detecting people selecting items
There is different ways of detecting when people select an item from the segmented control.
- Events, `DidSelectItem` / `DidDeSelectItem`
- Commands, `SelectedItemCommand` / `DeSelectedItemCommand`
- Properties, `SelectedItem` (`SelectionMode=Single`) / `SelectedItems` (`SelectionMode=Multi`)

## Properties
Inspect the [components properties class](https://github.com/DIPSAS/DIPS.Mobile.UI/blob/main/src/library/DIPS.Mobile.UI/Components/Pickers/SegmentedControl/SegmentedControl.Properties.cs) to further customize and use it.

# Scroll Picker
A `ScrollPicker` is a view that displays one or more wheels that the user can manipulate to select items. Use this picker when there are a lot of items instead of an `ItemPicker`. In this example the name `Component` comes up. The name means a wheel, so if there are three components, there are three wheels for the user to manipulate to select items.

## Implementations
The implementations on each platform are implemented natively.
* **iOS**: [UIPickerView](https://developer.apple.com/documentation/uikit/uipickerview)
* **Android**: [NumberPicker](https://developer.android.com/reference/android/widget/NumberPicker)

## Usage
To create a `ScrollPicker`:

```xml
<dui:ScrollPicker Title="Title"
                  Components="{Binding Components}" />
```

>`Title` is Android-specific, it displays a title above the wheel(s) in its [DialogFragment](https://developer.android.com/reference/android/app/DialogFragment).


To feed the `ScrollPicker` the values you need it to display, the `Components` property is used, which takes in a list of `IScrollPickerComponent`. Each element of `IScrollPickerComponent` represents a `Component`. The `ScrollPicker` uses the interface to display the items, and set the selected item. We have made a `BaseScrollPickerComponent` so that the implementation requires less boilerplate. You have two choices here: 
* Use the `StandardScrollPickerComponent`, which takes in a `List<T>`. It will take care of everything for you, and is able to set a pre-selected item, and callback when the selected index changes.
* Create an implementation of `BaseScrollPickerComponent` and take care of everything yourself. An example can be to display the years from 0 - 9999:

```cs
public class YearScrollPickerComponent : BaseScrollPickerComponent
{
    private const int StartYear = 0;
    private const int EndYear = 9999;

    public override int GetItemsCount()
    {
        return EndYear - StartYear + 1;
    }

    protected override int GetDefaultIndex()
    {
        return DateTime.Now.Year - 1;
    }

    protected override bool ShouldBeNullable()
    {
        return true;
    }

    protected override bool ShouldDefaultValueOnlyBeSetOnOpen()
    {
        return false;
    }

    public override string GetTextAtIndex(int index)
    {
        return (StartYear + index).ToString();
    }
}
```

Example using `StandardScrollPickerComponent` to display two wheels:
```cs
public Test()
{
    var englishFootballers = new List<string> { "Foden", "Kane", "Rashford" };
    var norwegianFootballers = new List<string> { "Haaland", "Odegaard", "Sørloth" };

    var englishFootballersComponent = new StandardScrollPickerComponent<string>(englishFootballers, 1);
    var norwegianFootballersComponent = new StandardScrollPickerComponent<string>(norwegianFootballers);

    Components = [englishFootballersComponent, norwegianFootballersComponent];
}
```

Example using a custom implementation of `IScrollPickerComponent` to display one wheel:

```cs
    var yearsComponent = new YearScrollPickerComponent();

    Components = [yearsComponent];
```

To update the `SelectedItem` without requiring the consumers' tap, you can call the `SetSelectedItem` function and for the UI to update you must call `InvalidateData`:

```cs
public void SetSelectedItemToIndexZero()
{
    var englishFootballersComponent = Components[0];

    englishFootballersComponent[0].SetSelectedItem(0);
    englishFootballersComponent[0].InvalidateData();
}
```

For more examples check out [ScrollPickersSamples.xaml](https://github.com/DIPSAS/DIPS.Mobile.UI/blob/main/src/app/Components/ComponentsSamples/Pickers/ScrollPickersSamples.xaml) and [ScrollPickerSamplesViewModel.cs](https://github.com/DIPSAS/DIPS.Mobile.UI/blob/main/src/app/Components/ComponentsSamples/Pickers/ScrollPickerSamplesViewModel.cs).