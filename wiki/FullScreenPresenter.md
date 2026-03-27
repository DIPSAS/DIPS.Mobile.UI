# FullScreenPresenter

The `FullScreenPresenterService` presents any view in full screen with smooth animations. It is designed to give full attention to a piece of content — such as a document, image, or any view — by displaying it centered on a dark background with a close button.

The original page is **never modified**. Instead, a native bitmap snapshot of the entire page is overlaid on the window. That page copy then fades away while a separate snapshot of the target view animates from its on-screen position to the center of the screen — creating the illusion that everything around the view disappears.

## Usage

Call `FullScreenPresenterService.Present(view)` with any MAUI view. A snapshot of the view is captured and animated to full screen. The original view stays in the page untouched.

```csharp
using DIPS.Mobile.UI.Components.FullScreenPresenter;

// Present a view in full screen
await FullScreenPresenterService.Present(myDocumentView);

// Close programmatically (also closeable via the built-in close button)
await FullScreenPresenterService.Close();

// Check if currently presenting
if (FullScreenPresenterService.IsPresenting)
{
    // ...
}
```

### Example: Toolbar button opening full screen

**XAML:**
```xml
<dui:ToolbarButton x:Name="fullScreenButton"
                   Icon="{dui:Icons expand_line}"
                   Title="Fullscreen" />

<!-- The view to present -->
<dui:VerticalStackLayout x:Name="documentSection">
    <!-- document content -->
</dui:VerticalStackLayout>
```

**Code-behind:**
```csharp
fullScreenButton.Command = new Command(async () =>
    await FullScreenPresenterService.Present(documentSection));
```

## Behavior

- A native bitmap snapshot of the **entire page** is placed as an overlay (the real page is untouched underneath)
- A separate snapshot of the **target view** is placed on top at its exact position
- The page snapshot fades out, revealing the design system background color behind it
- Simultaneously, the target view snapshot animates from its position to center screen
- A circular close button (X) appears in the top-right corner, respecting platform safe areas
- Tapping the close button reverses the animation — the view moves back and the page fades back in
- The close button is accessible with a localized "Close" semantic description

### Platform details

| Platform | Overlay | Snapshot | Animation | Close icon |
|---|---|---|---|---|
| iOS | `UIView` added to key `UIWindow` | `DrawViewHierarchy` → `UIImage` (page + view) | `UIView.Animate` (0.4s ease-in-out) | SF Symbol `xmark` |
| Android | `FrameLayout` added to `DecorView` | `Bitmap.CreateBitmap` + `Canvas.Draw` (page + view) | `ObjectAnimator` / `AnimatorSet` (400ms decelerate) | `close_line` drawable |

## API

| Member | Description |
|---|---|
| `Present(View content)` | Takes a snapshot of the entire page and the target view, then animates the surrounding content away while centering the view. The original page is not modified. |
| `Close()` | Closes the full screen presentation, animating the snapshot back to the original position. |
| `IsPresenting` | Returns `true` if a view is currently being presented. |
