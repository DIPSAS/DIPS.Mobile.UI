## InvertedBoolConverter

A converter that can be used with a `Binding` to a boolean value. The converter will return a inverted boolean value.

### Example usage

```xml
<Label Text="Welcome to Xamarin"
    IsVisible="{Binding SomeLogicalProperty, Converter={dui:InvertedBoolConverter}}" />
```

## IsEmptyConverter

A converter that can be used with different input values. It will return a boolean value indicating whether or not the input value is empty. It also has the ability to be inverted.

### Example usage

```xml
<Label Text="Is visible if list is empty"
    IsVisible="{Binding EmptyListOfStrings, Converter={dui:IsEmptyConverter}}" />
```

## IsEmptyToObjectConverter

Converter that takes different input input types and returns a true/false object to indicate if it is empty or not. It also has the ability to be inverted.

### Example usage

```xml
<Label Opacity="{Binding MyText, Converter=IsEmptyToObjectConverter TrueObject=0, FalseObject=1}" />
```

## BoolToObjectConverter

Converters a boolean input value to its respective `TrueObject`/`FalseObject`. Can be used with any object and has the ability to be inverted.

### Example usage

```xml
<Label Opacity="{Binding SomeLogicalProperty, Converter={dui:BoolToObjectConverter TrueObject=0.5, FalseObject=1}}"
    Text="My opacity changes" />
```

## ObjectToBoolConverter

Attempts to convert an object into its respective bool value based on its own equality implementation.

### Example usage
#### String
```xml
<Label IsVisible="{Binding SomeObjectProperty, Converter={dui:ObjectToBoolConverter TrueObject='Hello'}}"
    Text="I'm visible because SomeObjectProperty is 'Hello'!" />
```
#### Float
```xml
<Label IsVisible="{Binding SomeObjectProperty, Converter={dui:ObjectToBoolConverter TrueObject=0.5}}"
    Text="I'm visible because SomeObjectProperty is 0.5!" />
```

## DateConverter

Converts an `DateTime` object with a format and convert it to a readable time string. By default the time will first be converted to the systems local timezone.

### Formats

| Format | English-GB | English-US | Norwegian | Remarks |
|--------|---------|---------|-----------|---------|
Default / Short | 12th Dec 2019 | Dec 12th, 2019 | 12. des 2019 |
Text | Today | Today | I dag | Will return different texts based on the date based on todays date

To skip converting to local time zone use the `IgnoreLocalTime` property

### Example usage

```xml
<Label Text="{Binding Date, Converter={dxui:DateConverter Format=Default}}"
```

## TimeConverter

Converts an DateTime or TimeSpan object with a format and convert it to a readable time string. By default the time will first be converted to the systems local timezone

### Formats

| Format | English-GB | English-US | Norwegian | Remarks |
|--------|---------|---------|-----------|---------|
Default | 13:00 | 01:00 PM | 13:00 | |

To skip converting to local time zone use the `IgnoreLocalTime` property

### Example Usage

```xml
<Label Text="{Binding Time, Converter={dui:TimeConverter Format=Default}}" />
```

## DateAndTimeConverter

Converters a DateTime object with an format and convert it to a readable string. By default the time will first be converted to the systems local timezone.

### Formats

| Format | English-GB | English-US | Norwegian | Remarks |
|--------|---------|---------|-----------|---------|
Default / Short | 12th Dec 1990 13:00 | Dec 12th, 1990 01:00 PM | 12. des 1990 13:00 | |
Text | Today 13:00 | Today 01:00 PM | I dag, kl 13:00 | Will return different date texts based on the date based on todays date |

To skip converting to local time zone use the `IgnoreLocalTime` property

### Example usage

```xml
<Label Text="{Binding Date, Converter={dui:DateAndTimeConverter Format=Default}}" />
```
## MultiplicationConverter

Multiplies the input value with a provided factor.

### Example usage

```xml
<Label  Text="{Binding Value, Converter={dui:MultiplicationConverter Factor=2}}" />
```

## AdditionConverter

Adds the provided value (a term) with a Addend to create a sum

### Example usage

```xml
<Label  Text="{Binding Value, Converter={dui:AdditionConverter Addend=2}}" />
```

## StringCaseConverter

Converters a input string to a desired string case

### Example usage

```xml
<Label Text="{Binding MyString, Converter={dui:StringCaseConverter StringCase=Upper}}"/>
```

## TypeToObjectConverter

Checks the type of the binding against a `Type` and returns a `True` / `False` object accordingly to the type check.

### Example usage

```xml
<Frame BackgroundColor="{Binding CurrentTypeClass, Converter={dui:TypeToObjectConverter Type={x:Type valueconverters:OneTypeClass}, TrueObject=Red, FalseObject=Green}}" />
```

## LogicalExpressionConverter

Converter that takes multiple boolean inputs, runs it through a `LogicalGate` and returns a `True`/`FalseObject`.

> 👉  If `True`/`FalseObject` is not set, it will return boolean `true` / `false`

### Example usage

```xml
<Label Text="MyText">
    <Label.IsVisible>
        <MultiBinding Converter="{dui:LogicalExpressionConverter
                                    LogicalGate=Or}">
            <Binding Path="SomeBoolProperty" />
            <Binding Path="SomeOtherBoolProperty" />
        </MultiBinding>
    </Label.IsVisible>
</Label>
```

### LogicalGate
These are the different options for logical gates:
#### And (default) 
| A | B | Output |
|---|---|--------|
| 0 | 0 | 0      |
| 1 | 0 | 0      |
| 0 | 1 | 0      |
| 1 | 1 | 1      |
#### Nand
| A | B | Output |
|---|---|--------|
| 0 | 0 | 1      |
| 1 | 0 | 1      |
| 0 | 1 | 1      |
| 1 | 1 | 0      |
#### Or
| A | B | Output |
|---|---|--------|
| 0 | 0 | 0      |
| 1 | 0 | 1      |
| 0 | 1 | 1      |
| 1 | 1 | 1      |
#### Nor
| A | B | Output |
|---|---|--------|
| 0 | 0 | 1      |
| 1 | 0 | 0      |
| 0 | 1 | 0      |
| 1 | 1 | 0      |
#### Xor
| A | B | Output |
|---|---|--------|
| 0 | 0 | 0      |
| 1 | 0 | 1      |
| 0 | 1 | 1      |
| 1 | 1 | 0      |
#### Xand
| A | B | Output |
|---|---|--------|
| 0 | 0 | 1      |
| 1 | 0 | 0      |
| 0 | 1 | 0      |
| 1 | 1 | 1      |


## PositionInListConverter

A converter that takes a item and a list as bindings and compare the index of the item with a Position and return a `True`/`FalseObject`

> 👉  If `True`/`FalseObject` is not set, it will return boolean `true` / `false`

### Example usage

You are displaying a list of items and you want all of the items to have a bottom separator except the last item in the list.
This can be achieved by using this converter to set a separator for each item but not the last item by doing so:

```xml
<StackLayout x:Name="ItemsStackLayout"
            BindableLayout.ItemsSource="{Binding Items}"
            Orientation="Vertical">
    <BindableLayout.ItemTemplate>
        <DataTemplate x:DataType="{x:Type x:String}">
            <StackLayout Margin="15,0,0,0">
                <Label Text="{Binding .}"/>
                <BoxView HeightRequest="1"
                        Color="Black">
                    <BoxView.IsVisible>
                        <MultiBinding Converter="{dui:PositionInListConverter Position=Last, Inverted=True}">
                            <Binding /> <!-- The item -->
                            <Binding Path="BindingContext.Items"
                                    Source="{x:Reference ItemsStackLayout}"/> <!-- The list -->
                        </MultiBinding>
                    </BoxView.IsVisible>
                </BoxView>
            </StackLayout>
        </DataTemplate>
    </BindableLayout.ItemTemplate>
</StackLayout>
```
> In this example we are using the `Inverted` property to make sure it returns `true` for all items except the `Last` item.
### Position
This is a string and it accepts both `First` / `Last` and indexes that are inside of the bounds of the list. This means that you can write `Position=1` to have it return `true` for the item at position = 1 and `false` for the rest.

If you do not set the `Position` property it will default to `Last`.

### ObservableCollection

When dealing with an `ObservableCollection` managing items with the `Add()` / `Remove()` method, you will have to force the layout to redraw all items to get the effect. This is because converters does not fire again magically by a `ObservableCollection.CollectionChanged`.

The workaround is to set the entire list with a new modified list and raise property changed to make the UI redraw:

#### Add ([example](https://github.com/DIPSAS/DIPS.Xamarin.UI/blob/66f30393cd0b2c52136b9eb12a59afd3a569e983/src/Samples/DIPS.Xamarin.UI.Samples/Converters/MultiValueConverters/PositionInListConverterPage.xaml.cs#L50))
```csharp
private void AddItem()
{
    var newList = new ObservableCollection<string>(Items);
    newList.Add(<item>);
    Items = newList; //Raises property changed in the `Items.Set`
    ...
```

#### Remove ([example](https://github.com/DIPSAS/DIPS.Xamarin.UI/blob/66f30393cd0b2c52136b9eb12a59afd3a569e983/src/Samples/DIPS.Xamarin.UI.Samples/Converters/MultiValueConverters/PositionInListConverterPage.xaml.cs#L33))
```csharp
private void RemoveItem()
{
    var newList = new ObservableCollection<string>(Items);
    newList.Remove(<item>);
    Items = newList; //Raises property changed in the `Items.Set`
...
```

## ValueConverterGroup

Takes a list of converters and pipes the value through all of them, from first to last.  

```
                    <Label.Opacity>
                        <Binding Path="Digits">
                            <Binding.Converter>
                                <essentials:ValueConverterGroup>
                                    <dui:IsEmptyConverter />
                                    <dui:BoolToObjectConverter TrueObject="0.5" FalseObject="1" />
                                </essentials:ValueConverterGroup>
                            </Binding.Converter>
                        </Binding>
                    </Label.Opacity>

```
