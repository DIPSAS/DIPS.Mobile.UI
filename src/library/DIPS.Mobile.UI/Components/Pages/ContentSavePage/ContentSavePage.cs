using DIPS.Mobile.UI.Components.Saving.SaveView;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Components.Pages.ContentSavePage;

[ContentProperty(nameof(OriginalContent))]
public partial class ContentSavePage : ContentPage
{
    private readonly SaveView m_saveView;

    private readonly Grid m_contentGrid = new();

    public ContentSavePage()
    {
        m_saveView = new SaveView { IsVisible = false, BackgroundColor = Colors.GetColor(BackgroundColorName)};
        m_saveView.SetBinding(SaveView.IsSavingProperty, new Binding(nameof(IsSaving), source: this));
        m_saveView.SetBinding(SaveView.IsSavingCompletedProperty, new Binding(nameof(IsSavingCompleted), source: this));
        m_saveView.SetBinding(SaveView.SavingTextProperty, new Binding(nameof(SavingText), source: this));
        m_saveView.SetBinding(SaveView.SavingCompletedTextProperty, new Binding(nameof(SavingCompletedText), source: this));
        m_saveView.SetBinding(SaveView.SavingCompletedCommandProperty, new Binding(nameof(SavingCompletedCommand), source: this));

        m_contentGrid.Add(m_saveView);
        
        Content = m_contentGrid;
    }

    private Task GoToSaveView()
    {
        m_saveView.IsVisible = true;
        return Task.CompletedTask;
    }
    
    private Task GoBackToDefaultContent()
    {
        m_saveView.IsVisible = false;
        return Task.CompletedTask;
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
        
        m_contentGrid.Insert(0, OriginalContent);
    }
}