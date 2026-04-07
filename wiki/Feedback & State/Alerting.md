Notifying people when you have information that (might) be relevant is important. Leaving people clueless can lead be very confusing and frustrating. We provide different APIs or views that you can use to make sure people are happy. 

# System Message
A system message is a message that is displayed at the top of the screen.

## Inspiration
[Android Developer - Toast](https://developer.android.com/guide/topics/ui/notifiers/toasts)

## Configuration
A system message can be configured when calling the function to display the system message, with the [`ISystemMessageConfigurator`](https://github.com/DIPSAS/DIPS.Mobile.UI/blob/main/src/library/DIPS.Mobile.UI/Components/Alerting/SystemMessage/ISystemMessageConfigurator.cs) interface.

## Usage
In the following example a system message with the text: "A system message" and a duration of 2 seconds is displayed.

```csharp
SystemMessageService.Display(config =>
        {
            config.Text = "A system message";
            config.Duration = 2000;
        });
```

# Dialogs
Dialogs presents critical information that users needs right away. There are three different dialogs: 
* _Regular_. A dialog with a title, description and a single button.
* _Confirmation_. A dialog with a title, description, a cancel button and a confirmation button.
* _Destructive_. A dialog with a title, description, a cancel button and a destructive button.

## Inspiration
[Material Design 2 - Dialogs](https://developer.android.com/guide/topics/ui/notifiers/toasts)

[iOS Developer - Alerts](https://developer.apple.com/design/human-interface-guidelines/alerts)

## Usage
In the following example a _Regular_ dialog is presented with title, description and action title set. The function will return a `DialogAction` that either tells the system that the user cancelled the dialog or tapped the action button.

```csharp
 var result = await DialogService.ShowMessage("Title", 
                "Description", 
                "OK");
```

## API
Inspect the [DialogService class](https://github.com/DIPSAS/DIPS.Mobile.UI/blob/main/src/library/DIPS.Mobile.UI/Components/Alerting/Dialog/DialogService.cs) to examine how it is used.


# AlertView
Alerts can be used when you need to display information in line of your apps page to people. There are different types of styles:
- Information
- Warning
- Success
- Error

You need to provide a good title along with an optional description.

> You can optionally display a left / right button that people can tap.

## Usage

```xml

<dui:AlertView Title="Informing title"
            Description="This is a description that will provide you with information."
            Style="{dui:Styles Alert=Information}"
            LeftButtonText="{Binding ButtonText}"
            LeftButtonCommand="{Binding Command}"
            LeftButtonCommandParameter="Here's the info!" />

...
<dui:AlertView Title="Something went wrong"
            Description="Something terribly wrong happened."
            Style="{dui:Styles Alert=Error}" />


...
<dui:AlertView Title="We warn you"
            Description="Dont do it...okay do it. No, dont!"
            Style="{dui:Styles Alert=Warning}" />

...
<dui:AlertView Title="Yey"
            Description="Good job! You did it!"
            Style="{dui:Styles Alert=Success}" />
```

## Title Truncation
When only a title is set (no description), the maximum number of lines for the title depends on the `TitleTruncationMode` property:

```xml
<dui:AlertView Title="A very long title that might need to be truncated"
               TitleTruncationMode="Aggressive" />
```

The `TitleTruncationMode` can be set to:
- **Aggressive**: Title is limited to 1 line
- **Moderate**: Title can span up to 2 lines

## Large vs Small AlertView
The AlertView has two different layout modes depending on whether a description is provided:

### Small AlertView (Title only)
- Maximum 1-2 lines for title (based on `TitleTruncationMode`)
- Icon is centered vertically with the text
- Buttons appear inline with the content (If there is space, otherwise underlying)

### Large AlertView (Title + Description)
- Maximum 4 lines total for all text content
- Icon is positioned at the top-left
- Buttons are always positioned below the text content

```xml
<!-- Small AlertView -->
<dui:AlertView Title="Short alert"
               Style="{dui:Styles Alert=Information}" />

<!-- Large AlertView -->
<dui:AlertView Title="Detailed alert"
               Description="This creates a large AlertView with different layout behavior."
               Style="{dui:Styles Alert=Information}" />
```

## Text Truncation and Expansion
When text content exceeds the maximum line limits, the AlertView will display a "...more" indicator. Users can tap on the truncated text to view the complete content in a bottomsheet.

This behavior applies to:
- Title text when it exceeds the `TitleTruncationMode` limits in Small AlertView
- Combined title and description text when it exceeds 4 lines in Large AlertView

```xml
<dui:AlertView Title="This is a very long title that will be truncated and show a more indicator"
               Description="Along with a description that makes the total content exceed the 4-line limit, triggering the truncation behavior."
               Style="{dui:Styles Alert=Information}" />
```

## API
Inspect the [AlertView properties class](https://github.com/DIPSAS/DIPS.Mobile.UI/blob/main/src/library/DIPS.Mobile.UI/Components/Alerting/Alert/AlertView.Properties.cs) to examine how it is used.