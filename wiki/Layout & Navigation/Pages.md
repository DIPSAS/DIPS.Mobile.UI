We have added a [ContentPage](https://github.com/DIPSAS/DIPS.Mobile.UI/blob/main/src/library/DIPS.Mobile.UI/Components/Pages/ContentPage.cs) that derives from MAUI's implementation of `ContentPage`. Currently its main purpose is to set color on the background, title and foreground of the page. Additionally, it also sets the `HideSoftInputOnTapped` to default true. 

> Follow this [link](https://github.com/DIPSAS/DIPS.Mobile.UI/wiki/HideSoftInputOnTapped) to enable our custom `HideSoftInputOnTapped` implementation on Android.

# Modal navigation bar color

`ContentPage` applies navigation bar colors when it is shown inside a modal `NavigationPage`. Set `NavigationBarColor` to override the background color for a specific page, and `NavigationBarTextColor` to override the title, back button, and toolbar item color. When they are unset, the modal navigation bar uses the parent `NavigationPage` colors, then the Shell navigation bar colors.

On iOS, the status bar content mode is explicitly resolved from `NavigationBarColor`, so light modal navigation bars use dark status bar text and dark modal navigation bars use light status bar text.

```xml
<dui:ContentPage NavigationBarColor="{dui:Colors color_surface_default}"
                 NavigationBarTextColor="{dui:Colors color_text_default}">
    <!-- Page content -->
</dui:ContentPage>
```

# ContentSavePage
Additionally, we have made an extension to the `ContentPage`, to make it easier to use [SaveView](https://github.com/DIPSAS/DIPS.Mobile.UI/wiki/SaveView). It replaces the content of the page with a [SaveView](https://github.com/DIPSAS/DIPS.Mobile.UI/wiki/SaveView), when something is to be saved.

## Usage 
In this example, the `Label` will be replaced with a [SaveView](https://github.com/DIPSAS/DIPS.Mobile.UI/wiki/SaveView) when the `IsSaving` property is `true`.

```xml

<dui:ContentSavePage xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
                     xmlns:dui="http://dips.com/mobile.ui"
                     x:Class="Components.ComponentsSamples.Saving.SaveViewSamples"
                     IsSavingCompleted="{Binding IsChecked}"
                     IsSaving="{Binding IsProgressing}"
                     SavingCompletedCommand="{Binding CompletedCommand}"
                     SavingCompletedText="Completed!"
                     SavingText="Loading...">

    <dui:Label Text="Hello" />

</dui:ContentSavePage>

```
