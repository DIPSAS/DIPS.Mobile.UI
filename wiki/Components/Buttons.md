A button initiates an instantaneous action when people tap it.

# Styles
A button can change its visual style based on size, color, and shape. The styles available for you to use can be found [here](https://github.com/DIPSAS/DIPS.Mobile.UI/blob/main/src/library/DIPS.Mobile.UI/Resources/Styles/Button/ButtonStyle.cs).

> By default the button's style is set to `PrimaryLarge`.

The styles are grouped into roles:
- **Primary** - Use primary button for the main action or to move the people to the next step in their flow.
> Example use case: Navigate people to their the next step in a step-by-step process, or to submit a form.
- **Secondary** - Use secondary buttons for secondary actions.
> Example use case: Allow people to get back to a previous step in the process, or as non-necessary actions.
- **Ghost** - Use ghost buttons for alternative/tertiary actions.
> Example use case: You want to offer people another feature which is not a part of the main flow or relevant for everyone. 


# Usage
## Using styles

```xml
<dui:Button Style="{dui:Styles Button=SecondaryLarge}"          
            Text="Button"
            Command="{Binding Something}" />
```

## Rounded button with a image
```xml
<dui:Button Style="{dui:Styles Button=SecondaryRoundedLarge}"          
            ImageSource="{dui:Icons bell_line}"
            Command="{Binding Something}" />
```
> Do not use `Text` with a `ImageSource` when creating rounded buttons, as this will lead to bad UX.

## Text and image
```xml
<dui:Button ImageSource="{dui:Icons bell_line}"
            ImagePlacement="Right"
            Text="Button"
            Command="{Binding Something}" />
```

# Properties
Inspect the [components properties class](https://github.com/DIPSAS/DIPS.Mobile.UI/blob/main/src/library/DIPS.Mobile.UI/Components/Buttons/Button.Properties.cs) to further customize and use it.