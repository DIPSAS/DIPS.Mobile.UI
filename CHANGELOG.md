## [16.9.0] 
- Resources was updated from DIPS.Mobile.DesignTokens

## [16.8.2]
- [ContentControl][iOS] If content is CollectionView we wrap it in a grid with row=auto.
- [ListItem] Options now inherit the BindingContext of the ListItem.

## [16.8.1]
- [Chip] Made sure background color of chip is set correctly.

## [16.8.0]
- [Checkmark] Added
- [RadioButton] Added
- [Switch] Added
- [SearchBar] Change the default BarColor to Transparent
- [MultiItemsPicker] Changed the style of the items to select from.
- [ItemPicker] Changed the style of the items to select from.
- [ComponentsApp] Removed CheckBoxes samples and added Selection samples.

## [16.7.0] 
- Resources was updated from DIPS.Mobile.DesignTokens

## [16.6.0]
- Added animations API and added SkiaSharp Lottie nuget dependency.

## [16.5.2]
- [Android] Fixed a bug where setting IsEllipzised property were not set correctly when in a CollectionView

## [16.5.1]
- Fixed a bug where setting FormattedText on dui:Label would crash the application.

## [16.5.0]
- [Android] Fixed a bug where content in LoadableListItem were invisible.
- Fixed a bug where if InLineContent were not set in NavigationListItem, the navigation icon were not visible.
- Added a new property to dui:Label to check if the text were ellipsized.
- Fixed a bug where setting LineBreakMode on Label would always truncate to one line.

## [16.4.0] 
- Resources was updated from DIPS.Mobile.DesignTokens

## [16.3.1]
- [ComponentsApp] Fixed an issue where the wrong data was displayed when picking items from the multi item picker.
- [MultiItemsPicker] Will now use ToString if ItemDisplayProperty is not set.

## [16.3.0]
- [SegmentedControl] Added segmented control.
- [Label] Changed default text color to neutral_90.
- [CollectionView] Added ShouldBounce property.
- [ScrollView] Added ScrollView as a component, with ShouldBounce property.
- [SearchPage] The underlying SearchBar is now public. This can be used to clear the search bar programatically or other things.

## [16.2.2]
- [Android][CollectionView] Fixed a bug where if you set ItemSpacing on a CollectionView, the CollectionView tries to snap to the elements.

## [16.2.1]
- [ItemPicker] Made sure to use `Equals` instead of memory reference to check if an item was selected the first time `ItemPicker` opens.
- [HorizontalInlineDatePicker] Fixed an issue where the date picker when people press the current date did not appear on iOS.

## [16.2.0]
- ItemPicker and MultiItemsPicker can now be opened from a `Open()` method or by using `OpenCommand` property.

## [16.1.0]
- Removed obsolete properties from ListItem

## [16.0.1]
- Made sure in line label for date and time picker has the correct color and that the size is not bigger than what it should.
- Made sure Delay and ShouldDelay are added to SearchBar as well as SearchPage.
- Made sure SearchPage does not crash when you have not set a HintView or NoResultView

## [16.0.0]
- [BreakingChange] Refactored ListItem and its API, along with its extensions; LoadableListItem and NavigationListItem. Wiki page with the changes can be found [here](https://github.com/DIPSAS/DIPS.Mobile.UI/wiki/ListItem).

## [15.0.0]
- [MultiItemPicker] Added new component to pick multiple items.
- [Chip] Changed default colors and added property to change it.
- [Chip] Added property to set corner radius.
- [DatePicker] Made sure the iOS inline date picker follow the same default color as Chip. This however is not the case for [TimePicker] as of yet.
- [ItemPicker] Renamed BottomSheetConfiguration to BottomSheetPickerConfiguration


## [14.2.0] 
- Resources was updated from DIPS.Mobile.DesignTokens

## [14.1.0] 
- Resources was updated from DIPS.Mobile.DesignTokens

## [14.0.3]
- [ListItem] made sure subtitle changes are reflected to title label.

## [14.0.2]
- Fixed bug where content would not expand when setting static content item on LoadableListItem.

## [14.0.1]
- [ItemPicker] Made sure the search bar is default visible when in bottom sheet mode.

## [14.0.0]
- [ItemPicker] Added the posibility of adding a custom selectable item template.
- [ItemPicker][Breaking Change] Should have search bar is now moved to `BottomSheetConfiguration`.

## [13.17.0]
- [Colors] Added the posibility of setting an colors `Alpha` if needed.
- [Button] Made sure the shadows are properly removed no matter what color.
- [Button] Made sure paddings are correct.

## [13.16.2]
- [CollectionView][iOS] Fixed an issue where collectionview had the wrong width when in a scrollview.
- [RefreshView] Removed workaround code for refreshview when the new collection view hotfix was added to remove complexity.
- [CollectionView] Fixed an issue where Itemspacing was inherited between collection views between pages.
- [ListItem] Added `VerticalContentItemRowHeight`.

## [13.16.1]
- Fixed an issue where dividers was using extra space on iOS.

## [13.16.0]
- [ListItem] Added the ability to set the icons visibility.
- [ListItem] Changed inheritance from ContentView to Border for consumers to modify the shape and colors of the list item.
- [ComponentsApp] Cleaned up List Items page.

## [13.15.0]
- Rollback changes to 13.14.0 to add FloationgNavButton to RemoveViewsLocatedOnTopOfPage

## [13.14.0]
- Added `FloatingNavigationButtonService.Remove()` to `RemoveViewsLocatedOnTopOfPage`

## [13.13.0]
- Changed default values of column width in ListItem for Title and HorizontalContentItem
- Consumers can now override column width for Title and HorizontalContentItem in ListItem

## [13.12.0] 
- Resources was updated from DIPS.Mobile.DesignTokens

## [13.11.0]
- LoadableListItem can now have a static item, that is not affected when the LoadableListItem is set to busy or error.
- Added properties: TitleTextColor and TitleFontSize on ListItem

## [13.10.6]
- Moved Icon property from NavigationListItem to ListItem

## [13.10.5]
- Changed color of Divider

## [13.10.4]
- Fixed a bug where items inside ListItem were not clickable
- Hooked IsEnabled property on ListItem to enable/disable touch

## [13.10.3]
- Fixed a bug where you could not set Command on LoadableListItem 

## [13.10.2]
- Extended LoadableListItem with BusyText and ErrorText, also the possibility to fade content in 

## [13.10.1]
- Fixed bug where ItemPicker's ItemsSource would not update itself

## [13.10.0]
- [Android] Fixed visual bug with dui:Touch.IsEnabled
- Added LoadableListItem

## [13.9.0]
- Extended ListItem to be able to contain a vertical content item

## [13.8.0]
- Added SaveView
- Added FilledCheckBox
- Added ContentSavePage
- ListItem can now take in a Command and a CommandParameter

## [13.7.0]
- Extended FloatingNavigationButton so that consumers can programatically close it and also remove it from layout.
- [Android] Changed API calls to get FragmentManager so Android < 31 can also execute the code.
- Changed animation of FloatingNavigationButton.

## [13.6.1]
- [Android] dui:ImageButton now fixes padding issue: https://github.com/dotnet/maui/pull/14905

## [13.6.0]
- Added ShouldDelay property to SearchPage for enabling a small delay before search is invoked
- Added Delay property for defining delay in milliseconds (default = 500)
- Added SearchMode property for disabling automatic search triggering on search text changed

## [13.5.0]
- Add IsEnabled property for Touch.

## [13.4.0]
- Added IndicatorView.
- Made sure RefreshView is wrapping the content in a Grid to fix : https://github.com/dotnet/maui/issues/7315 on dotnet 7.

## [13.3.0]
- [ListItem] Added font attributes for title and subtitle.
- [BottomSheet] Made sure Android and iOS bottom sheets are always closed from RemoveViewsLocatedOnTopOfPage().
- [BottomSheet] Made sure Android bottom sheet service returns a task that waits until the fragment is dismissed for consumers to rely on the task from closing methods.
- [CollectionView] Made sure we add additional space at the end of the list (footer) so the last item is always visible. 1/3 of the visible list will be added as a invisible footer so people can easily see and tap the last item.

## [13.2.0]
- Added ActivityIndicator and RefreshView to reuse same colors and size for indicator.
- [ComponentsApp] Made sure that only positive sizes are displayed in the samples, as negatives or 0 makes sense.

## [13.1.0] 
- Resources was updated from DIPS.Mobile.DesignTokens

## [13.0.0]
- [BreakingChange] List item corner radius defaults to 0.

## [12.11.0]
- Extended dui:Button and dui:ImageButton so consumers can set additional hitbox size.
- Added ripple effect to dui:ImageButton.

## [12.10.0]
- Added DateTimeFormatter

## [12.9.3]
- Made sure modal effect on Android to add navbar does not use a vertical stack layout because it will break collection view scrolling on Android.

## [12.9.2]
- Icons on a context menu item is now of type icon image source.

## [12.9.1]
- [iOS] Fixed an issue where Floating Navigation Button did not work

## [12.9.0]
- Extended NavigationListItem with the possibility to add an icon to it
- [Android] Fixed an issue where setting padding directly on a (modal)contentpage would also pad the toolbar

## [12.8.0]
- [iOS] Fixed an issue where touch effect would still be imminent when a user taps and then slides the finger out of bounds
- [Android] Fixed an issue where touch effect would go out of bounds on some elements

## [12.7.0]
- [Android] Added a workaround to enable toolbar for modal pages

## [12.6.0]
- Rewrote how icons, sizes and colors are stored

## [12.5.1]
- [iOS] Fixed an issue when displaying two dialogs concurrently

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
