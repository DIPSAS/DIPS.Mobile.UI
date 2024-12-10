using DIPS.Mobile.UI.Components.Saving.SaveView;

namespace DIPS.Mobile.UI.Components.Pages.ContentSavePage;

public partial class ContentSavePage : ContentPage
{
    private readonly SaveView m_saveView;
    private View? OriginalContent { get; set; }

    public ContentSavePage()
    {
        m_saveView = new SaveView();
        m_saveView.SetBinding(SaveView.IsSavingProperty, static (ContentSavePage contentSavePage) => contentSavePage.IsSaving, source: this);
        m_saveView.SetBinding(SaveView.IsSavingCompletedProperty, static (ContentSavePage contentSavePage) => contentSavePage.IsSavingCompleted, source: this);
        m_saveView.SetBinding(SaveView.SavingTextProperty, static (ContentSavePage contentSavePage) => contentSavePage.SavingText, source: this);
        m_saveView.SetBinding(SaveView.SavingCompletedTextProperty, static (ContentSavePage contentSavePage) => contentSavePage.SavingCompletedText, source: this);
        m_saveView.SetBinding(SaveView.SavingCompletedCommandProperty, static (ContentSavePage contentSavePage) => contentSavePage.SavingCompletedCommand, source: this);
    }

    private async Task CreateSaveView()
    {
        await Content.FadeTo(0, 150, Easing.CubicInOut);

        m_saveView.Opacity = 0;

        Content = m_saveView;

        _ = m_saveView.FadeTo(1, easing: Easing.CubicInOut);
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