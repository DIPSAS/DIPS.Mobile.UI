Counters are simple badge-like components that can be used to display an integer number. They can for example be used to let the user quickly see the amount of content in a menu item.

# Usage
The display value of the counter can be set with the `Value` property. Optionally, a secondary counter can be shown by setting `Mode=Double`. The secondary counter can be set with the `SecondaryValue` property. An Urgent style can also be set for each counter by setting `IsUrgent` or `IsSecondaryUrgent` to `True`.

For improved accessibility, you can provide semantic descriptions for the values using the `ValueSemanticDescription` and `SecondaryValueSemanticDescription` properties. These descriptions are used to convey the meaning of the counter values.

```xml
<dui:ListItem Title="Counter">
    <dui:Counter Value="5"
                 SecondaryValue="2"
                 IsSecondaryUrgent="True"
                 ValueSemanticDescription="Unread messages"
                 SecondaryValueSemanticDescription="Mentions"/>
</dui:ListItem>
```

# Auto-Display
You can set the secondary counter to only be displayed when `SecondaryValue` is not equal to zero. This can be done by setting `Mode=Auto`.