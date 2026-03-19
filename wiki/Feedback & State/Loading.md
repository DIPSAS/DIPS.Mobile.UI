When people have to wait for your application to load data, the application should indicate that it is busy. DIPS delivers ways for you to display loading indication in your application.


# Skeleton
A skeleton is an animated placeholder that occupies space in the page that is loading while the data is loading. Once the content is finished loading, the animated placeholder will disappear and your content will appear. The animated placeholder will simulate the layout while it's loading the layout. This gives the user the impression that the application is faster because they already know what type of content is loading before it appears. This is referred to as perceived performance.

## Usage

```xml
<dui:SkeletonView IsLoading="{Binding IsBusy}">
    <dui:SkeletonView.Shapes>
        <dui:SkeletonShape  />
    </dui:SkeletonView.Shapes>
    <dui:ListItem Title="Name"
                  Subtitle="This is the item that will load">
        <dui:Label Text="Amund Amundsen" />
    </dui:ListItem>
</dui:SkeletonView>
```

Once the `IsBusy` property is set to `true`, the skeleton view will replace the `ListItem` with an animated placeholder. The animated placeholder will use the entire space that the `ListItem` has. You can add multiple `SkeletonShapes`, and the `SkeletonShape` can be customised. The idea with it is that you can tell it how it should look, because it does not know how the layout looks. The `SkeletonShape` lives inside a `Grid`, this means that you can have multiple shapes placed in a column / row order for it to look the best while its visible.

## Properties
Inspect the [SkeletonView properties class](https://github.com/DIPSAS/DIPS.Mobile.UI/blob/main/src/library/DIPS.Mobile.UI/Components/Loading/Skeleton/SkeletonView.Properties.cs) and [SkeletonShape properties class](https://github.com/DIPSAS/DIPS.Mobile.UI/blob/main/src/library/DIPS.Mobile.UI/Components/Loading/Skeleton/SkeletonShape.cs) to further customise and use it.


# LoadingOverlay
A `LoadingOverlay` is a component that displays a semi-transparent overlay with a loading indicator on top of existing content. It is useful when you want to indicate that a specific part of your application is busy loading data, without blocking the entire UI. The overlay prevents user interaction with the underlying content until the loading is complete, providing a clear visual cue that an operation is in progress.

### Properties

The `LoadingOverlay` component exposes several properties to customize its appearance and behavior:

- `Text`: Sets the loading message displayed in the overlay. For example, `Text="Loading documents..."` will show a custom loading message.
- `ContentColor`: Defines the color of the loading indicator and text. For example, `ContentColor="Black"` sets the content color to black.
- `OverlayColor`: Sets the background color of the overlay. For example, `OverlayColor="White"` makes the overlay background white.
- `ContentFadeOutValue`: Controls the opacity of the underlying content when the overlay is active. A value of `0` will fully fade out the content, while higher values make it more visible.

These properties allow you to tailor the `LoadingOverlay` to fit your application's design and user experience requirements.

## Usage

```xml
<dui:LoadingOverlay IsBusy="{Binding IsBusy}">
    <dui:Label Text="This content will be overlaid while loading." />
</dui:LoadingOverlay>
```

When the `IsBusy` property is set to `true`, the `LoadingOverlay` will appear above the content, showing a loading spinner and blocking interaction. Once loading is finished and `IsBusy` is set to `false`, the overlay disappears and the user can interact with the content again.

## Properties
You can customize the appearance and behavior of the `LoadingOverlay` by inspecting the [LoadingOverlay properties class](https://github.com/DIPSAS/DIPS.Mobile.UI/blob/main/src/library/DIPS.Mobile.UI/Components/Loading/LoadingOverlay/LoadingOverlay.Properties.cs).


# DelayedView
A `DelayedView` is a component that delays rendering its content for a set amount of time, showing an activity indicator in the meantime. This is particularly useful to workaround UI freezes when navigating to heavy pages or loading complex layouts. By deferring the rendering of expensive content, the UI remains responsive and provides immediate feedback to the user.

The component displays a centered activity indicator while waiting, then replaces it with your actual content once the delay has elapsed. This improves the perceived performance of your application by keeping the UI thread responsive during navigation and initial page setup.

## Usage

```xml
<dui:DelayedView SecondsUntilRender="0.6">
    <dui:DelayedView.ItemTemplate>
        <DataTemplate>
            <VerticalStackLayout>
                <dui:Label Text="Heavy Content" />
                <CollectionView ItemsSource="{Binding LargeDataSet}">
                    <!-- Complex item template -->
                </CollectionView>
            </VerticalStackLayout>
        </DataTemplate>
    </dui:DelayedView.ItemTemplate>
</dui:DelayedView>
```

The content defined in the `ItemTemplate` will be rendered after the specified delay (default is 0.6 seconds). During the delay, an `ActivityIndicator` is shown to provide visual feedback that content is loading.

### Recommended Delay Values

Based on testing, the following delay values provide the best user experience:

- **iOS - Normal page navigation**: 0.1 seconds
- **Android - Normal page navigation**: 0.5 seconds (minimum)
- **iOS & Android - Modal navigation**: 0.6 seconds

These values help balance between keeping the UI responsive and providing enough time for the page to initialize without freezing.

## Properties

- `ItemTemplate` (DataTemplate): The template used to render the view after the delay. This is a required property.
- `SecondsUntilRender` (float): How many seconds to wait until rendering the view. Default value is 0.6 seconds. See recommended values above for different scenarios.
- `OnRendered` (EventHandler): An event that is invoked when the view has been rendered, allowing you to perform additional actions once the content is visible.

You can further customize and explore the component by inspecting the [DelayedView properties class](https://github.com/DIPSAS/DIPS.Mobile.UI/blob/main/src/library/DIPS.Mobile.UI/Components/Loading/DelayedView/DelayedView.Properties.cs).