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

            var selectorItem = IsSet(SelectorItemProperty) ? SelectorItem : BindingContext;
            if (selectorItem is null)
            {
                return;
            }

            if (BindingContext == selectorItem && Content is not null)
            {
                DUILogService.LogError<ContentControl>("SelectorItem and BindingContext is the same, don't use SelectorItem as BindingContext. This can cause ContentControl to display the wrong content.");
                return;
            }
            
            Content?.DisconnectHandlers();
            Content = TemplateSelector.SelectTemplate(selectorItem, this).CreateContent() as View;
            
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            UpdateContent();
        }
    }
}
