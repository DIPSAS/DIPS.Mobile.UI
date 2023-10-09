using DIPS.Mobile.UI.Components.Saving.SaveView;

namespace DIPS.Mobile.UI.Components.Pages.ContentSavePage;

public partial class ContentSavePage : ContentPage
{
    private View? OriginalContent { get; set; }

    private async Task CreateSaveView()
    {
        await Content.FadeTo(0, 150, Easing.CubicInOut);

        var saveView = new SaveView { Opacity = 0 };
        saveView.SetBinding(SaveView.IsSavingProperty, new Binding(nameof(IsSaving), source: this));
        saveView.SetBinding(SaveView.IsSavingCompletedProperty, new Binding(nameof(IsSavingCompleted), source: this));
        saveView.SetBinding(SaveView.SavingTextProperty, new Binding(nameof(SavingText), source: this));
        saveView.SetBinding(SaveView.SavingCompletedTextProperty, new Binding(nameof(SavingCompletedText), source: this));
        saveView.SetBinding(SaveView.SavingCompletedCommandProperty, new Binding(nameof(SavingCompletedCommand), source: this));

        Content = saveView;

        _ = saveView.FadeTo(1, easing: Easing.CubicInOut);
    }
    
    private async Task GoBackToDefaultContent()
    {
        await Content.FadeTo(0, 150, Easing.CubicInOut);

        Content = OriginalContent;

        _ = OriginalContent!.FadeTo(1, easing: Easing.CubicInOut);
    }

    private static void IsSavingChanged(BindableObject bindableObject, object oldValue, object newValue)
    {
        if(bindableObject is not ContentSavePage contentPage)
            return;

        if (newValue is true)
        {
            contentPage.OriginalContent = contentPage.Content;
            _ = contentPage.CreateSaveView();
        }
        else if (contentPage.OriginalContent is not null)
        {
            _ = contentPage.GoBackToDefaultContent();
        }
    }
}