using DIPS.Mobile.UI.Components.Saving.SaveView;

namespace DIPS.Mobile.UI.Components.Pages.ContentSavePage;

public partial class ContentSavePage : ContentPage
{
    private readonly SaveView m_saveView;
    private View? OriginalContent { get; set; }

    public ContentSavePage()
    {
        m_saveView = new SaveView();
        m_saveView.SetBinding(SaveView.IsSavingProperty, new Binding(nameof(IsSaving), source: this));
        m_saveView.SetBinding(SaveView.IsSavingCompletedProperty, new Binding(nameof(IsSavingCompleted), source: this));
        m_saveView.SetBinding(SaveView.SavingTextProperty, new Binding(nameof(SavingText), source: this));
        m_saveView.SetBinding(SaveView.SavingCompletedTextProperty, new Binding(nameof(SavingCompletedText), source: this));
        m_saveView.SetBinding(SaveView.SavingCompletedCommandProperty, new Binding(nameof(SavingCompletedCommand), source: this));
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