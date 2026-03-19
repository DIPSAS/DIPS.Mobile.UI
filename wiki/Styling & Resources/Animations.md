# Animation API
We have created an attached bindable property that you can use on any `VisualElement`.

## Supported Animations
- **FadeIn**: This animation gradually increases the opacity of a `VisualElement` from 0 to 1, creating a smooth fade-in effect. It is triggered when the element appears on the screen or when its `IsVisible` property changes from `False` to `True`.

## Usage
To set an animation to a `VisualElement`, set the property to your `VisualElement` in XAML or C#.

### XAML Example:
```xml
<Label 
    Text="Hello, World!">

    <dui:Animation.FadeIn>
        <AnimationConfig />
    </dui:Animation.FadeIn>

</Label>
```

### C# Example:
```csharp
var label = new Label { Text = "Hello, World!" };
Animation.SetFadeIn(label, new AnimationConfig());
```

> If no properties are changed on `AnimationConfig`, default values will be used that is set by .NET MAUI.

#### AnimationConfig
`AnimationConfig` allows you to customize your animation by setting the following properties:

- **Duration**: Specifies the duration of the animation in milliseconds. Default is `250ms`.
- **Easing**: Defines the easing function to control the animation's acceleration and deceleration. Default is `Easing.CubicInOut`.

### Example:
#### XAML:
```xml
<Label 
    Text="Hello, World!">

    <dui:Animation.FadeIn>
        <AnimationConfig Duration="500" 
                         Easing="Easing.Linear" />
    </dui:Animation.FadeIn>

</Label>
```

#### C#:
```csharp
var label = new Label { Text = "Hello, World!" };
var config = new AnimationConfig
{
    Duration = 500,
    Easing = Easing.CubicInOut
};
Animation.SetFadeIn(label, config);
```

# SKLottieView animations
DIPS delivers a set of animated images that you can use in your app. [The animations are located in the mobile design tokens repository.](https://github.com/DIPSAS/DIPS.Mobile.DesignTokens/tree/main/src/tokens/animations)

## Usage
The animations are available for you to use with [SKLottieView](https://mono.github.io/SkiaSharp.Extended/api/ui-maui/sklottieview) from [SkiaSharp.Extended.UI.Maui](https://mono.github.io/SkiaSharp.Extended/api/ui-maui/):

XAML Shared:
```xml
<skia:SKLottieView 
    Source="{dui:Animations saved}" 
    HeightRequest="{dui:Sizes size_25}" 
    WidthRequest="{dui:Sizes size_25}"
/>               
```

## Tips and tricks
### Detect animation completed
To know when an animation has completed, subscribe to `SKLottieView.PropertyChanged`. Inspect the `PropertyChangedEventArgs` to know that `IsCompleted` property changed. If it changes, it will be completed when `IsCompleted` is `true`. 

## Turn it off
If you do not use our animations and are worried your app including animations that are not needed, you can turn it off by setting the following property in your `.csproj`:

```xml
<PropertyGroup>
    <DIPSIncludeAnimations>False</DIPSIncludeAnimations>
</PropertyGroup>
```
