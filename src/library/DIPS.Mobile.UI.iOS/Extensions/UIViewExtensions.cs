using System;
using System.Collections.Generic;
using System.Linq;
using CoreAnimation;
using CoreGraphics;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

namespace DIPS.Mobile.UI.iOS.Extensions
{
    public static class UIViewExtensions
    {
        //Inspired by pancake view: https://github.com/sthewissen/Xamarin.Forms.PancakeView/blob/8dea1291034ef3e04a8c251c62d58b25630022c8/src/Xamarin.Forms.PancakeView.Multi/Platforms/iOS/PancakeViewRenderer.cs#L142
        public static void AddCornerRadius(this UIView uiView, CornerRadius cornerRadius, Color colorToRender)
        {
            var layerName = "backgroundLayer";

            // Remove previous background layer if any
            var prevBackgroundLayer = uiView.Layer.Sublayers?.FirstOrDefault(x => x.Name == layerName);
            prevBackgroundLayer?.RemoveFromSuperLayer();

            var rect = uiView.Bounds;
            var cornerPath = rect.CreateRoundedRectPath(cornerRadius);
            var shape = new CAShapeLayer() {Frame = rect, Path = cornerPath.CGPath};

            uiView.Layer.Mask = shape;
            uiView.Layer.MasksToBounds = true;
            var shapeLayer = new CAShapeLayer
            {
                Frame = rect,
                Path = cornerPath.CGPath,
                MasksToBounds = true,
                FillColor = colorToRender.ToCGColor(),
                Name = layerName
            };
            AddLayer(shapeLayer, 0, uiView);
        }

        public static void AddLayer(CALayer layer, int position, UIView viewToAddTo)
        {
            // If there is already a layer with the given name, remove it before inserting.
            if (layer != null)
            {
                // There's no background layer yet, insert it.
                if (position > -1)
                    viewToAddTo.Layer.InsertSublayer(layer, position);
                else
                    viewToAddTo.Layer.AddSublayer(layer);
            }
        }

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

        public static string PrintAllChildrenOfView(this UIView view, int depth = 0)
        {
            var tabs = "";
            for (int i = 0; i < depth; i++)
            {
                tabs += "\t";
            }

            var returnString = $"\n{tabs}{view.Class.Name}";

            depth++;
            foreach (var subView in view.Subviews)
            {
                returnString += PrintAllChildrenOfView(subView, depth);
            }

            return returnString;
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