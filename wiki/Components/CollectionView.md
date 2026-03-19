# CollectionView

The `CollectionView` is a flexible and performant view for presenting lists of data using different layouts in .NET MAUI. It supports features such as horizontal and vertical scrolling, grid and list layouts, and data virtualization. `CollectionView` is designed to provide a more powerful and customizable alternative to the `ListView`.

Key features of `CollectionView` include:
- Multiple layout options (list and grid)
- Horizontal and vertical scrolling
- Data virtualization for improved performance
- Support for empty views
- Grouping and headers

`CollectionView` is ideal for scenarios where you need to display a large set of data with varying layouts and improved performance.

## Additional space at the end
Additional space at the end of a list can improve the user experience by preventing the last item from being too close to the edge of the screen. This makes it easier for users to focus on the content.

In our CollectionView this setting is enabled by default. To turn off this behaviour simply set the `HasAdditionalSpaceAtTheEnd` to false:

```xml
<dui:CollectionView HasAdditionalSpaceAtTheEnd="False" />
```
## Losing focus on Input fields if the list is scrolled

We have implemented a property to ensure that input views lose focus when the list is scrolled. By losing focus on scroll, it prevents the keyboard hiding the CollectionView's items.

To enable this feature, set the `RemoveFocusOnScroll` property to true:

```xml
<dui:CollectionView RemoveFocusOnScroll="True" />
```

## Corner Radius on first and last element
In many designs, lists often have a corner radius applied to the first and last elements. To reduce boilerplate code and ensure that only the first and last elements have a corner radius, this feature is built directly into `dui:CollectionView` and is enabled by default. For instructions on how to disable this feature, refer to the [Corner Radius](https://github.com/DIPSAS/DIPS.Mobile.UI/wiki/CornerRadius#collectionview) documentation.

> **NB: Will only work if the list is vertical.**

## Hiding the last divider
In certain designs, hiding the last divider in a list can create a cleaner appearance. To simplify this, we have introduced a global property. For more details, visit [Divider - Tips and tricks](https://github.com/DIPSAS/DIPS.Mobile.UI/wiki/Divider#tips-and-tricks).

```xaml
<dui:CollectionView dui:Layout.AutoHideLastDivider="True" />
```

> **NB: Will only work if the list is vertical.**

## Padding
We have added padding property to our `CollectionView`, which has a default Horizontal padding. 

**NB: Will only work if the list is vertical.**

> Top padding is not implemented yet


## New handler on iOS
.NET MAUI have started developing a `CollectionView` that uses a new handler on iOS. To make use of this, you can use `CollectionView2`. This should be used if you encounter buggy UI.

> There is no change on Android if you decide to use this component.

## Collapsible Element
We have introduced a property that allows you to bind an element that should collapse when the `CollectionView` is scrolled. This feature provides customization options for the collapse behavior, including:

- **Collapse Trigger**: Specify when the element should start collapsing.
- **Fade Behavior**: Define if and when the element should start fading and when it should be completely faded out.
- **Input Transparency**: Control the input transparency of the element during the collapse process.

To use this feature, bind the element to the `CollapsibleElement` property and configure the desired behavior:

```xml
<dui:CollectionView.CollapsableElement>
    <dui:CollapsableElement Element="{Binding Source={x:Reference ContainerGrid}, x:DataType={x:Type Grid}}"
                                                InputTransparentOnCollapse="True" 
                                                OffsetThreshold="100"
                                                ShouldFadeOut="True"
                                                FadeOutThreshold="0.5"/>
</dui:CollectionView.CollapsableElement>
```

# Properties
Inspect the [components properties class](https://github.com/DIPSAS/DIPS.Mobile.UI/blob/main/src/library/DIPS.Mobile.UI/Components/Lists/CollectionView/CollectionView.Properties.cs) to further customize and use it.