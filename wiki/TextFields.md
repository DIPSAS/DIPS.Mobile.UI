# Entry
An `Entry` allows you to enter a single line of text. We have derived from MAUI's implementation of `Entry`: https://learn.microsoft.com/en-us/dotnet/maui/user-interface/controls/entry.

## Usage
Here we place an Entry with no borders and also all the text will be focused upon focusing the `Entry`.
```xml
<dui:Entry Placeholder="Input text"
           HasBorder="False"
           ShouldSelectAllTextOnFocused="True" />
```

# Properties
Inspect the [components properties class](https://github.com/DIPSAS/DIPS.Mobile.UI/blob/main/src/library/DIPS.Mobile.UI/Components/TextFields/Entry/Entry.Properties.cs) to further customize and use it.

<br>

# Editor
An 'Editor' allows you to enter and edit multiple lines of text. Also here we have derived from MAUI's implementation of `Editor`: https://learn.microsoft.com/en-us/dotnet/maui/user-interface/controls/editor?view=net-maui-8.0

## Usage
Here we place an `Editor` with no borders and also all the text will be focused upon focusing the `Editor`.

>Setting HasBorder="False" has no effect on iOS, only Android. iOS has no borders on `Editor` by default.

```xml
<dui:Editor Placeholder="Test"
            HasBorder="False"
            ShouldSelectAllTextOnFocused="True">
```

# Properties
Inspect the [components properties class](https://github.com/DIPSAS/DIPS.Mobile.UI/blob/main/src/library/DIPS.Mobile.UI/Components/TextFields/Editor/Editor.Properties.cs) to further customize and use it.

<br>

# InputFields

## SingleLineInputField

`SingleLineInputField` is a UI component designed for single-line text input.

### Usage
```xml
<dui:SingleLineInputField HelpText="This is for help purposes"
                          HeaderText="This is a header" />
```

### Key Features
* <b>HeaderText:</b> Display a header text above the input field to provide additional context.
* <b>HelpText:</b> Include a help text below the input field for supplementary guidance.

# Properties
Inspect the [components properties class](https://github.com/DIPSAS/DIPS.Mobile.UI/blob/main/src/library/DIPS.Mobile.UI/Components/TextFields/InputFields/SingleLineInputField.Properties.cs) to further customize and use it.

<br>

## MultiLineInputField

`MultiLineInputField` is a UI component designed for multi-line text input, and inherits from [SingleLineInputField](#singlelineinputfield).

### Usage
```xml
<dui:MultiLineInputField HelpText="This is for help purposes"
                         HeaderText="This is a header"
                         IsSaving="True"
                         IsError="False"
                         IsSavingSuccess="True"
                         MaxLines="2">                       
```

### Key Features
* <b>Save Command:</b> Execute custom logic with the SaveCommand property, handling the save button tap.
* <b>Saving State:</b> Indicate the saving state with IsSaving and provide feedback with IsSavingSuccess.
* <b>Error Handling:</b> Handle errors by setting IsError and providing error text via the ErrorText property.
* <b>MaxLines:</b> Sets the maximum number of lines before the text is truncated. (<b>NB:</b> Is only truncated when it is not focused)

# Properties
Inspect the [components properties class](https://github.com/DIPSAS/DIPS.Mobile.UI/blob/main/src/library/DIPS.Mobile.UI/Components/TextFields/InputFields/MultiLineInputField/MultiLineInputField.Properties.cs) to further customize and use it.