using DIPS.Mobile.UI.Components.Saving.SaveView;

namespace DIPS.Mobile.UI.Components.Pages.ContentSavePage;

[ContentProperty(nameof(OriginalContent))]
public partial class ContentSavePage : ContentPage
{
    private readonly SaveView m_saveView;

    private readonly Grid m_contentGrid = new();

    public ContentSavePage()
    {
        m_saveView = new SaveView { IsVisible = false };
        m_saveView.SetBinding(SaveView.IsSavingProperty, new Binding(nameof(IsSaving), source: this));
        m_saveView.SetBinding(SaveView.IsSavingCompletedProperty, new Binding(nameof(IsSavingCompleted), source: this));
        m_saveView.SetBinding(SaveView.SavingTextProperty, new Binding(nameof(SavingText), source: this));
        m_saveView.SetBinding(SaveView.SavingCompletedTextProperty, new Binding(nameof(SavingCompletedText), source: this));
        m_saveView.SetBinding(SaveView.SavingCompletedCommandProperty, new Binding(nameof(SavingCompletedCommand), source: this));

        m_contentGrid.Add(m_saveView);
        
        Content = m_contentGrid;
    }

    private async Task GoToSaveView()
    {
        _ = OriginalContent.FadeTo(0, 150, Easing.CubicInOut);

        m_saveView.Opacity = 0;
        m_saveView.IsVisible = true;
        
        OriginalContent.IsVisible = false;
        
        Content = m_saveView;

        _ = m_saveView.FadeTo(1, easing: Easing.CubicInOut);
    }
    
    private async Task GoBackToDefaultContent()
    {
        _ = m_saveView.FadeTo(0, 150, Easing.CubicInOut);

        OriginalContent.IsVisible = true;
        m_saveView.IsVisible = false;
        
        Content = OriginalContent;

        _ = OriginalContent.FadeTo(1, easing: Easing.CubicInOut);
    }

    private static void IsSavingChanged(BindableObject bindableObject, object oldValue, object newValue)
    {
        if(bindableObject is not ContentSavePage contentPage)
            return;

        if (newValue is true)
        {
            _ = contentPage.GoToSaveView();
        }
        else
        {
            _ = contentPage.GoBackToDefaultContent();
        }
    }


    private void OnOriginalContentChanged()
    {
        if (m_contentGrid.Contains(OriginalContent))
            m_contentGrid.Remove(OriginalContent);
        
        m_contentGrid.Add(OriginalContent);
    }
}