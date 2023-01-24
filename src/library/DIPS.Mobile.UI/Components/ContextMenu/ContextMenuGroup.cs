using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace DIPS.Mobile.UI.Components.ContextMenu
{
    /// <summary>
    /// A context menu group with multiple items
    /// </summary>
    [ContentProperty(nameof(ItemsSource))]
    
    public partial class ContextMenuGroup : ContextMenuItem
    {
        
        /// <summary>
        /// <inheritdoc cref="BindableObject"/>
        /// </summary>
        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            ItemsSource.ForEach(c => c.BindingContext = BindingContext);
        }
    }
}