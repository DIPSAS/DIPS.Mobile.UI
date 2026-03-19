We have added a [ContentPage](https://github.com/DIPSAS/DIPS.Mobile.UI/blob/main/src/library/DIPS.Mobile.UI/Components/Pages/ContentPage.cs) that derives from MAUI's implementation of `ContentPage`. Currently its main purpose is to set color on the background, title and foreground of the page. Additionally, it also sets the `HideSoftInputOnTapped` to default true. 

> Follow this [link](https://github.com/DIPSAS/DIPS.Mobile.UI/wiki/HideSoftInputOnTapped) to enable our custom `HideSoftInputOnTapped` implementation on Android.

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
