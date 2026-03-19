`DelayedView` is a container that delays the display of its content for a specified duration. This is useful for preventing flickering when loading content that might appear quickly, or for creating intentional delays in UI updates.

# Properties

- **`Delay`** (`TimeSpan`) - The duration to wait before showing the content. Default is 500 milliseconds.
- **`Content`** (`View`) - The view to display after the delay period has elapsed.

# Usage

Here we create a `DelayedView` that waits 500ms (default) before showing its content:

```xml
<dui:DelayedView>
    <Label Text="This content appears after a delay" />
</dui:DelayedView>
```

## Custom Delay Duration

You can specify a custom delay duration:

```xml
<dui:DelayedView Delay="00:00:01">
    <Label Text="This content appears after 1 second" />
</dui:DelayedView>
```

## Use Cases

The `DelayedView` is particularly useful in scenarios such as:

- **Preventing loading flicker**: When data loads quickly, showing a loading indicator briefly can cause jarring flicker. DelayedView can ensure the loading indicator only appears if loading takes longer than expected.
- **Smooth transitions**: Delay the appearance of success messages or notifications to create a more polished user experience.
- **Progressive disclosure**: Reveal UI elements gradually to guide user attention.

```xml
<dui:DelayedView Delay="00:00:00.3">
    <dui:ActivityIndicator IsRunning="True" />
</dui:DelayedView>
```
