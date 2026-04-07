DIPS delivers a way for you to place a control in a page which will select the content to display based on a variable. This can be useful when you need to display content differently based on some kind of state.

# Usage

To get started, you will need to decide two things:

1. What should be used to differentiate your content?

_This means that you will have to decide some kind of property that should be used to check a state to know what content to pick. This property is called `SelectorItem` and is located on the `ContentControl`._

2. What is the content you want to display?

_This is as simple as a `View`, and you should create a `View` for each of the different content you want to display._

## DataTemplateSelector
When these prerequisites are met, you will need to create your own implementation of MAUI `DataTemplateSelector`, once this is done you need to set the `ContentControl`s `TemplateSelector` property to a new instance of your `DataTemplateSelector`

The responsibility of the selector is to take the `SelectorItem` as a input to the `SelectTemplate` method, where you can select your `DataTemplate` with the content you want to select.

## Example 
```xml
<dui:ContentControl
    SelectorItem="{Binding MyState}">
    <dui:ContentControl.TemplateSelector>
        <namespace:MyTemplateSelector />
    </dui:ContentControl.TemplateSelector>
</dui:ContentControl>
```

> In this example, we have created our own `MyTemplateSelector`. This selector will get passed `MyState` to the `SelectTemplate` method, where we use the state to pick a `DataTemplate` that matches the state. The `SelectTemplate` method will run every time `MyState` changes.

# Tips
For convince, DIPS Mobile UI delivers a set of `DataTemplateSelector` that you can use. These are found [here](https://github.com/DIPSAS/DIPS.Mobile.UI/tree/main/src/library/DIPS.Mobile.UI/Components/Content/DataTemplateSelectors).

# Properties
Inspect the [components properties class](https://github.com/DIPSAS/DIPS.Mobile.UI/blob/main/src/library/DIPS.Mobile.UI/Components/Content/ContentControl.Properties.cs) to further customize and use it.
  