## [12.6.0]
- Rewrote how icons, sizes and colors are stored

## [12.5.0]
- Added dialogs

## [12.4.0]
- Added SystemMessage

## [12.3.3]
- Fixed an issue where touch effect were not displayed on some devices
- Changed touch behaviour
- Fixed an issue with ios Inline date pickers width being too small.
- Made sure that Android date picker respects the day of min and max dates.

## [12.3.0] 
- Resources was updated from DIPS.Mobile.DesignTokens

## [12.2.2]
- Made sure nuget package uses releaes configuration

## [12.2.1]
- Made sure the Touch effect on iOS does not UI freeze when people touch view elements its attached to.
- Fixed an issue on iOS with Touch effect when people tap and pan, like in a scrollable view.

## [12.2.0] 
- Fixed an issue where Title and Subtitle of ListItem would not wrap and continue horizontally, overwriting content.
- Added possibility to set TopDivider and BottomDivider on ListItem
- Added dui:Image to set color of an image

## [12.1.0] 
- Added option to add custom view to navigationlistitem

## [12.0.0]
- [BreakingChange] Changed colors of ContentPage, BottomSheet, Context Menu Android and Chip (Pickers).
- [DatePicker] Made sure android only sets date when people tap the ok (positive) button.
- [TimePicker] Made sure android only sets time when people tap the ok (positive) button.
- [TimePicker] Made sure android timepicker uses 12/24H format based on the users locale.
- [HorizontaInlineDatePicker] Made sure it doesnt crash when people select a date from the date picker service.
- [HorizontaInlineDatePicker] Made sure people can not set a date outside of the upper and lower ranges of the horizontal inline date picker.
- [DatePickerService] Made sure people have to manually close the date picker to set the date.
- [BottomSheet] Made sure iOS closed events is called when bottom sheet is programatically closed.
- [BottomSheet] Made sure toolbar items inherits the consumers bottom sheet binding context.
- [ContextMenu] Changed the style of context menu on Android. It now has more rounded corners, more elevation and the correct surface color as other material components.
- [ComponentsApp] Added sizes as font sizes and as boxes to size samples.
- [ImageButton] Made sure a default TintColor is set to make sure it doesnt crash when its not set.
 
## [11.4.0] 
- Resources was updated from DIPS.Mobile.DesignTokens

## [11.3.0]
- Added Divider
- [Android] Removed margin around material buttons
- [Android] Fixed an issue where ListItem had margin

## [11.2.1]
- Made sure that HorizontalInlineDatePicker do not crash when you change date.
- [ComponentsApp] Made sure the correct version of gets set to the iOS app. 

## [11.2.0] 
- Resources was updated from DIPS.Mobile.DesignTokens

## [11.1.0]
- Extended our Touch(effect) to include "LongPressCommand", users can now long press on any element
- Added AsyncCommand

## [11.0.0]
- [BreakingChange] Changed the animated flag for closing bottom sheets from bottom sheet service from default:false to true.
- Fixed an issue where HorizontalInlineDatePicker did not set a colors for the date that it started with.
- Fixed an issue where item picker bottom sheet mode did not close the bottom sheet.
- Fixed an issue where setting animated flag when closing bottom sheet did not do anything for Android.
- [ComponentsApp] Added version number to the first pages title.

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
