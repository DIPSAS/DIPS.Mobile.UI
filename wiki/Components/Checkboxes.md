A checkbox is used to let users select one or more options of a limited number of choices.

# Usage
In this example a single checkbox is added and is checked:

```xml
<dui:CheckBox Text="Checkbox title"
              IsSelected="True" />
```

# Properties
Inspect the [Checkbox Properties Class](https://github.com/DIPSAS/DIPS.Mobile.UI/blob/main/src/library/DIPS.Mobile.UI/Components/CheckBoxes/CheckBox.Properties.cs) to further customize and use it.

## FilledCheckBox
`FilledCheckBox` can be used when an action is to be executed when pressed, and will indicate when that action is done by filling up the checkbox.

## Usage
In this example a `FilledCheckBox` is used to save something that takes a while to complete:

```xml
<dui:FilledCheckBox IsProgressing="{Binding IsProgressing}"
                    IsChecked="{Binding IsChecked}"
                    Command="{Binding SaveCommand}"
                    UnCheckedBackgroundColor="{dui:Colors color_neutral_10}"
                    HorizontalOptions="Start"
                    WidthRequest="100"
                    HeightRequest="100"
                    CornerRadius="50" />
```

## Properties
Inspect the [FilledCheckBox Properties Class](https://github.com/DIPSAS/DIPS.Mobile.UI/blob/main/src/library/DIPS.Mobile.UI/Components/CheckBoxes/FilledCheckBox.Properties.cs) to further customize and use it.