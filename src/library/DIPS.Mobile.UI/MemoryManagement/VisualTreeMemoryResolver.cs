namespace DIPS.Mobile.UI.MemoryManagement;

internal class VisualTreeMemoryResolver
{
    private Action<object>? m_additionalResolver;

    [Obsolete("Use DisconnectHandlers() instead")]
    internal void TryResolveMemoryLeakCascading(object? target)
    {
        if(target is null)
            return;
        
        if (target is IVisualTreeElement visualTreeElement)
        {
            foreach (var child in visualTreeElement.GetVisualChildren())
            {
                TryResolveMemoryLeakCascading(child);
            }
        }
        
        TryResolveMemoryLeak(target);
    }
    
    [Obsolete("Use DisconnectHandler() instead")]
    internal void TryResolveMemoryLeak(object target)
    {
        try
        {
            switch (target)
            {
                case VisualElement visualElement:
                    {
                        visualElement.Effects.Clear();

                        visualElement.BindingContext = null;
                        visualElement.Parent = null;

                        switch (visualElement)
                        {
                            case ContentView contentView:
                                contentView.Content = null;
                                break;
                            case CollectionView collectionView:
                                if (!collectionView.IsGrouped)
                                {
                                    collectionView.FooterTemplate = null;
                                    collectionView.HeaderTemplate = null;
                                    collectionView.Footer = null;
                                    collectionView.Header = null;
                                    collectionView.EmptyView = null;
                                }
                                break;
                            case Border border:
                                border.Content = null;
                                break;
                            case ContentPage contentPage:
                                contentPage.Content = null;
                                break;
                            case ScrollView scrollView:
                                scrollView.Content = null;
                                break;
                        }

                        // CollectionView, CarouselView etc..
                        if (visualElement is ItemsView itemsView)
                        {
                            itemsView.ItemsSource = null;
                            itemsView.ItemTemplate = null;
                        }
                        
                        visualElement.ClearLogicalChildren();

                        if (visualElement.Handler is not null)
                        {
                            if (visualElement.Handler is IDisposable disposableHandler)
                                disposableHandler.Dispose();
                            visualElement.Handler?.DisconnectHandler();
                            visualElement.Handler = null;
                        }

                        visualElement.Resources = null;
                        break;
                    }
                case Element element:
                    {
                        element.Effects.Clear();

                        element.BindingContext = null;
                        element.Parent = null;

                        element.ClearLogicalChildren();

                        if (element.Handler is not null)
                        {
                            if (element.Handler is IDisposable disposableElementHandler)
                                disposableElementHandler.Dispose();
                            element.Handler?.DisconnectHandler();
                        }

                        break;
                    }
            }
            
            // Try run user-defined resolver method
            m_additionalResolver?.Invoke(target);
        }
        catch(Exception e)
        {
            // Should never crash the app
        }
    }

    internal void SetAdditionalResolver(Action<object> additionalResolver)
    {
        m_additionalResolver = additionalResolver;
    }
}