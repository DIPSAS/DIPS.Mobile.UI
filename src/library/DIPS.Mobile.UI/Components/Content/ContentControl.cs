namespace DIPS.Mobile.UI.Components.Content
{
    /// <summary>
    /// Controls what content to display based on a <see cref="SelectorItem"/> with a <see cref="TemplateSelector"/>
    /// </summary>
    public partial class ContentControl : ContentView
    {
        /// <summary>
        /// Creates an instance to use a DataTemplateSelector
        /// </summary>
        public ContentControl()
        {
            BindingContextChanged += (s, e) => UpdateContent();
        }

        private void UpdateContent()
        {
            if(BindingContext == null || TemplateSelector == null)
            {
                return;
            }
            
            Content = TemplateSelector.SelectTemplate(SelectorItem ?? BindingContext, this).CreateContent() as View;
        }
    }
}
