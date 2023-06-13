## [10.1.0]
- Extended our Touch(effect) to include "LongPressCommand", users can now long press on any element
- Added AsyncCommand

## [10.0.0]
- [BreakingChange] Changed API of BottomSheet regarding events when closing/opening
- Added the ability to set title and toolbar items to BottomSheet
- [iOS] Fixed an issue where OnClosed were not called when BottomSheet is closed

## [9.0.0]
- Added SlideableLayout
- Added SlideableContentLayout
- Added HorizontalInlineDatePicker
- Changed namespace of builder to correspond with internal project structure. Use DIPS.Mobile.UI.API.Builder now.

## [8.1.0]
- [Android] Fixed an issue where CollectionView's height inside BottomSheet would expand beyond its items
- [Android] Fixed an issue where the Handle and Searchbar were scrollable inside BottomSheet
- [iOS] Fixed an issue where CollectionView's height inside BottomSheet would be cut short
- Consumers has now the option to include a searchbar in BottomSheet's 

## [8.0.0]
- Added FloatingNavigationButton 
- Added new effect: ImageTint
- Renamed existing effect: DUITouchEffect => Touch
- Added test project: Playground

## [7.1.0] 
- Resources was updated from DIPS.Mobile.DesignTokens

## [7.0.0]
- ListItem: Renamed SubTitle to Subtitle
- SkeletonView: Added SkeletonView as a new component. This is placed in the Loading namespace.
- Components app: New Loading page with Skeleton loading.
- Markup extensions: Added Margin and Padding markup extensions.

## [6.4.0] 
- Resources was updated from DIPS.Mobile.DesignTokens

## [6.3.0] 
- Resources was updated from DIPS.Mobile.DesignTokens

## [6.2.0]
- Content Control: Added a new ContentControl for consumers to easily switch between two different views in a page based on a item and a template selector.
- Components app: Cleaned up search bar pages background.

## [6.1.0]
- Colors: Added Deprecated Colors API with mapping to the new Colors to use.

## [6.0.0]
- Colors: Added new colors from the design system
- Sizes: Changed namespace of Sizes, breaking change.

## [5.1.1]
- Context Menu: Fixed a bug where context menus sometimes does not show on iOS.
- Context Menu: Fixed issue where checking items did not work as expected for both platforms.

## [5.1.0]
- Added new colors from Design System.
- Components app: Added better download message.
- Components app: Added search for resources.

## [5.0.0]
- Added build script for delivering Components app to AppCenter
- Added in app API for checking version of AppCenter
- Breaking change: Changed the ItemPicker base class to remove unessesary properties.
- Breaking change: Changed ItemPickers Title property to Placeholder
- Added ItemSpacing for CollectionView
- Added a default ItemSpacing for CollectionView
- Added VerticalStackLayout
- Added default Spacing for VerticalStackLayout
- Added StringFormat markup extension to easily use a static string format with an hard argument in XAML.
- Cleaned up components app
- Made sure Components app samples are alphabetical
- Added Splash screen, this can be disabled by setting <DIPSIncludeSplashScreen>False</DIPSIncludeSplashScreen> in consumers csproj.

## [4.0.0]
- Added TimePicker
- Added DateAndTimePicker
- Can now set Minimum and Maximum date for DatePickers
- Added Chip
- Changed style of Pickers
- Added ListItems
- Added TouchEffect

## [3.0.1]
- Minor changes to the search bar in order for it to work more smoothly.

## [3.0.0]
- Breaking change: Renamed dui:Image to dui:NativeIcon and simplified the ios and android properties for setting the icon.
- Added <MauiImage> for our icons. This can be disabled by setting DIPSIncludeIcons = False in your .csproj.

## [2.1.1]
- SearchBar on Android is now showing if you have set HasCancelButton = false.

## [2.1.0]
- Added DatePicker
- Added ItemPicker
- Added SearchPage
- Added SearchBar
- Added CheckBoxes
- Fixed bottom sheet height issues / scroll issues when using collection view on iOS

## [2.0.1]
- Made bottom sheet content is centered and reset the background color.

## [2.0.0]
- Added first version
