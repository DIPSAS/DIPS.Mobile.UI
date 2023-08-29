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
            
            var theContent = TemplateSelector.SelectTemplate(SelectorItem ?? BindingContext, this).CreateContent() as View;
#if __IOS__
            if (theContent is CollectionView)
            {
                //TODO: Fix Dotnet 8. : UICollectionViews inside a ContentView messes up the height, the fix is to wrap it in a grid with row=auto
                var grid = new Grid() {RowDefinitions = new RowDefinitionCollection() {new(GridLength.Auto)}};
                grid.Add(theContent);
                theContent = grid;
            } 
#endif
           

            Content = theContent;
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            UpdateContent();
        }
    }
}
