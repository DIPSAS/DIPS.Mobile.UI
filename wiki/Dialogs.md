 Dialogs are used to interact with users, gather information, or display important messages. They help in guiding users through tasks, confirming actions, and providing feedback. 

 # DialogService
In this library, a `DialogService` is provided to easily display such a dialog.

## MessageDialog
A message dialog is a type of user interface element that displays a message to the user. It typically appears as a small window or pop-up that contains text and buttons for user interaction. 

Here are some common characteristics of message dialogs:

* Title: A brief heading that indicates the purpose of the dialog.
* Message: The main content, which provides information or instructions to the user.
* Buttons: Options for the user to respond, such as "OK", "Cancel", "Yes", "No".

Example:

```csharp
DialogService.ShowMessage(config =>
{
    config.SetTitle("Test");
    config.SetDescription("Testing");
    config.SetActionTitle("Ok");
}
```

### InputDialog
An Input Dialog extends the MessageDialog, allowing you to implement input fields into the message dialog.

```csharp
var response = DialogService.ShowInputDialog(config =>
        {
            config.SetTitle("Test");
            config.SetDescription("Testing");
            config.AddInputField(new StringDialogInputField("Placeholder"));
        });
```

To check the users' input after pressing the action button:

```csharp
var response = await DialogService.ShowInputDialog(config =>
{
    config.SetTitle("Test");
    config.SetDescription("Testing");
    config.AddInputField(new StringDialogInputField("Placeholder"));
});

if(response.DialogInputs.First() is StringDialogInputField inputField)
{
    var value = inputField.Value;
    // Do something with value
}
```

or

```csharp
var inputField = new StringDialogInputField("Placeholder");
var response = await DialogService.ShowInputDialog(config =>
{
    config.SetTitle("Test");
    config.SetDescription("Testing");
    config.AddInputField(inputField);
});

var value = inputField.Value;
// Do something with the value
```

Head on over to [DialogService](https://github.com/DIPSAS/DIPS.Mobile.UI/blob/main/src/library/DIPS.Mobile.UI/Components/Alerting/Dialog/DialogService.cs) for futher inspection.