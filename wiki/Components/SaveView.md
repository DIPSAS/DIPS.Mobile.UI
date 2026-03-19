`SaveView` is used when something is currently saving. Usually consumers replaces the content of the whole page to only display the `SaveView`. `SaveView` uses [FilledCheckBox](https://github.com/DIPSAS/DIPS.Mobile.UI/wiki/Checkboxes) to notify users when the saving is processing and when it is completed.

# Usage
In this example the `SaveView` will start the saving animation and display the `SavingText` when the `IsSaving` property is `true`. When the boolean transforms to `false`, the `SavingCompletedText` will be displayed and the [FilledCheckBox](https://github.com/DIPSAS/DIPS.Mobile.UI/wiki/Checkboxes) will be filled up.

```xml
   <dui:SaveView SavingText="Saving..."
                 SavingCompletedText="Saved!"
                 IsSaving="{Binding IsSaving}" />
```