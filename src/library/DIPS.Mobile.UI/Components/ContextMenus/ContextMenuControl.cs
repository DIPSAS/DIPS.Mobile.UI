using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DIPS.Mobile.UI.Components.ContextMenus
{
    /// <summary>
    /// Creates a control with a content that has context menu items attached to it
    /// </summary>
    [ContentProperty(nameof(TheContent))]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContextMenuControl : ContentView, IContextMenu
    {
        /// <summary/>
        public ContextMenuControl()
        {
            m_contextMenuButton = new ContextMenuButton {Parent = this};
            m_contextMenuButton.SetBinding(ContextMenuButton.ContextMenuHorizontalOptionsProperty,
                new Binding(nameof(ContextMenuHorizontalOptions), source: this));
            m_contextMenuButton.SetBinding(ContextMenuButton.TitleProperty,
                new Binding(nameof(Title), source: this));
            m_contextMenuButton.SetBinding(ContextMenuButton.ItemsSourceProperty,
                new Binding(nameof(ItemsSource), source: this));
            m_contextMenuButton.SetBinding(ContextMenuButton.ItemClickedCommandProperty,
                new Binding(nameof(ItemClickedCommand), source: this));
            m_contextMenuButton.DidOpenContextMenu += ContextMenuButton_OnContextMenuOpened;
            m_contextMenuButton.DidClickItem += ContextMenuButton_OnItemClicked;
            m_contextMenuButton.BackgroundColor = Color.Transparent;

            m_theContentView = new ContentView();
            m_theContentView.SetBinding(ContentProperty, new Binding(nameof(TheContent), source: this));
            m_theContentView.InputTransparent = true;
            Content = new Grid() {Children = {m_contextMenuButton, m_theContentView}};
        }

        private void ContextMenuButton_OnContextMenuOpened(object sender, EventArgs e)
        {
            DidOpenContextMenu?.Invoke(sender, e);
        }

        private void ContextMenuButton_OnItemClicked(object sender, EventArgs e)
        {
            DidClickItem?.Invoke(sender, e);
        }
    }
}