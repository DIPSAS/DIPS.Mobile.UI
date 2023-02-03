using System.Collections.Generic;
using UIKit;

namespace DIPS.Mobile.UI.iOS.Extensions
{
    public static class UIViewExtensions
    {
        public static T? FindChildView<T>(this UIView view) where T : UIView
        {
            var queue = new Queue<UIView>();
            queue.Enqueue(view);

            while (queue.Count > 0)
            {
                var descendantView = queue.Dequeue();

                if (descendantView is T result)
                    return result;

                for (var i = 0; i < descendantView.Subviews?.Length; i++)
                    queue.Enqueue(descendantView.Subviews[i]);
            }

            return null;
        }
        
        public static T? FindParentView<T>(this UIView? view)
            where T : class
        {
            if (view is T t)
                return t;

            while (view != null)
            {
                if (view.Superview is T parent)
                    return parent;

                view = view.Superview;
            }

            return null;
        }
    }
}