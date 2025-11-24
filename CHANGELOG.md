## [54.3.1]
- Make sure apps that have locked their light mode, can force dark mode.
- Renamed experimental feature `DarkMode` -> `ForceDarkMode`.

## [54.3.0]
- Added `DarkMode` experimental feature.

## [54.2.5]
- [Counter] Forbedret tilgjengelighet slik at skjermleser leser opp teller-verdiene, teller-beskrivelsen, samt om de haster, eller om det eventuelt er en error.

## [54.2.4]
- [Alert] Fixed accessibility

## [54.2.3]
- [Touch] Added IsButtonTraitEnabledProperty to allow consumer to disable button trait on Touch effect.
- [MultiLineInputField][SingleLineInputField] Enhanced accessability, screen reader now announces the value of the field and that it is an input field, and all buttons are available in navigation

## [54.2.2]
- [ItemPicker] Made screen recorder read button and the itempicker's selected item
- [Chip] Made screen recorder focus on close button if present

## [54.2.0]
- [Accessibility] Added `Trait` attached property with support for Button, Selected, and NotSelected traits for improved screen reader announcements on iOS and Android
- [SortControl] Fixed accessibility.
- [RadioButtonListItem] Now automatically sets appropriate traits based on selection state

## [54.1.0]
- [ListItem] Added `DisableInternalAccessibility` property to exclude internal elements (Title, Subtitle, Icon) from accessibility tree, allowing screen readers to focus directly on interactive content like switches or buttons

## [54.0.0]
- [Touch] Improved accessibility for Touch effect - screen readers will now announce "Button" when `SemanticProperties.Description` is set
- [Touch][BreakingChange] Removed deprecated `Touch.AccessibilityContentDescription` property - consumers should use `SemanticProperties.Description` instead
- [SegmentedControl][Android] Correct accessibility.

## [53.8.2]
- Made sure bottom sheet header is focused and not the drag handles for both platforms.

## [53.8.1]
- Set semantic description on segmented control items

## [53.8.0]
- [Accessibility] Added `Mode` attached property.

## [53.7.4]
- Fix error causing index out of range in `TabBadgeService` when tabs have changed 

## [53.7.3]
- The shutter button will now get semantic focus when its ready to take photos.
- The shutter button will now announce the purpose of the button.
- The blitz button will now announce the purpose of the button.

## [53.7.2]
- [Android] Fixed issue where screenrecorder was able to record content in modals

## [53.7.1]
- [Tip][iOS] Fixed an issue where the current `UIViewController` would sometimes close when tapping outside the tip.

## [53.7.0]
- Added `DelayedView`.

## [53.6.2]
- [iOS] Fixed so Touch.Command will not be executed when long press is recognized.

## [53.6.1]
- [AlertView][Android] Fixed bug where buttons would not be visible if text size was increased and the Alert was not large.
- [AlertView][iOS] Fixed bug where `AlertView` would not show the custom truncation text if its visibility were set to false at the start.

## [53.6.0]
- [ItemPicker] Added `AllowEmpty` and `EmptyItemTitle` properties to allow for clearing the picker's selected item

## [53.5.0]
- [Dictation] Added experimental dictation button with delegate on multiline input fields.

## [53.4.1]
- [ImageThumbnailView] Fixed bug where close button could not be tapped.

## [53.4.0] 
- [GC] Added `EnableAutomaticModalHandlerDisconnection` to `IDIPSUIOptions` to enable automatic disconnection of modal pages.
- [GC] Fixed bug where the tooling could not print out actual elements.

## [53.3.0] 
- Resources was updated from DIPS.Mobile.DesignTokens

## [53.2.1] 
- [DateView] Fix styling.
- [Counter] Set correct border color when `IsUrgent` is true, and no secondary value.

## [53.2.0] 
- Resources was updated from DIPS.Mobile.DesignTokens

## [53.1.1] 
- Fix missing icon.

## [53.1.0] 
- Resources was updated from DIPS.Mobile.DesignTokens

## [53.0.2]
- [CheckTruncatedLabel][Android] Fix potential crashes.

## [53.0.1]
- [BottomSheetService] Fix issue where using `CloseAll` method, would blank out `BottomSheet`.

## [53.0.0]
- [BreakingChange] Renamed some button styles.
- [Button] Fixed floating and close button disabled state.
- [Counter] Always center text, and now behaves better when font size is increased on the OS.
- [Tag] Increased padding.
- [ListItem] Fixed potential crash.
- [Layout][Android] Convert to correct stroke size.

## [52.0.2]
- [iOS] Automatically set compliance when in App Store Connect.

## [52.0.1]
- [Layout][Android] Fix bug where setting `Stroke` would not appear visually sometimes.

## [52.0.0]
- Imported and set new colors.
- Imported new icons.
- [Layout] Added `Stroke` and `StrokeThickness` bindable attached properties to easier set stroke on any `VisualElement`.

## [51.4.4]
- Pin SkiaSharp to version 3.119.1

## [51.4.3]
- Revert some changes.

## [51.4.2]
- Modify build script to publish to Google Play and TestFlight.

## [51.4.1]
- Add comment on LineBreakMode Property in TitleOptions
 
## [51.4.0]
- InvertedBoolConverter now supports nullable bools.

## [51.3.4]
- Fixed LoadingOverlay not functioning properly.

## [51.3.3]
- [Android] Update nuget packages to support 16kb page sizes.

## [51.3.2]
- [ScrollPicker][iOS] Fixed bug where `ScrollPicker` could not be used when residing in a modal.

## [51.3.1]
- [Chip][iOS] Increased hitbox on close button.

## [51.3.0]
- [Label] Removed handler because of obsolete workaround.
- [CheckTruncatedLabel] Removed redundant constructor.
- Moved some files.

## [51.2.1] 
- [Android][MultiItemsPicker] Fixed issue where ToString of the MultiItemsPicker would be visible if footer on the BottomSheet were not set.

## [51.2.0] 
- Resources was updated from DIPS.Mobile.DesignTokens

## [51.1.0]
- Added `CustomTruncationTextView`.
- Added `CheckTruncatedLabel`.
- [Android] Fix crash on startup.

## [51.0.0]
- Added `CustomTruncationLabel`.

## [50.0.0]
- [AlertView] Optimized layout.
- [AlertView][BreakingChange] Removed `ButtonAlignment`, `TitleMaxLines` and `DescriptionMaxLines` properties.
- [AlertView] The component now corresponds to figma.
- [AlertView] Added comprehensive accessibility support with proper screen reader announcements and navigation.
- [Label] Fixed bug where Spans would lose formatting from FormattedText when `TruncatedText` was set.
- [Label] Added `TruncatedTextStyle` property with new `SpanStyle` enum to apply span styles to custom truncation text.
- [Styles] Added `SpanStyle` enum and styling system for spans, with shared style definitions between `LabelStyle` and `SpanStyle` for easier maintenance.

## [49.9.7]
- Fix wrong margin on tag without icon

## [49.9.6]
- Update color resources
- Revert changes that broke navigationlistitem with icon

## [49.9.5]
- [SaveView] Removed touch effect when tapping the component

## [49.9.4]
- [SearchBar] Set correct text color.
- [SearchBar][iOS] Modify background color. 
- Revert previous commit where things were changed in `IconTintColorHandler`, breaking icon tinting.

## [49.9.3]
- [Checkmark][CheckmarkListItem][iOS] Fix bug where checkmark is never visible.

## [49.9.2] 
- [ChipGroup][iOS] Chips now won't be truncated when there is available space.

## [49.9.1] 
- [Button][iOS] Fix possible crash.
- [SearchBar][Android] Make sure busy indicator default color is same as searchbar background

## [49.9.0] 
- [SearchBar] Updated styling.

## [49.8.0] 
- [Android] Retapping tab will now pop to root, similar to iOS.

## [49.7.1] 
- [Button] Fixed bug where toggling IsVisible would reset icon's tint color.

## [49.6.0] 
- Resources was updated from DIPS.Mobile.DesignTokens

## [49.5.2] 
- [Gallery] Fix crash when removing image.
- Revert previous commit

## [49.5.1] 
 - [ContextMenu][iOS] Make sure nothing breaks when context menu item is tapped.

## [49.5.0] 
- Resources was updated from DIPS.Mobile.DesignTokens

## [49.4.0] 
- Resources was updated from DIPS.Mobile.DesignTokens

## [49.3.0]
- Fixed splash screen icon colors.

## [49.2.1]
- [PositionInListConverter] Converter now works properly when list elements are grouped.

## [49.2.0] 
- [Button] Changed `CloseButtonIcon` style colors.
- [BottomSheet] Changed color on `Divider` in header.

## [49.1.1] 
- [AlertView] Don't trigger animation from `AlertViewService` if `ShouldAnimate` is set to `false`.

## [49.1.0] 
- [ItemPicker] Added `IsReadOnly` property.
- [AlertView] Fixed bug where animating it, would break consumers' bindings on IsVisible.
- [AlertView] Fixed bug where title were not centered when description not set.

## [49.0.0] 
- Upgrade .NET MAUI to 9.0.100.
- Change style of `SectionHeader`.
- [Android][Tip] Fix wrong color.

## [48.6.0] 
- [ContextMenuEffect] Added `MenuBindingContext` property. 

## [48.5.0] 
- [LoadableListItem] Changed color when IsBusy.
- [LoadingOverlay] Changed default colors.

## [48.4.2] 
- [LoadingOverlay] Fixed bug where it displayed duplicate loading overlay.

## [48.4.1] 
- [AlertView] Fixed bug where triggering animation would make non-visible `AlertView` visible.

## [48.4.0] 
- Added `LoadingOverlay`.

## [48.3.0] 
- Added `AlertViewService` to easily trigger `AlertView` animation.

## [48.2.0] 
- [ItemPicker] Added `CustomTapCommand` property.

## [48.1.2] 
- [AlertView] Fixed icon tint colors not being set.
- [AlertView] Fixed icon size.
- [AlertView] Now using correct text style on title, if the description is not set.
- [AlertView] Can now be easily animated.
- [Animations] Added Scale animation.

## [48.1.1] 
- [Touch][iOS] Block touch when context menu is open.

## [48.1.0] 
- [ImageCapture] Fixed bug where if you edit an image, the edited image would not be displayed after saving.
- [CameraPreview] Made methods public to add views to top/bottom toolbar.

## [48.0.0] 
- Upgraded to material design 3.
- [SearchBar][BreakingChange] Renamed iOSSearchFieldBackgroundColor property to SearchFieldBackgroundColor.
- [SearchBar][Android] Changed styling.

## [47.15.2]
- [ItemPicker] Fixed arrow icon size for `ItemPicker` when `Size=Large`.

## [47.15.1]
- [AlertView][Android] Fixed crash on Android when not setting `TitleMaxLines` or `DescriptionMaxLines`.

## [47.15.0] 
- Fixed splash screen color.

## [47.14.0] 
- Resources was updated from DIPS.Mobile.DesignTokens

## [47.13.0]
- [AlertView] Added `TitleMaxlines` and `DescriptionMaxLines`.

## [47.12.0]
- [Tag] Added `LineBreakMode` property

## [47.11.0] 
- Resources was updated from DIPS.Mobile.DesignTokens

## [47.10.1]
- [TabView] Remove padding on sides on iOS

## [47.10.0]
- [TabView] Add possibility to check if switching tab is allowed.

## [47.9.0]
- [Counter] Add `IsError`, `IsSecondaryError` and `IsFlipped` properties

## [47.8.1] 
- [TabView] Small style fixes

## [47.8.0] 
- Resources was updated from DIPS.Mobile.DesignTokens

## [47.7.1] 
- [ItemPicker] BottomSheetPickerConfiguration's properties can now be bound.

## [47.7.0] 
- [ItemPicker] Added `Large` size, which will transform its layout.
- [ItemPicker] Added `AdditionalContextMenuItem` property.
- [Chip][Android] Set tint color to custom icon.
- [Shell][iOS] Remove divider in navigationbar on all shell pages.

## [47.6.0]
- Added TabView Component

## [47.5.1]
- [SegmentedControl][Android] Make sure all border always have same height.
- [SegmentedControl] Unselected items' text color is now text_default.

## [47.5.0]
- Added `Counter` component

## [47.4.0] 
- Resources was updated from DIPS.Mobile.DesignTokens

## [47.3.0] 
- [Shell][iOS] You can now control navigation bar separator by using `Shell.NavBarHasShadow`.

## [47.2.0] 
- [NavigationListItem] Removed default icon color and set chevron color.
- [TabBar] Set different background color.
- [Tag] Add `Information` style.

## [47.1.0] 
- Added `Tag` component.

## [47.0.0] 
- Removed all old colors.
- Set remaining colors.

## [46.0.0] 
- Set new colors.
- Updated colors.
- Obsoleted old colors.
- [iOS] Can now use Large Title. (However, it's buggy)
- [Android] Removed workaround to set status bar when pushing modal
- [Android] Removed workaround for Label
- Upgraded to .NET MAUI 9.0.80.

## [45.11.0] 
- Resources was updated from DIPS.Mobile.DesignTokens

## [45.10.0] 
- Resources was updated from DIPS.Mobile.DesignTokens

## [45.9.7]
- [MultiItemsPicker] Can now tap outside the `BottomSheet` to close.
- [BottomSheet] `SearchBar` has now keyboard `Done` button as default.

## [45.9.6]
- [BottomSheet][Android] Focusing any `SearchBar` in `BottomSheet` will now expand the `BottomSheet`.

## [45.9.5]
- [ImageThumbnailView] Fix position of close button on image

## [45.9.4]
- [ItemPickerBottomSheet] Prevent the bindingcontext from being displayed in the bottom sheet in Android

## [45.9.3]
- [ListItem][CollectionView][Divider][iOS] Revert all fixes regarding ListItem's divider. Now we only toggle Opacity value instead of IsVisible, to fix divider UI glitches.

## [45.9.2]
- [CollectionView][Image] Handle resetting image tint colors on iOS and Android to avoid tint artifacts caused by view recycling.

## [45.9.1]
- [MultiItemsPicker] Set BindingContext to BottomSheetPickerConfiguration inside MultiItemsPicker

## [45.9.0]
- [BottomSheet][Android] Focusing `SearchBar` will now expand the `BottomSheet`.

## [45.8.3]
- [ListItem][CollectionView][iOS] ListItem's divider in a CollectionView will now no longer have weird positioning.

## [45.8.2]
- Fix duplicates when filtering in ItemPicker BottomSheet

## [45.8.1]
- [ListItem][CollectionView][iOS] ListItem's divider in a CollectionView will now no longer expand its height.

## [45.8.0]
- Imported new colors.

## [45.7.4]
- [iOS] Fixed freeze when showing barcode result.

## [45.7.3]
- [Searchbar][Android] `AndroidBusyBackgroundColor` will now show if `IsBusy` is set to false.

## [45.7.2]
- [Android][AlertView] Fix bad scaling when text goes over multiple lines and no buttons are visible

## [45.7.1]
- [SingleLineInputField] Make sure to set InputView visible when text is set from consumer.

## [45.7.0]
- [MultiItemsPicker] Added property `HasDoneButton` for displaying a done button in the bottom sheet
- [BottomSheet] Added property `IsCloseButtonVisible` for controlling whether the close button should be visible in the header

## [45.6.1]
- [SearchBar][iOS] Keep cancel button enabled when losing focus on search bar.

## [45.6.0]
- [SearchBar][iOS] Setting cancel button visibility in runtime will now animate.

## [45.5.0]
- [MultiItemsPicker] Added `ResetBehaviour` property to `MultiItemsPicker` to enable a button for resetting selected items.

## [45.4.0]
- [ItemPicker][MultiItemsPicker] Added `FooterTemplate` property to `BottomSheetPickerConfiguration` to allow for setting a footer to item picker bottom sheet.

## [45.3.3] 
- [BottomSheet] Fixed memory leak.

## [45.3.2] 
- [SegmentedControl][Android] Fixed bug where setting `HorizontalOptions` had no effect.

## [45.3.1] 
- [iOS][CollapsibleElement] Fix bug when small list.

## [45.3.0] 
- Added `CollapsibleElement` to `dui:CollectionView`.

## [45.2.0] 
- Resources was updated from DIPS.Mobile.DesignTokens

## [45.1.0] 
- Added `TabBadgeService`.

## [45.0.1] 
- [CollectionView][iOS] Support Border as root element for padding.

## [45.0.0] 
- Upgraded .NET MAUI to 9.0.60.
- Added Experimental Features API.

## [44.9.3] 
- [CollectionView][iOS] Fixed bug where auto corner radius in grouped collectionview would sometimes be set wrong.

## [44.9.2] 
- [Tip][iOS] Fixed bug where text sometimes were clipped and in completely wrong position.

## [44.9.1] 
- [ItemPicker] Fixed an issue where keyboard would close if searching with free-text enabled.

## [44.9.0] 
- Resources was updated from DIPS.Mobile.DesignTokens

## [44.8.1] 
- [Tip][iOS] Fixed bug where you could not present a tip in a modal context.

## [44.8.0]
- [ItemPicker] Added `FreeTextItemFactory` and `FreeTextPrefix` properties to support custom text item in bottom sheet mode

## [44.7.0]
- Resources was updated from DIPS.Mobile.DesignTokens

## [44.6.0] 
- Added `AutoScrollingTextView`.
- Added `Animation` API.

## [44.5.0] 
- Resources was updated from DIPS.Mobile.DesignTokens

## [44.4.0]
- [AlertView] Added `ButtonAlignment` property that determines whether the buttons should be aligned underneath the title/description, or to the top right.

## [44.3.0] 
- Resources was updated from DIPS.Mobile.DesignTokens

## [44.2.0] 
- Resources was updated from DIPS.Mobile.DesignTokens

## [44.1.3] 
- [ContextMenu][iOS] ContextMenu's with mode `Pressed` will now rebuild the menu when one of the items' properties changes. 

## [44.1.2] 
- [ListItem][iOS] Fixed a bug where setting `FormattedText` sometimes did not wrap on a new line.

## [44.1.1] 
- [CollectionView][Android] Fixed a bug where CornerRadius and padding were not set on last element if group footer was not set.
- [AmplitudeView] Added ElapsedMilliseconds to Controller.
- [AmplitudeView] Fixed bug where pause animation on timer could be set multiple times.

## [44.1.0] 
- Added AmplitudeView.

## [44.0.1] 
- [ListItem] Fixed a regression bug where consumers could not fill out title without setting `InlineContent`.

## [44.0.0] 
- [BreakingChange][ListItem] Removed `ContextMenu` property.
- [BreakingChange][ListItem] Removed Horizontal -and Vertical Text alignment properties on `TitleOptions` and `SubtitleOptions`, they never worked anyway.
- [ListItem] Optimized ListItem.

## [43.3.0]
- Added `IncludeInheritance` property to `TypeToObjectConverter` to allow to check for sub-types

## [43.2.0] 
- Resources was updated from DIPS.Mobile.DesignTokens

## [43.1.0] 
- Resources was updated from DIPS.Mobile.DesignTokens

## [43.0.1] 
- [Touch][iOS] Fixed a bug where disabling an element right after touch effect fires, makes the element stay at clicked opacity.

## [43.0.0] 
- [Button][Android] Fixed an issue where padding were not added to icon in a button.
- [Touch] Support to also use `IsEnabled` property on the Element.
- [SortControl] Fixed padding of elements in the BottomSheet.
- [BottomSheet] Added default header, which contains title and close button.
- [BreakingChange][BottomSheet] Removed ToolbarItems.

## [42.2.7]
- [SearchPage, SearchBar] Added property `IsAutocorrectEnabled` for consumer to disable autocorrect in search bar.

## [42.2.6] 
- [SearchPage] Fixed an issue where consumer could not modify `AutoHideLastDivider` property.

## [42.2.5] 
- [CollectionView][Android] Fixed issue where headers/footers would get same padding as elements in list.
- [CollectionView] CornerRadius will now get updated to the next item if an item is added to CollectionView using ObservableCollection.
- Added new property `AutoHideLastDivider`; attempts to hide the last divider in CollectionView/VerticalStacklayout (With BindableLayout)

## [42.2.4] 
- [CollectionView][iOS] Fixed regression bug where `AutoCornerRadius` did not work.

## [42.2.3] 
- [CollectionView][iOS] Fixed poor performance.

## [42.2.2] 
- [Chip][Android] Fixed so that default CornerRadius will be set.

## [42.2.1] 
- [SearchPage][ItemPicker] Fixed so that items will get correct default margin and corner radius

## [42.2.0] 
- Added Input dialog
- Refactored DialogService

## [42.1.1]
- [CollectionView][Android] Fix bug where `AutoCornerRadius` would apply to header and footer.

## [42.1.0]
- Added new global property to `Layout`: AutoCornerRadius. Will set corner radius on elements automatically, in lists, the first and last item in the list will receive corner radius.
- [CollectionView] Added padding property. 
- [CollectionView] We now add more additional space at the bottom using a different method, potentially fixing bugs.

## [42.0.1] 
- [Sizes] Convert size to int if BindableProperty expects an int.

## [42.0.0] 
- Import semantic colors and sizes.
- Set semantic colors and sizes on components.

## [41.0.0]
- [ContextMenu] `SetContextMenuItemClickedCallback` is renamed to `ItemsShouldSendGlobalClicks`.
- [ContextMenu] `ItemsShouldSendGlobalClicks` now sends a `GlobalContextMenuClickMetadata`.
- [ContextMenu] Added `Mode` getter to know which mode a menu has.
- [ContextMenuItem] Added `ShouldSendGlobalClick` which controls if all Context Menu item will send a logging callback. Default false.

## [40.3.9]
- [Entry][iOS] Added 'Done' toolbar item. 
- [Entry][iOS] Attempt to avoid seemingly random crash.

## [40.3.8] 
- [dui:Editor][iOS] Fixed an issue where the top of the page would sometimes scroll down if `dui:Editor` resides in a ScrollView/CollectionView.

## [40.3.7]
- [Touch][iOS] Reinitialize the gesture to prevent it from loosing scroll capabilities on iOS 18.3.x.

## [40.3.6]
- [Label][Android] Fixed a crash that could occur if `TruncatedText` is set, while `Label` has not enough width for the `TruncatedText` to display.

## [40.3.5] 
- [Button] Try-catched an unreproducible crash that happened in `ButtonHandler`.

## [40.3.4] 
- [ImageButton][Button][Android] Fixed a rare crash when setting `AdditionalHitBoxSize`.

## [40.3.3] 
- [Button] Fixed an issue where if a button has bound to a property that returns false, the button would not change its style to disabled.

## [40.3.2] 
- [Android15] Opts out of edge-to-edge to fix layout issues.

## [40.3.1] 
- [Button][Android] Touch effect on buttons that defines its own CornerRadius will no longer go out of bounds.

## [40.3.0] 
- Resources was updated from DIPS.Mobile.DesignTokens

## [40.2.0] 
- Added custom implementation of HideSoftInputOnTapped, which works better for custom controls, and works better on Android.

## [40.1.1]
- [AlertView] Ensure icon is placed correctly according to whether `Title` is set or not

## [40.1.0] 
- Resources was updated from DIPS.Mobile.DesignTokens

## [40.0.6]
- [Button][Android] Touch effect on buttons will not go out of bounds anymore.

## [40.0.5]
- [Android][BottomSheet] Don't set status bar color on BottomSheets.

## [40.0.4]
- [iOS] Rewrote how we set the tint color on `ImageButton` and `Image`, fixing possible crashes.

## [40.0.3]
- Make sure functions does not run when handler is null.
- Make sure to null check `PlatformView` in some handlers and log if it is.

## [40.0.2]
- [ImageCapture] Make sure we handle unchecked orientation data

## [40.0.1]
- [SegmentedControl][iOS] Rewrote to use BindableLayout with HorizontalStackLayout to fix a bug where SegmentedControl were put in a CollectionView's header.

## [40.0.0]
- Upgraded to .NET MAUI 9030.

## [39.0.2]
- [ContentControl] Log and avoid possible crash if `BindingContext` and `SelectorItem` is the same object.
- [DatePickers][iOS] Remove popover if a datepicker is disconnected.

## [39.0.1]
- [MultiLineInputField] Ensure that component resets when `IsSaving` is set to `false` manually

## [39.0.0]
- [Colors] Removed old deprecated colors
- [MarkupExtensios] Made sure to add serviceprovider annotation for the missing markupextensions.

## [38.5.4]
- [ContextMenuEffect] Fixed edge-case-crash.

## [38.5.3]
- [ContentControl] Make sure to disconnect handlers of views.

## [38.5.2]
- [Localization] Swapped language of three properties in DUILocalizedStrings.

## [38.5.1]
- [Button] Button's that are disabled as default will now have the disabled style.

## [38.5.0]
- [SingleLineInputField] Fixed bug where header text would take up space even when it was empty
- [MultiLineInputField] Added `ShowButtons` property that determines whether the Save and Cancel buttons should be shown

## [38.4.3]
- [iOS][ContextMenu] ContextMenu will now respect the property `IsEnabled` when the effect is attached to Element.

## [38.4.2]
- [Android] Fixed an issue where the status bar were not set to the correct color when pushing modals.
- [Android] Fixed an issue where the icon's color were not set to the correct color in modal context.

## [38.4.1]
- [ContextMenu] ContextMenu will now respect the property `IsEnabled`, if setting context menu on non-buttons.

## [38.4.0]
- [JsonViewer] Added a new JsonViewer that can be used to view JSON with syntax highligthing. 

## [38.3.1] 
- Fixed a bug where toggled icon were always visible if a `Chip` was set to be toggleable.
- Fixed a crash that sometimes would happen when `Layout` effect were detached on navigating away.

## [38.3.0] 
- Resources was updated from DIPS.Mobile.DesignTokens

## [38.2.0]
- Annotated all classes that inherits from IMarkupExtension with either [AcceptEmptyProvider] or [RequireService[typeof(T)]]
- Enabled Source compilation in Components app.

## [38.1.1]
- [CameraPreview] Fixed potential crash when disconnecting camerapreview.

## [38.1.0]
- Converted all C# bindings to Compiled Bindings.

## [38.0.4]
- [iOS] Made sure long press effect works for dui:Button and other non-MauiView views.

## [38.0.3]
- [iOS][Layout] Fix crash.

## [38.0.2]
- [Label][Android] Error handle when trying to set ellipsis text.

## [38.0.1]
- Use xCode 16.1

## [38.0.0]
- Upgraded to .NET 9.

## [37.5.0] 
- [DialogService] You can now determine if the dialog service is showing a dialog.

## [37.4.3] 
- [Android] Fix crash when trying to remove dialog while app minimized.

## [37.4.2] 
- [Android] Fix crash when trying to display dialog while app minimized.

## [37.4.1] 
- [SaveView] You can now change the opacity of the non-checked animation

## [37.4.0] 
- Resources was updated from DIPS.Mobile.DesignTokens

## [37.3.8]
- [ImageCapture][Android] Fixed an issue where on some devices you could not rotate images because 'Thumbnail' was null.

## [37.3.7]
- [ListItem] Fixed an issue where the visibility of `Subtitle` were not updated when the `Subtitle` were set at a later point.

## [37.3.6]
- [ImageCapture][Android] Fixed an issue where the tap to focus indicator were not animated
- [ImageCapture][iOS] Fixed an issue where the tap to focus indicator were not placed correctly

## [37.3.5]
- [ItemPicker] Fixed an issue where `IsEnabled` was ignored.

## [37.3.4] 
- [ImageCapture][Android] Fixed an issue where the rotated image would have wrong translation in either X or Y direction, when in edit mode.

## [37.3.3] 
- [Button][iOS] Fixed an edge-case where some buttons did not get rounded corners.

## [37.3.2] 
- [ImageCapture] Fixed toolbar height on smaller devices.

## [37.3.1] 
- [ImageCapture][iOS] Fixed close button on thumbnail

## [37.3.0] 
- [ImageCapture][Gallery] Can now rotate images after capturing the photo. Both in gallery and in the actual capturing component.
- [ImageCapture] Controls will now rotate with the device.
- [ImageCapture][Android] Fixed a bug where devices with ultra-wide lens would get a lot of decimals in zoom buttons.
- [ImageCapture] Zoom-slider will now only be visible when the camera is ready.

## [37.2.2] 
- [Button][iOS] Fixed an issue where buttons with `IconButtonSmall` style would have a glitchy icon. 

## [37.2.1] 
- Buttons will now calculate their own size instead of setting static height.

## [37.2.0] 
- Resources was updated from DIPS.Mobile.DesignTokens

## [37.1.2] 
- [SegmentedControl] Avoid setting static Height, instead use padding of content to calculate height so it scales better with font-size. Removing the static height also reduced its height so its similar to the design.
- [Switch][iOS] Fixed an issue where the off color would blend into the background color of the view that is below the `Switch`.

## [37.1.1] 
- [Chip] Fixed bug where consumer could not bind to `CustomIcon` because BindableProperty name did not correspond to the getter and setter
- Will now localize text correctly

## [37.1.0] 
- Resources was updated from DIPS.Mobile.DesignTokens

## [37.0.3] 
- [StateView] Fixed bug where content were not always displayed.

## [37.0.2] 
- [Chip] Fixed bug where consumer could not bind to `CustomIcon` because BindableProperty name did not correspond to the getter and setter

## [37.0.1]
- [ImageCapture] Changed text from "Take new photo" to "Retake photo".
- [ImageCapture] Changed text from "Done" to "Cancel".
- [ImageCapture] Changed icon from "information_line" to "information_fill".
- [ImageCapture] Changed text from "Remove" to "Delete".

## [37.0.0]
- [ImageCapture] Added a new `ImageCapture` API along side with `Camera` api that can be used for both image capture and bar code scanning.
- [BarcodeScanner][BreakingChange] Requires you to provide a CameraFailed delegate when starting the barcode scanner.
- [BottomSheet] Added new property 'IsDraggable', which prevents users from dragging the `BottomSheet`.

## [36.0.1] 
- Set default date to `DateTime.Now` on datepickers to avoid a bug where the datepickers jumps two dates forward.

## [36.0.0] 
- Upgraded MAUI to 8.0.82.

## [35.5.1] 
- [ScrollPicker][iOS] Fixed crash when trying to display scroll pickers.
- [MemoryLeak] Added navigation testing.

## [35.5.0] 
- [Chip][Android] Made sure the text is centered horizontally.
- [Chip] Changed its styling so that it corresponds better to the design.
- [Chip][iOS] Changed the implementation from `UIButton` to usage of `Border` instead.
- [DatePickers][iOS] Moved away from using native compact chip, to using `Chip`, and then show inline date pickers in a popover instead.
- [DatePicker/DateAndTimePicker] Consumers can now add a "Today" button.
- [DatePicker/DateAndTimePicker] Consumers can now set the desired format of the date text.
- [DatePicker] Consumers can now toggle if the date picker should close when selecting a date.
- [NullableDatePickers] Now you can clear the date by tapping a button on the `Chip`.

## [35.4.0] 
- [MemoryLeak] Will now correctly log GC of modal pages.
- [MemoryLeak] Added better logging.

## [35.3.2]
- [Label] Made sure people can change `Text` after it has been truncated and has added the `TruncationText` to the end.

## [35.3.1]
- [Button] Made sure to guard changing style if platformview is null.

## [35.3.0]
- [MultiLineInputField] Add `MaxTextLength` property.

## [35.2.1] 
- [Android][DateAndTimePicker] If there is not enough space to show all characters in DateAndTimePicker, the date will be truncated.

## [35.2.0] 
- [MemoryLeak] Added a bindable attached property so that consumers can ignore memory leak resolving on certain views/pages.

## [35.1.0] 
- Resources was updated from DIPS.Mobile.DesignTokens

## [35.0.11]
- [MemoryLeak] Will now try to resolve memory leaks before checking if there are any.
- [MemoryLeak] Fixed duplicate checking of memory leaks in `BindingContext` of BottomSheets.

## [35.0.10]
- [MemoryLeak] Fixed crash of app when changing root page.

## [35.0.9]
- [MemoryLeak] Always wait for 9 GC collections before checking if there is any memory leaks.

## [35.0.8]
- [BottomSheet] Fixed an issue where app would crash if attempting to create a bottom bar in bottom sheet.

## [35.0.7]
- [MemoryLeak] Improved GC helping and better output for developers to solve memory leaks.

## [35.0.6]
- [MemoryLeak] Improved GC helping when changing root and when modals are present, both modals with navigation and not.
- [MemoryLeak] Made sure BottomSheet binding context will try to GC in debug.

## [35.0.5]
- [MemoryLeak][iOS] Fixed an issue where a leak could appear in context menu with `Pressed` mode.

## [35.0.4]
- [MemoryLeak] Improved the output when pages/binding context is resolved or has leaks.
- [MemoryLeak] Added observing of a pages BindingContext.

## [35.0.3]
- [MemoryLeak] Fixed an error where if you removed a page from the stack while in a modal context, all pages in the navigation stack would try to be resolved for memory leaks.
- Refactor `TryResolvePoppedPages` function.

## [35.0.2]
- [Android][Chip] Fixed an issue where checkmark were sometimes not visible or the checkmark icon were tinted as black
- [Android][ChipGroup] Fixed an issue where you could not de-toggle toggled chips when multi-selected `ChipGroup` were used.
- [Android][Chip] Some refactor and code cleanup in its handler.

## [35.0.1]
- Fixed an issue where the app could crash when navigating back and forth if the automatic memory resolving were activated.

## [35.0.0]
- Updated .NET MAUI from 8.0.60 to 8.0.70.

## [34.0.1]
- Fixed an issue where pages inbetween were not GC'ed when `PopToRoot` were used.

## [34.0.0]
- Refactored `GCCollectionMonitor`, so that it pinpoints what handlers/views that is leaking
- `GCCollectionMonitor` will now try to resolve memory leaks automatically.
- Patched up all/most leaks in DUI.

## [33.0.2]
- [Android] Removed workaround for setting border px to 0 in ListItem
- [iOS] Removed workaround in ContentControl where CollectionView did not use available space

## [33.0.1]
- [iOS] Removed workaround for setting correct width on button/chip when they have an image.

## [33.0.0]
- Removed `VirtualListView`.

## [32.0.1]
- [Android] Removed old code for a workaround where we created our own toolbar for modals 

## [32.0.0]
- Upgraded MAUI to 8.0.60.
- [MAUI-Upgrade][Android] Fixed an issue where icons were not placed at center in buttons

## [31.0.7]
- [ShellRenderer][Android] Check if index is out of range before trying to get `ToolbarItem`.

## [31.0.6]
- [ShellRenderer][Android] Null check on `CurrentPage`.

## [31.0.5]
- [ContextMenuToolbarItem][Android] Made sure menu listeners are re-added when a modal page has been popped.
- [ContextMenuToolbarItem][Android] Made sure it does not memoery leak.

## [31.0.4]
- [ContextMenuToolbarItem] Bindings for Context Menu Items now works.

## [31.0.3]
- Added internal logging framework for DUI. The logs can be observerd in LogCat, Console and XCode logs.

## [31.0.2]
- [FloatingNavigationMenu][Android] Prevent fragment from creating its self

## [31.0.1]
- Library is now built on latest macOS 14.
- Added exception handling for components app.

## [31.0.0]
- [Icons] Now return as ImageSource.
- [Icons] Removed AsImageSource method as its not needed anymore.
- [FloatingNavigationButton] Added try-catch to prevent app crash and it now logs to the Console for debugging.

## [30.7.0] 
- Resources was updated from DIPS.Mobile.DesignTokens

## [30.6.0] 
- Resources was updated from DIPS.Mobile.DesignTokens

## [30.5.1]
 - [AlertView] Corrected icon for Error style.
 
## [30.5.0] 
- Resources was updated from DIPS.Mobile.DesignTokens

## [30.4.1] 
- [AlertView] Corrected design.

## [30.4.0] 
- Resources was updated from DIPS.Mobile.DesignTokens

## [30.3.1] 
- [AlertView] Updated icons for Error and Warning style.

## [30.3.0] 
- Resources was updated from DIPS.Mobile.DesignTokens

## [30.2.0]
- [AlertView] Added alert view. A new component to use in line when you need to alert people using your app.

## [30.1.0] 
- [BottomSheet] Added property `IsBackButtonVisible`.

## [30.0.1] 
- Made sure all public image sources has a image source converter so the component do not freeze up on Android when passing strings.

## [30.0.0] 
- [Icons] Changed icons from ImageSource to string to prevent memory leaks when using icons.

## [29.5.0] 
- Resources was updated from DIPS.Mobile.DesignTokens

## [29.4.1]
- [DateAndTimePicker][Android] Fixed an issue where Time would only add +2 hours no matter what you did.
- [DateAndTimePicker][Android] Fixed an issue where the component would show the converted date to consumer, and not the actual DateTime set from consumer.
- [DateAndTimePicker][Android] Fixed an issue where IgnoreLocalTime did not work correctly.

## [29.4.0]
- [BottomSheet] Added property 'BackButtonBehavior', so that the back button can be set in a bottomsheet.

## [29.3.2]
- [MultiItemsPicker] Fixed an issue where `MultiItemsPicker.DidSelectItem` was not triggered when first item was selected

## [29.3.1]
- [iOS][ContextMenuToolbarItem] Fixed an issue where `IsCheckable` did not work.

## [29.3.0]
- Added `ContextMenuToolbarItem`, to set context menu on a `ToolbarItem`.

## [29.2.0] 
- Resources was updated from DIPS.Mobile.DesignTokens

## [29.1.3]
- [ListItem] Now there is no need to set `Title` or `Subtitle` on `ListItem` in order to use the `FormattedText` in their options.
- [ListItem][Android] Fixed an issue where a blank `Subtitle` would take up space.
- [ListItem] Fixed a bug where a `ListItem` in a `CollectionView` sometimes don't give `Title` it's required space. 
- [VirtualListView][Android] The `OnScrolled` event will never be invoked now unless user has scrolled.

## [29.1.2]
- [VirtualListView][Android] When the list is rendered the first time, it won't invoke `OnScrolled` event.
- [ListItem] `Title`, `Subtitle` and `InlineContent` will now always be centered no matter the height of the `ListItem` (If underlying content has not been set).
- [ListItem] Setting `FormattedText` of `Title` in `TitleOptions` will now make the `TitleLabel` wrap correctly.
- [ListItem] `Subtitle` will now adhere to `LineBreakMode`, instead of just cutting off.

## [29.1.1]
- [NullableDatePicker] Fixed an issue where if dates were set as default, the pickers would still be null.

## [29.1.0]
- [SegmentedControl] Added property `ShouldDeSelectOnSameItemTapped`.
- [SegmentedControl] Fixed bug where `DidDeSelectItem` and `DidSelectItem` were fired when consumer tapped on the item that already was selected. 

## [29.0.4]
- [SaveView] Made sure SaveView SavingText is bindable.

## [29.0.3]
- [DatePicker][iOS] Made sure IgnoreLocalTime is respected.
- [DateAndTimePicker][Android] Made sure IgnoreLocalTime is respected.

## [29.0.2]
- [NullableDatePickers] Fixed an issue where consumers could not set date or time.

## [29.0.1]
- [ScrollPicker][iOS] Fixed an issue where you mass pressed on a scroll picker the application would crash
- [ScrollPicker][iOS] If consumer sets selected index to -1, the scroll picker's popover will be closed.
- [NullableDatePickers] If consumer sets the date or time to null, the switch will be set to false. 

## [29.0.0] 
- [BreakingChange][ScrollPicker] Refactored so that it can be nullable.

## [28.6.1] 
- [iOS][Chip] Add slightly more width when `IsToggleable=True` in order to not truncate title.

## [28.6.0] 
- Added new components: `NullableDatePicker`, `NullableTimePicker` and `NullableDateAndTimePicker`.
- [iOS] DatePickers will now have exact same design as Chips. 

## [28.5.0] 
- Resources was updated from DIPS.Mobile.DesignTokens

## [28.4.1]
- [ChipGroup] Clear old items when `ItemsSource` changes.

## [28.4.0] 
- [iOS][InlineDatePicker] Changed the presentation of `InlinedatePicker` from BottomSheet to Popover.

## [28.3.2]
- [Tip] Made sure tip can be opened inside bottom sheets on iOS.

## [28.3.1] 
- [ItemPicker] Fixed an issue where the context menu did not reflect correctly what item was selected when setting selected item programatically.

## [28.3.0] 
- [iOS] Fixed performance of BottomSheet

## [28.2.0] 
- Resources was updated from DIPS.Mobile.DesignTokens

## [28.1.0]
- Revert MAUI 8.0.20

## [28.0.0]
- [ScrollPicker][BreakingChange] Added `OnDataInvalidated`, to force the UI to update itself. Added a BaseScrollPickerComponent.

## [27.6.0]
- Added support for MAUI 8.0.20

## [27.5.0]
- [ChipGroup] Added the ability to set the selected items to an empty list.
- [ChipGroup] Fixed an issue where the consumer could not set selected items after the component has been initialized.

## [27.4.0]
- Added 'FormattedText' to Subtitle in `ListItem`.

## [27.3.0]
- Added support for MAUI 8.0.14. 

## [27.2.1]
- [Android] Ensure `TaskCompletionSource.Result` for `BottomSheetFragment` is set after fragment is removed from stack.

## [27.2.0]
- Handle `IScrollPickerComponent` being `null`.

## [27.1.0]
- Added `VirtualListView`
- Fixed an issue where `Sizes as font sizes` page were jittering

## [27.0.0]
- Added `ScrollPicker`.

## [26.5.0]
- [Tip] Added a new `TipService` to attach a tip to a view and show it.
- [Tip] Added `TipCommand`to easily show a tip with your view.
- [BarcodeScanner] Added the possibility of adding a tip to the zoom slider.

## [26.4.0]
- Changed default localization to norwegian.

## [26.3.0] 
- Resources was updated from DIPS.Mobile.DesignTokens

## [26.2.0]
- Added new component: `ChipGroup`.

## [26.1.0]
- [EmptyView] is now public
- [EmptyView] Added EmptyViewModel as a property
- [ErrorView] is now public
- [ErrorView] Added ErrorViewModel as a property
- [BarcodeScanner][iOS] Made sure capture output runs on its own dispatcher.
- [BarcodeScanner][iOS] Set preset to PresetHigh for better image quality while scanning.

## [26.0.0]
- [BreakingChange][Camera] Renamed `Preview` to `CameraPreview`

## [25.0.0]
- [BarcodeScanner] Made sure the bar code scanning result goes through an observation process to determine the barcode with the most number of scans to the consumer.
- [BreakingChange][BarcodeScanner] BarcodeScanner.Start now requires a `DidFindBarcodeCallback(BarcodeScanResult)` instead of `Action<Barcode>`.
- [BreakingChange][BarcodeScanner] `BarcodeScanMetadataView` is now renamed to `BarcodeScanResultView` which accepts `BarcodeScanResult`.

## [24.13.2]
- [BarcodeScanner][iOS] Improved rect of interest
- [BarcodeScanner][iOS] Improved recommended zoom factor
- [BarcodeScanner][iOS] Made sure the components app does not stop and start between results, this makes it more stable on iOS.

## [24.13.1]
- [Image][iOS] Made sure we do nessesary null-check when the image is ready to be tinted.

## [24.13.0]
- [BarcodeScanner][Android] Added slider for zooming.

## [24.12.0]
- [BarcodeScanner][iOS] Added haptic when people tap to focus

## [24.11.0]
- [BarcodeScanner][iOS] Added pinch to zoom
- [BarcodeScanner][iOS] Improved tap to focus
- [BarcodeScanner][iOS] Improved recommended zoom factor for people
- [BarcodeScanner][iOS] Accessability: Added better VoiceOver feedback for zoom slider

## [24.10.1]
- Fixed components app.

## [24.10.0]
- Added Barcode Scanner. This is not stable yet, but ready to be tested. 

## [24.9.0] 
- Pin dotnet SDK and MAUI version (8.0.100 | 8.0.3).

## [24.8.0] 
- Resources was updated from DIPS.Mobile.DesignTokens

## [24.7.5]
- [Picker] Provide `Title` property for `Mode=BottomSheet`.

## [24.7.4]
- [Android] Fixed an issue where icons were black in modal pages.

## [24.7.3]
- Fixed an issue where several `SaveView`'s in `ContentSavePage` could potentially be created.

## [24.7.2]
- Make event for context menu item clicked be triggered before the command is triggered

## [24.7.1]
- [Android] Fixed an issue where application would crash if the `ShellToolbarAppearanceTracker` disposed itself before setting appearance

## [24.7.0]
- Added possibility to set options for DIPS UI in `MauiProgram.cs`
- Added option for setting global callback for when a context menu item is clicked

## [24.6.2]
- [Android] Fixed an issue where toolbar items with icon added later to the page would be black.

## [24.6.1]
- Fixed an issue where the label inside `SaveView` would take up space when text empty.

## [24.6.0]
- [GarbageCollecting] Added `ShouldGarbageCollectPreviousPage` to Shell for a simple way of monitoring the previous page.

## [24.5.2]
- Set default `MaxLines` and `LineBreakMode` for subtitle on `ListItem`.

## [24.5.1]
- [Android] Added null check to `CornerRadius` effect. 

## [24.5.0]
- Added 'Layout' effect with attached property `CornerRadius`.

## [24.4.0]
- Added `FooterView` to `SearchPage`.

## [24.3.0]
- Added profiling properties for pages. [See this link](https://github.com/DIPSAS/DIPS.Mobile.UI/wiki/Performance#profiling-page-loading).

## [24.2.0]
- [ListItem] Added `MaxLines` and `LineBreakMode` to subtitle

## [24.1.0]
- [GarbageCollection] Added garbage collection API to use whever you want.
- [MemoryLeaks] Added memory leaks project to use for testing in the future.

## [24.0.3]
- [StateView] Fixed an issue where RefreshCommand would get executed several times when consumer enabling RefreshView for default implementations.

## [24.0.2]
- Reset before memory leak.

## [24.0.0]
- [BreakingChange][StateView] Added the ability for consumer to add a RefreshView to some state views.

## [23.2.3]
- [ContextMenu] Add icon to group if Icon property is set and `IsCheckable` is false.

## [23.2.2]
- Fixed an issue where title and description on EmptyView and ErrorView were not horizontally aligned

## [23.2.1]
- [Label] Fixed an issue where label would not be shown if text was set after view was loaded.

## [23.2.0] 
- Resources was updated from DIPS.Mobile.DesignTokens

## [23.1.2]
- [Label] Fixed an issue where Label would crash application if Text was not set.

## [23.1.1]
- [StateView] Fixed an issue where the starting state was not set
- [StateView] Fixed an issue where the default views would always reference to the same object in memory, even though user has navigated back from the page and navigated in again

## [23.1.0]
- Added option to turn on garbage collection logging when debugging for content pages.

## [23.0.0]
- [BreakingChange] [StateView] ViewModel is now in charge of creating 'StateViewModel', added new property: 'ShouldUpdateViewWhenStateSetToSame'
- [iOS] [Label] Added a null check when checking for truncation 

## [22.6.0]
- Added two new properties to Label: 'TruncatedText' and 'TruncatedTextColor'

## [22.5.2]
- [SystemMessage] Removed animation from the Android implementation to make sure the system message is horizontally in center and that the font size is correct.

## [22.5.1]
- [ListItem] Made sure list item can be subscribed to and tapped on without having to set the `Command` as well.

## [22.5.0]
- Added StateView.

## [22.4.4]
- [SearchBar] Made sure to colorize cancel button when HasCancelButton has changed on ios.

## [22.4.3]
- [ContextMenu] Fixed Context Menu data context not getting updated in collections that are updated from another page
- [SearchBar] Made sure `CancelCommand` is nullable and cancling without having a `CancelCommand` does not crash.

## [22.4.2]
- [iOS][ItemPicker] Ensure chip resizes when selected item changes.

## [22.4.1]
- [SearchBar] Made sure unfocused event was correctly mapped.

## [22.4.0]
- Fixed an issue where keyboard would still be opened when scrolling in ItemPicker bottomsheet.
- Added new property: 'ShouldAutoFocusSearchBar' for BottomSheet and ItemPicker. 

## [22.3.1] 
- Fixed an issue where HorizontalInlineDatePicker took too much height when in landscape mode.
- Fixed an issue where HorizontalInlineDatePicker would instantly snap to dates when scrolling.
- DateView now changes its view based on its orientation.

## [22.3.0] 
- Resources was updated from DIPS.Mobile.DesignTokens

## [22.2.2]
- [iOS] Chip will now have touch effect when tapping
- [iOS] Fixed an issue where the commands would fire when touches began and not ending on Chip
- Cleanup in ChipHandler for iOS

## [22.2.1]
- [Button][iOS] Made sure setting size from the library only happens when the button has a text and a an image.

## [22.2.0] 
- Resources was updated from DIPS.Mobile.DesignTokens

## [22.1.3]
- Fixed an issue where the floating navigation button sometimes did not respect changing orientation.
- Fixed an issue where buttons with only an icon did not have the correct layout.

## [22.1.2]
- [SlidableLayout] Fixed a bug where you couldn't tap the next element (only the previous) in a slidable layout

## [22.1.1]
- [Button] Fixed an issue when setting image to a button taking the entire parent space.

## [22.1.0]
- Introduced two new properties on SearchBar.
- Keyboard now dismisses when pressing return key when having focus on dui:SearchBar
- SearchPage now changes the text/icon of return key determined by 'SearchMode'
- [iOS] Fixed an issue where the return key were not set as 'Done' when focusing dui:Entry 
- [Android] Fixed an issue where dui:Entry were not unfocused when pressing the return key

## [22.0.1]
- Make sure dui:Editor and dui:Entry capitalizes each new sentence.

## [22.0.0]
- Added new property to ContentPage: 'ShouldHideFloatingNavigationMenuButton'.
- [BreakingChange] Removed the ability to set predefined pages where the FloatingNavigationMenuButton would hide itself automatically.

## [21.0.0]
- [BreakingChange] Changed so that any type can be compared with when checking to hide the floating navigation button.

## [20.14.1]
- Fixed an issue where floating navigation button would go out of bounds when changing orientation

## [20.14.0]
- [MultiLineInputField] Added CancelCommand.
- [MultiLineInputField] Added Save and Cancel tapped events.

## [20.13.1]
- [BottomSheet][Android] Fixed an issue where pressing delete buttons when the bottom sheet was open caused it to try closing.

## [20.13.0]
- Set default margin for dividers on ListItem.
- Added new property to ListItem: 'AutoDivider'

## [20.12.2]
- [iOS] Fixed an issue where releasing the long-press context menu interaction too early would fire off the tap event.
- Changed default padding of ListItem

## [20.12.1]
- [BottomSheet] Made sure `Title` is visible for Android.

## [20.12.0]
- Added `MaxWidth` to `ListItem.TitleOptions`

## [20.11.0] 
- Resources was updated from DIPS.Mobile.DesignTokens

## [20.10.2]
- Moved the sort icon to the left side of the text in SortControl, this implicitly also fixes that the text wont ever get truncated

## [20.10.1]
- [BottomSheet][Android] Made sure consumers do not need to set the text of a toolbar item to add the toolbar item on Android.

## [20.10.0]
- [ScrollView] Added additional space at the bottom of scroll view to make sure the last item is scrolled to approx half the size of the scrollview for better UX.

## [20.9.0]
- [SearchPage] Made sure keyboard will disapear when scrolling the result.
- [SearchPage] Keyboard is now removed when people tap the cancel button.
- [CollectionView] Added a new ContentHeight property that will return the correct height of the CollectionView on iOS and Android.
- [CollectionView] Changed the size of the invisible footer from 1/3 to 1/2 of the CollectionView.

## [20.8.3]
- SaveView will now only vibrate on success when user actually taps the
SaveView (Added new property 'Command' to SaveView)

## [20.8.2]
- [Android] Fixed a regression bug where the cursor would not be set at the end of the text when focusing Editor

## [20.8.1]
- Fixed an issue where buttons in BottombarButtons (BottomSheet) did not take up full width
- [Android] Fixed an issue where the Bottombar had too low height

## [20.8.0]
- Fixed an issue where changing the 'Position' property on BottomSheet would only run one time.
- [iOS] Fixed an issue where changing the 'Position' property on BottomSheet would not animate the change
- Added the ability to set the position of the last opened BottomSheet in BottomSheetService
- [Android] dui:Entry and dui:Editor will now expand the BottomSheet it is in when they have focus

## [20.7.3]
- [iOS] Fixed an issue with nesting of the touch API, where several touch commands would fire at the same time.

## [20.7.2]
- [Android] Fixed an issue where the keyboard would not show up if BottomBarButtons were set in a BottomSheet

## [20.7.1]
- Fix padding for toggleable chip on iOS.

## [20.7.0]
- [MultiLineInputField] Buttons will be disabled when programatically saving
- Fixed an issue where the 'IsInteractiveCloseable' property on BottomSheet was set to BindingMode.OneTime

## [20.6.0]
- Added new option to InLineContentOptions in ListItem so that the InLineContent can span over UnderlyingContent
- Fixed an issue where focusing on a dui:Editor for the first time would not place cursor at the end of the text
- Fixed an issue where the BindingContext were not set for BottomBarButtons in a BottomSheet

## [20.5.0]
- Added new properties to MultiLineInputField so that consumers can give users feedback when saving
- Fixed an issue where consumers could not set the padding of ListItem to zero.

## [20.4.0]
- [BottomSheet] Added BottomBarButtons, to add floating buttons to the bottom of the bottom sheet.

## [20.3.0] 
- Resources was updated from DIPS.Mobile.DesignTokens

## [20.2.0] 
- Resources was updated from DIPS.Mobile.DesignTokens

## [20.1.0]
- Renamed 'IsEllipsized' property to 'IsTruncated' in Label
- Now checks if the Label is truncated, does not matter if LineBreakMode is set (Ellipsis is apparent)
- Added new components: SingleLineInputField and MultiLineInputField
- Can now disable default padding in dui:Entry and dui:Editor

## [20.0.0]
- [BottomSheet] Will now respond to property changes when its open.
- [BottomSheet] Added new `Position` property which will determine what position the bottom sheet will present in when opened.
- [BottomSheet] People can drag the bottom sheet to large when the position is medium and vica versa.
- [BottomSheet] Consumer can open multiple bottom sheets and close them by using `BottomSheetService.CloseAll()`
- [BottomSheet][BreakingChange] Replaced `OpenBottomSheet()` with `Open()`
- [BottomSheet][BreakingChange] ShouldFitContent is replaced with `Position` = `Fit`

## [19.11.1]
- [ContextMenu] Group `ContextMenuGroup` if at least one item alongside it. 

## [19.11.0]
- [SearchBar] Made it possible to Focus and Unfocus
- [SearchBar] Made sure consumers can set `Text` property.

## [19.10.1]
- Fixed an issue with ListItem where Dividers took up space in height
- Fixed an issue where setting CornerRadius on ListItem would make the ListItem take up more space in height

## [19.10.0]
- [SearchBar] Hooked up Focused event from internal serach bar to the component.
- [SearchPage] Added ShouldAutoFocus for the consumer to disable focusing.
- [SearchPage] Added SearchBarFocused event.

## [19.9.0]
- Bottom sheets can now be set to not be dismissed when user swipes down the bottomsheet.
    - Added two new properties to BottomSheet: 'IsInteractiveCloseable' and 'OnBackButtonPressedCommand'.

## [19.8.0]
- Added `TextColor` and `Style` properties to `SubtitleOptions`

## [19.7.0]
- [CollectionView] Added ReloadData method for consumers to redraw all items without modifying ItemSource. 

## [19.6.0] 
- Resources was updated from DIPS.Mobile.DesignTokens

## [19.5.1]
- Fix not being able to set `LoadableListItem.ContextMenu`

## [19.5.0] 
- Resources was updated from DIPS.Mobile.DesignTokens

## [19.4.3] 
- Changed default dui:ContentPage background color

## [19.4.2] 
- Reverted change where color on Image were default set

## [19.4.1] 
- Fixed an issue where SectionHeader style was not correct
- Changed 'UI' font to a more bold font so that it is easier to differentiate between 'UI' and 'Body' fonts. 

## [19.4.0] 
- Changed default page background-color.
- Added new component: dui:Editor.
- Changed from 16 -> 12 padding left and right in ListItem
- Changed from 16 -> 8 margin between icon and title in ListItem
- Moved icon to the left of the text in SortControl
- Set font-size, fontfamily and placeholdercolor in dui:Entry
- Set default textcolor and placeholdercolor in dui:SearchBar
- Changed default tintcolor for dui:Image.
- Added new style for Labels: 'SectionHeader'

## [19.3.3]
- Reset base size changes on icons.

## [19.3.2]
- Increased base size of icons.

## [19.3.1]
- Ensure correct style is set for toggleable `Chip` when toggled off.

## [19.3.0] 
- Resources was updated from DIPS.Mobile.DesignTokens

## [19.2.0] 
- Resources was updated from DIPS.Mobile.DesignTokens

## [19.1.0] 
- Resources was updated from DIPS.Mobile.DesignTokens

## [19.0.1]
- [iOS] Fix distance between toggled icon and title on `Chip`.

## [19.0.0]
- Added toggleable property on `Chip`
- [BreakingChange] Change property `Chip.HasCloseButton` to `Chip.IsCloseable`

## [18.2.0]
- Added new control: 'Entry'

## [18.1.0]
- [Android] Fixed a bug where progress bar would not take full width in Searchbar
- Set Ghost buttons to transparent background
- Cleaned up unused component
- Fixed a bug in SavingSamples where it would remove the SaveView before popping the page

## [18.0.0]
- [BreakingChange] Changed TitleOptions property from FontSize to Style
- Added styles for Labels
- [iOS] Fixed periodic crash on initialization of Button in new .NET MAUI version.
- Upgraded to newest .NET MAUI version

## [17.26.0] 
- Resources was updated from DIPS.Mobile.DesignTokens

## [17.25.0] 
- Resources was updated from DIPS.Mobile.DesignTokens

## [17.24.1]
- [iOS] Fixed a bug where items inside ListItem were sometimes not tappable

## [17.24.0]
- Added support for dividers in context menu

## [17.23.5]
- [iOS] Fixed an issue where having native buttons in a subview of a view that has the touch API on it, would fire both the button and the view that has the touch api .
- [iOS] Fixed an issue with the touch API, where if you press and then drag the element, the touch effect would never go back to its default state.

## [17.23.4]
- [Android] Fixed an issue where the action buttons in dialogs were not sometimes placed on the bottom
- Fixed an issue where the first and the last button in segmentedcontrol were squished to the side when selected

## [17.23.3]
- [iOS] Fixed ShouldFitToContent on BottomSheet ignoring height of navigation bar

## [17.23.2]
- [iOS] Possible fix for crash when using long press context menu

## [17.23.1]
- [Android] Keyboard will now close when navigating from a searchbar

## [17.23.0]
- Modified 'CheckmarkListItem' so that changing IsSelected does not fire SelectedCommand

## [17.22.1]
- [RefreshView] Removed work around for RefreshView for .NET 7 which causes a glitch for iOS CollectionView in .NET 8.
- [ContextMenuItem] Made sure Item is a bindable property.

## [17.22.0]
- Added 'FormattedText' property on TitleOptions
- Added 'IsDebugMode' and 'DebugOptions' property on ListItem

## [17.21.0]
- Added 'IsDestructive' property on ContextMenuItem

## [17.20.3]
- Added ContextMenuOptions on ListItem
- [Android] Fixed an issue where setting both ContextMenu and Touch API, would disable ContextMenu

## [17.20.2]
- [Button] Renamed Rounded styles to IconButton

## [17.20.1]
- [Button] Changed the disabled text color of buttons.

## [17.20.0]
- [Button][Styles] Added PrimaryRoundedSmall/Large
- [Button][Styles] Added SecondaryRoundedSmall/Large
- [Button][Styles] Added GhostRoundedSmall/Large

## [17.19.4]
- [iOS] Fixed another issue where Context menu would crash when in a CollectionView

## [17.19.3]
- [ListItem] Margin and Paddings are now set on the underlying Border to prevent glitching placements of the containers contet.

## [17.19.2]
- [iOS] Fixed an issue where Context menu would crash when in a CollectionView
- Added 'IsVisible' property to ContextMenuItem

## [17.19.1]
- Fixed an issue where ContextMenuEffect could not be used in a CollectionView

## [17.19.0]
- Made property 'IsChecked' bindable in ContextMenuItem

## [17.18.0]
- [ListItem] Title will wrap better for consumers.
- [ListItem][TitleOptions] Added ability to control `LineBreakMode` and `MaxLines` to support truncation and wrapping properly.
- [ListItem] Made sure width options are set even if the corresponding view for the options is not set.
- [ComponentsApp] Made sure `none` is not added to animations page.

## [17.17.3]
- [ImageButton] Made sure ImageButton padding is correctly set when IsVisible is flipped for Android

## [17.17.2]
- [DatePicker][DateAndTimePicker] Fixed default colors being broken on iOS.

## [17.17.1]
- [ContentSavePage] Setting IsSaving back to 'false' will now switch the content back to the original content

## [17.17.0]
- Added `DateTimeView` component
- Made `DateView` and `SelectableDateViewModel` part of the public API

## [17.16.0]
- [FilledCheckBox][SaveView][ContentSavePage] Changed the way the filled checkedbox looks when its not checked.


## [17.15.1] 
- [SaveView] Changed vibration effect to `success` vibration.
- [Switch] Made sure it takes less space on Android.
- [SegmentedControl] Made sure it does not clip on Android.

## [17.15.0] 
- [FloatingNavigationButton] Changed the colors when opened and closed.
- [FloatingNavigationButton] Removed opacity.
- [FloatingNavigationButton] Changed the menu icons when opened and closed.
- [FloatingNavigationButton] Made sure VoiceOver and TalkBack works better with the component.

## [17.14.1] 
- [Android] Fixed an issue where icon in ToolbarItem did not set correct color

## [17.14.0] 
- Resources was updated from DIPS.Mobile.DesignTokens

## [17.13.2]
- [FloatingNavigationButton] Made sure people can tap the right side of the screen.

## [17.13.1] 
- Fixed an issue where app would crash trying to set tint color on Button when using hot reload

## [17.13.0] 
- Resources was updated from DIPS.Mobile.DesignTokens

## [17.12.3] 
- Fixed an issue where app would crash trying to set tint color on ImageButton when using hot reload

## [17.12.2] 
- [FloatingNavigationButton] Fixed an bug where the entire page of the app was not clickable until you opened the Floating Navigation.

## [17.12.1] 
- [Layout] Made sure all layouts ignore safe area when its not set by the consumer. Grid, StackLayout etc.
- [FloatingNavigationButton] Made sure people can tap the background of the FAB to close it.
- [FloatingNavigationButton] Made sure sizes of the icons are smaller for better UX.
- [FloatingNavigationButton] Made sure its accesibile for text readers.

## [17.12.0] 
- Resources was updated from DIPS.Mobile.DesignTokens

## [17.11.0] 
- Changed touch effect color on Android for dui:Buttons
- Icon size on buttons will now scale down with the Button's height

## [17.10.0] 
- Resources was updated from DIPS.Mobile.DesignTokens

## [17.9.0] 
- Resources was updated from DIPS.Mobile.DesignTokens

## [17.8.0] 
- Added Button styles

## [17.7.0] 
- [ItemPicker] Added haptics for when people select a item from ItemPicker with context menu mode.
- [SortControl] Added haptics for when people select a sort option.
- [SegmentedControl] Added haptics for when people select/deselect a segment from the segmented control.
- [ContextMenu] Added more shadow to context menu on Android.

## [17.6.0] 
- Resources was updated from DIPS.Mobile.DesignTokens

## [17.5.0] 
- Resources was updated from DIPS.Mobile.DesignTokens

## [17.4.0]
- [SaveContentPage] Added haptics when the page has finished saving.
- [ItemPicker] Added haptics when people select a item.
- [MultiItemsPicker] Added haptics when people select a items.
- [Checkmark] Added haptics when people touch it.
- [RadioButton] Added haptics when people touch it.

## [17.3.0]
- Fixed an issue where ItemPicker in bottom sheet had strange behavior when searching.
- [ItemPicker] The style when a placeholder is present has now changed.
- [ItemPicker] The default Placeholder is now `Choose`. The previous value was string empty.
- [ItemPicker] Fixed an issue where ItemPicker in bottom sheet had strange behavior when searching.
- [ItemPicker] If the consumer changes ItemsSource when people have selected an item that is not a part of the new ItemsSource, the SelecteItem should be set to null.
- [SearchPage] Made sure the Android keyboard is removed when the search page is destroyed.
- [SearchPage] Made sure Android keyboard is shown when the page is displayed and the search bar has focus.
- [SearchBar] Made sure we can set focus correct for Android with Focus().
- [ContentSavePage] Made sure animation is always displaying.
- [Chip] Made sure truncates and the end of the chip if the title is too long.


## [17.2.0]
- Resources was updated from DIPS.Mobile.DesignTokens

## [17.1.1]
- [Components] Fixed an issue where iOS appicon did not work. 

## [17.1.0] 
- [Android] Removed workaround for padding on ImageButton
- Fixed an issue where Components would not build

## [17.0.0] 
- Upgraded project to .NET 8
- Removed Modal effect

## [16.19.0] 
- Resources was updated from DIPS.Mobile.DesignTokens

## [16.18.0]
- Added new property to SearchPage: CancelCommand

## [16.17.2]
- Changed design of SortControl
- Changed so that items will be compared with .Equals function instead of by memory reference in SortControl

## [16.17.1]
- Fixed a bug where SortControl would crash if ItemsSource were not set instantly

## [16.17.0]
- Added SortControl
- [iOS] Fixed a bug where the color of an Image/ImageButton would be reset to black when changing the source in runtime
- Added DividersOptions property to ListItem to be able to set margin on TopDivider and BottomDivider

## [16.16.2]
- [SearchBar] Made sure android removes the text when the clear button is tapped.

## [16.16.1]
- [InLineDatePicker] Fixed an bug where it would sometimes not render until people had tapped or slided the layout.

## [16.16.0] 
- Resources was updated from DIPS.Mobile.DesignTokens

## [16.15.0] 
- [FloatingNavigationButton] Added option to add a navigation button as the last button. This will be respected if a new button is added after the button was added.

## [16.14.0] 
- [SearchBar] Added ClearCommand as a command to execute when people tap the clear button (x mark).
- [SearchPage] Make sure clear the text and state of the view when people tap the clear button.
- [ItemPicker] Made sure Placeholder gets set the first time it draws.

## [16.13.1]
- Selected Item can now be set to null. When its set to null the placeholder will be used. 

## [16.13.0] 
- Resources was updated from DIPS.Mobile.DesignTokens

## [16.12.0] 
- Resources was updated from DIPS.Mobile.DesignTokens

## [16.11.4] 
- Added unique identifiers for the animations,sizes and colors resources enum.

## [16.11.3] 
- Made sure to do a nullcheck on SearchCommand before executing it.

## [16.11.2] 
- Added unique identifiers for the icon resources enum.

## [16.11.1] 
- Made sure filled check box touch effect works when the command property changes

## [16.11.0] 
- Resources was updated from DIPS.Mobile.DesignTokens

## [16.10.1] 
- [Android] Made sure we safely commit the fragmentmanager when the state was lost to prevent app crashes.

## [16.10.0] 
- Resources was updated from DIPS.Mobile.DesignTokens

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
