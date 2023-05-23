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
