We offer an API to make any `VisualElement` tappable, which includes an animation when it is tapped. The animation behaves differently on iOS and Android:
- iOS: A fade out animation is imminent when the element is tapped or held in, and it fades back in when the element is no longer tapped.
- Android: A ripple effect is displayed on the `VisualElement` when its tapped or held.

There are two Commands:
- `Command`: Executed when the `VisualElement` is tapped
- `LongPressCommand`: Executed when the `VisualElement` is _long pressed_

>Both commands can be set on one `VisualElement`

# Usage
The following example makes the `Border` tappable, the `TestCommand` will be fired upon tapping.
```xml
<Border dui:Touch.Command="{Binding TestCommand}" />
```

## Accessibility
⚠️ **Important:** When using `Touch.Command`, you should also set `SemanticProperties.Description` for proper accessibility support. See [Accessibility - Touch Effect](Accessibility#touch-effect-accessibility) for details.

# Properties
Inspect the [Touch Properties Class](https://github.com/DIPSAS/DIPS.Mobile.UI/blob/main/src/library/DIPS.Mobile.UI/Effects/Touch/Touch.Properties.cs) to further customize and use it.