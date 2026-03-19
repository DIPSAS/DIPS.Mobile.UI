The `AutoScrollingTextView` is a UI component designed to enhance the user experience when displaying dynamic text content. It ensures that the text always resides at the vertical end and automatically scrolls when new text is appended to the bottom. 

## Key Features
- **Automatic Scrolling**: Automatically scrolls to the bottom when new content is added.
- **Fade Effect**: Displays a fade effect at the top to indicate overflow content.
- **Scroll-to-Bottom Button**: A button appears when the view is not at the bottom, allowing users to quickly scroll to the latest content.

This component is ideal for use cases such as chat applications, logs, or any scenario where real-time updates to text content are required.

## Usage

Here is an example of how to use the `AutoScrollingTextView`. You can also customize properties such as `textColor` and `textStyle`:

```xml
<dui:AutoScrollingTextView
    Text="{Binding TranscriptionText}" />
```

### Customization Options
- **`TextColor`**: Set the color of the text using a color resource or hex value.
- **`Style`**: Define the style of the text.
- **`ShouldFadeOut`**: Whether the text should fade out at the top.
- **`FadeColor`**: Defines the fade color at the top.