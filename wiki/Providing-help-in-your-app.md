You should aim towards creating approachable and intuitive user experience, but sometimes you can provide help.

# Tip

To provide a tip, use the `TipService` API. The tip will be anchored with a view, which makes people understand what functionality they should use. Tips are a great way to teach people about new or less obvious features in your app, or help them discover faster ways to accomplish a task.

## Usage


```csharp
TipService.Show("This is my amazing feature, please use it.", MyButton);
```

> MyButton is a button in a XAML page where I want my tip to be presented with


# Tooltip
To provide a tooltip, combine the `Touch` API with a `TipCommand`.

## Usage
```xml
<dui:Image Source="{dui:Icons arrow_up_thick_line_fill}"
           dui:Touch.Command="{dui:TipCommand 'This means that an value is going up.'}" />
```