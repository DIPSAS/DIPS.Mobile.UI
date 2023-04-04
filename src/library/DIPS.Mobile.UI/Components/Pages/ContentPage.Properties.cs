namespace DIPS.Mobile.UI.Components.Pages
{
    public partial class ContentPage
    {
        /// <summary>
        /// Event that will be invoked when the content will appear.
        /// </summary>
        /// /// <remarks>This will get invoked after the pages Appearing event, when the content is set, but the content is not visible yet.</remarks>
        public event EventHandler? ContentAppearing;
    
        /// <summary>
        /// Method that will be invoked when the content will appear. 
        /// </summary>
        /// <remarks>This will get invoked after the pages Appearing event, when the content is set, but the content is not visible yet.</remarks>
        protected virtual void OnContentAppearing(){}

        /// <summary>
        /// Method that will be invoked when the iOS Safe Area Insets did change.
        /// </summary>
        /// <param name="thickness">The <see cref="Thickness"/> of the Safe Area Insets</param>
        protected virtual void SafeAreaInsetsDidChange(Thickness thickness) { }
    }   
}