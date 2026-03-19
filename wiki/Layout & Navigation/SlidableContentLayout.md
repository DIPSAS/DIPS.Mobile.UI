DIPS Mobile UI provides a way for you to create a horizontal list of items where the centred item is in focus. This layout is is utilising caching for it to have the best performance possible. People can slide and tap the content.


# Usage
This component lets you provide a range (min and max values) and it will ask you what binding context each item in the list should have. This is done through the `BindingContextFactory` method, which needs to be a `Func<int, object>`. This will get called when needed but the layout. You will then have to create the content, this is done in the `SlidableContentLayout.ItemTemplate`. Give it a `DataTemplate` with the desired content.

If you are in need of doing something with the content when its in focus, you can use the `ItemSelectedCommand`.

# Example
```xml
<slidable:SlidableContentLayout BindingContextFactory="{Binding CreateMyObjectsBasedOnPosition}">
    <slidable:SlidableContentLayout.Config>
        <slidable:SliderConfig MaxValue="20"
                          MinValue="-20"/>
    </slidable:SlidableContentLayout.Config>
    <slidable:SlidableContentLayout.ItemTemplate>
        <DataTemplate x:DataType="{x:Type system:String}">
                <dui:Label Text="{Binding .}"/>
        </DataTemplate>
    </slidable:SlidableContentLayout.ItemTemplate>
</slidable:SlidableContentLayout>
```

```csharp
public Func<int, object> CreateMyObjectsBasedOnPosition => i => $"Number {i}";
```

This displays a horizontal list of 40 labels labels where the text of the label is `Number <item-number>`.


# Properties
Inspect the [components properties class](https://github.com/DIPSAS/DIPS.Mobile.UI/blob/master/src/library/DIPS.Mobile.UI/Components/Slidable/SlidableLayout.Properties.cs) to further customize and use it.