Bottom sheet is a component designed for you to display content anchored to the bottom of your page for people to use. This is considered a modal, a mode that people go to but still maintain focus to the context they are in.

# Inspiration
- Android: [Material Design 2 Bottom Sheets](https://m2.material.io/components/sheets-bottom)
- iOS: [Sheets - Human Interface Guidelines](https://developer.apple.com/design/human-interface-guidelines/components/presentation/sheets/)

# Usage

Create your bottom sheet by creating a XAML class that derives from `dui:BottomSheet`.
```xml
<dui:BottomSheet xmlns="http://xamarin.com/schemas/2014/forms"
                 xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
                 xmlns:dui="http://dips.com/mobile.ui"
                 x:Class="<MyNameSpace>.MyBottomSheet"
                 ....
                 >
    <!-- Content of bottom sheet goes here -->
</dui:BottomSheet>

```

Open the bottom sheet using C#:
```csharp
await BottomSheetService.Open(new MyBottomSheet());
```

Or by XAML:
```xml
<dui:Button Text="Open my bottom sheet"
            Command="{dui:OpenBottomSheetCommand {x:Type sheets: MyBottomSheet}}" />
```

## Features
- **Customizable Position**: Adjust the starting position of the bottom sheet.
- **Interactive Close**: Control whether users can swipe to close the sheet.
- **Header and Buttons**: Includes a default header and support for bottom bar buttons.

## Use Cases
- Display additional options or actions without leaving the current context.
- Provide modal-like functionality for focused user interactions.

## Configuration
- **Position**: Set to `Fit` for size-based positioning.
- **IsInteractiveCloseable**: Enable or disable swipe-to-close functionality.
- **Header Behavior**: Customize the header with commands and visibility options.

## Change start position.
The bottom sheets default position is half of the screen, this can be controlled by setting the `Position` property. To make it position using its size, set the `Position` property to `Fit`.

## Make sure people don't accidentally close it.
Use `IsInteractiveCloseable` to control whether people can close the bottom sheet by swiping it down (iOS and Android) or back button / back swipe (Android). The bottom sheet is still programatically closable. Use `this.Close()` or `BottomSheetService.Close(bottomSheet)` to close it. 

## Handling the back button on Android
When `IsInteractiveCloseable` is set to `false`, the back button will not close the bottom sheet on Android. However, you can bind a command to handle the back button press and execute custom logic before deciding whether to close the bottom sheet.

To achieve this, use the `OnBackButtonPressedCommand` property:

```xml
<dui:BottomSheet Title="The title"
                 OnBackButtonPressedCommand="{Binding TryCloseBottomSheetCommand}">
    
</dui:BottomSheet>
```

In your ViewModel, define the command to handle the back button press:

```csharp
public ICommand MyBackButtonCommand => new Command<Action>(async closeBottomSheet =>
{
    // Perform your custom logic here
    await ConfirmExitAsync();

    // Close the bottom sheet if needed
    closeBottomSheet.Invoke();
});
```

This allows you to control the behavior of the back button and decide whether to close the bottom sheet after executing your custom logic.

> An `Action` as `CommandParameter` is passed to the `Command` from the BottomSheet, allowing you to invoke it to close the bottom sheet programmatically after performing your custom logic.

## Header
The bottom sheet comes with a default header that includes a close button. You can set the title of the bottom sheet by using the `Title` property, which will be displayed in the header. Additionally, the header can include a back button if needed.

> To configure the header use `BottomSheetHeaderBehavior`.

### Close Button
The default close button in the header closes the bottom sheet by default. However, you can override this behavior by binding a command to the close button. For example, you can bind a command to perform an action (e.g., saving data) before closing the bottom sheet:

> An `Action` as `CommandParameter` is passed to the `Command` from the BottomSheet, allowing you to invoke it to close the bottom sheet programmatically after performing your custom logic.

```xml
<dui:BottomSheet Title="The title">
    
    <dui:BottomSheet.BottomSheetHeaderBehavior>
        <header:BottomSheetHeaderBehavior CloseButtonCommand="{Binding MyCloseCommand}" />
    </dui:BottomSheet.BottomSheetHeaderBehavior>
</dui:BottomSheet>
```

```csharp
public ICommand MyCloseCommand => new Command<Action>(async closeBottomSheet =>
{
    // Perform your custom logic here
    await SaveDataAsync();
    
    // Close the bottom sheet
    closeBottomSheet.Invoke();
});
```

### Back Button

To include a back button in the header, you can configure the `IsBackButtonVisible` property:

```xml
<dui:BottomSheet Title="The title">
    
    <dui:BottomSheet.BottomSheetHeaderBehavior>
        <header:BottomSheetHeaderBehavior IsBackButtonVisible="{Binding IsBackButtonVisible}" />
    </dui:BottomSheet.BottomSheetHeaderBehavior>
</dui:BottomSheet>
```

### Title and back button container
You can configure the container that contains the title and back button to execute a bound command by using the `TitleAndBackButtonCommand` property. This allows you to handle custom actions when interacting with the container.

```xml
<dui:BottomSheet Title="The title">
    
    <dui:BottomSheet.BottomSheetHeaderBehavior>
        <header:BottomSheetHeaderBehavior TitleAndBackButtonContainerCommand="{Binding TitleAndBackButtonContainerCommand}" />
    </dui:BottomSheet.BottomSheetHeaderBehavior>
</dui:BottomSheet>
```

In your ViewModel, define the command to handle the interaction:

```csharp
public ICommand MyTitleAndBackButtonCommand => new Command(() =>
{
    // Perform your custom logic here
    NavigateToAnotherPage();
});
```

#### Disabling the Command
To disable the container command, you can bind the `IsTitleAndBackButtonContainerEnabled` property to a boolean value in your ViewModel. When set to `false`, the container will be set to `IsEnabled = false`, thus, will not execute the `Command`.

```xml
<dui:BottomSheet Title="The title">
    
    <dui:BottomSheet.BottomSheetHeaderBehavior>
        <header:BottomSheetHeaderBehavior TitleAndBackButtonContainerCommand="{Binding TitleAndBackButtonContainerCommand}"
                                          IsTitleAndBackButtonContainerEnabled="{Binding IsContainerEnabled}" />
    </dui:BottomSheet.BottomSheetHeaderBehavior>
</dui:BottomSheet>
```

## BottombarButtons
The bottom sheet supports `BottombarButtons`, which can be used similarly to `ToolbarItems` in a regular page. These buttons are displayed floating at the bottom of the sheet. The buttons are displayed horizontally.

```xml
<dui:BottomSheet>
    <dui:BottomSheet.BottombarButtons>

        <dui:Button Text="Ghost Large"
                    Style="{dui:Styles Button=GhostLarge}"
                    VerticalOptions="End"/>

        <dui:Button Text="Primary Large"
                    HorizontalOptions="Fill"
                    VerticalOptions="End" />

    </dui:BottomSheet.BottombarButtons>
</dui:BottomSheet>
```
> Most likely you will want to set `VerticalOptions="End"` on the buttons, otherwise the they will fill out its vertical height to the container.

# Properties
Inspect the [components properties class](https://github.com/DIPSAS/DIPS.Mobile.UI/blob/main/src/library/DIPS.Mobile.UI/Components/BottomSheets/BottomSheet.Properties.cs) to further customise and use it.