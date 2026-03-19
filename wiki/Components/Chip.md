A `Chip` is a compact element that represents e.g. input, attribute or an action. 

# Inspiration
[Material Design 2 - Chips](https://m2.material.io/components/chips/android#using-chips)

# Remarks
iOS does not have a concept of `Chips`, thus, this is implemented using `UIButton`.

# Usage
In the following example the title of the `Chip` is bound. Furthermore a `Command` is also bound to make something happen when you tap on the `Chip`. 
```xml
<dui:Chip Title="Tap me"
          Command="{Binding Something}" />
```

# Properties
Inspect the [components properties class](https://github.com/DIPSAS/DIPS.Mobile.UI/blob/main/src/library/DIPS.Mobile.UI/Components/Chips/Chip.Properties.cs) to further customize and use it.

## ChipGroup
A `ChipGroup` is a component that takes in an ItemsSource, and outputs the items as Chips. They are laid out horizontally and will wrap if there is no horizontal space left.

### SelectionModes
There are two modes: `Single` and `Multi`. `Single` will make sure that only one `Chip` is selected at any time. Tapping a different `Chip` will deselect the current `Chip` and select the tapped `Chip`. However, `Multi` will select every tapped `Chip`.

### Usage
In this example we set the ItemsSource to `Footballer`. To display the `Chip`'s text, we use the property `ItemDisplayProperty` which is `Name` in this case. Also, we have bound to the `SelectedItems`, which is set to Two-Way as default. To be aware of changes to the `SelectedItems` of the `ChipGroup`, you can either do logic in the setter of the `SelectedItems` binding, or subscribe to the event: `OnSelectedItemsChanged`.

>**NB!** The `SelectedItems` property must be bound to a `List<object>`. Otherwise only the changes will only occur from ViewModel -> ChipGroup. And not both ways.

>**NB!** To clear the `SelectedItems` always use an empty `List<object>` and not set the `SelectedItems` to `null`.

```csharp
private List<object> m_selectedItemsFootballers = new() { new Footballer(){Name = "Odegaard"},new Footballer(){Name = "Haaland"} };


public List<object> SelectedItemsFootballers
{
    get => m_selectedItemsFootballers;
    set
    {
        RaiseWhenSet(ref m_selectedItemsFootballers, value);
        
        // Do stuff
    }
}

public class Footballer
{
    public string Name { get; set; }
}
```

```xml
<dui:ChipGroup SelectionMode="Multi"
               ItemsSource="{Binding Footballers}"
               ItemDisplayProperty="Name"
               SelectedItems="{Binding SelectedItemsFootballers, Mode=TwoWay}" />
```

### Properties
Inspect the [components properties class](https://github.com/DIPSAS/DIPS.Mobile.UI/blob/main/src/library/DIPS.Mobile.UI/Components/ChipGroup/ChipGroup.Properties.cs) to further customize and use it.