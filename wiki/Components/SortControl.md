The `SortControl` component is a user interface element designed to provide sorting functionality within the application. It allows users to organize and view data in a preferred order, enhancing usability and accessibility.

### Usage

To use the `SortControl`, you must set the `ItemsSource` property to provide the list of items to sort. Optionally, you can specify the initially selected item and sort order. 

When a user selects an item, the `SelectedItemCommand` is executed. The command parameter is passed as a tuple containing the selected object and the sort order.

#### Example

```xml
<dui:SortControl ItemsSource="{Binding SortOptions}"
                 ItemDisplayProperty="Text"
                 InitialSelectedItem="{Binding InitialSelectedItem}"
                 InitialSortOrder="{Binding InitialSortOrder}"
                 SelectedItemCommand="{Binding SortingDoneCommand}" />
```

#### ItemDisplayProperty

The `ItemDisplayProperty` specifies the name of the property in the `SortOption` object that should be displayed as text in the `SortControl`. This ensures that the correct and meaningful text is shown to the user for each sorting option.

For example, if the `SortOption` class has a property named `Text`, you can set `ItemDisplayProperty="Text"` to display the value of the `Text` property in the UI.


```csharp
public class SortOption
{
    public string Text { get; set; }
    public string Value { get; set; }
}
```

In this case, the `Text` property will be used to display the sorting options in the `SortControl`.

### Properties
Inspect the [SortControl Properties Class](https://github.com/DIPSAS/DIPS.Mobile.UI/blob/main/src/library/DIPS.Mobile.UI/Components/Sorting/SortControl.Properties.cs) to further customize and use it.
