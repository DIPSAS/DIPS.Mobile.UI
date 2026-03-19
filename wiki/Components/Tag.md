The `Tag` component is used to display concise, contextual information such as labels, statuses, or categories within the user interface. Tags help users quickly identify and differentiate items at a glance.

We provide five different styles for the Tag component to suit various use cases:

- **Default**
- **Subtle**
- **Danger**
- **Success**
- **Warning**

Each style is designed to convey a specific meaning or level of emphasis, ensuring clear and effective communication in your application.

# Usage
Customization options include `Text` and `Icon`, they can also be custom colored or set the `Style` you prefer.

```xaml
<dui:Tag Style="{dui:Styles Tag=Danger}"
         Text="Danger"
         Icon="{dui:Icons alert_fill}" />
```