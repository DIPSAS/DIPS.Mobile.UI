using DIPS.Mobile.UI.Internal.Logging;

namespace DIPS.Mobile.UI.Components.Content
{
    /// <summary>
    /// Controls what content to display based on a <see cref="SelectorItem"/> with a <see cref="TemplateSelector"/>
    /// </summary>
    public partial class ContentControl : ContentView
    {
        private void UpdateContent()
        {
            if(BindingContext == null || TemplateSelector == null)
            {
                return;
            }

            if (BindingContext == SelectorItem && Content is not null)
            {
                DUILogService.LogError<ContentControl>("SelectorItem and BindingContext is the same, don't use SelectorItem as BindingContext. This can cause ContentControl to display the wrong content.");
                return;
            }
            
            Content?.DisconnectHandlers();
            Content = TemplateSelector.SelectTemplate(SelectorItem ?? BindingContext, this).CreateContent() as View;;
            
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            UpdateContent();
        }
    }
}
