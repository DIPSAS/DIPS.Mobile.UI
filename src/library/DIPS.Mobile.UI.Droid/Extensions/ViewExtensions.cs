using System.Collections.Generic;
using System.Collections.ObjectModel;
using Android.Graphics.Drawables;
using Android.Views;
using Java.Lang;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using View = Android.Views.View;

namespace DIPS.Mobile.UI.Droid.Extensions
{
    public static class ViewExtensions
    {
        public static T? FindChildView<T>(this ViewGroup? viewGroup) where T : View
        {
            if (viewGroup is T view)
            {
                return view;
            }

            while (viewGroup != null)
            {
                for (var i = 0; i < viewGroup.ChildCount; i++)
                {
                    var childView = viewGroup.GetChildAt(i);
                    if (childView is T correctView)
                    {
                        return correctView;
                    }

                    if(childView is ViewGroup newViewGroup)
                    {
                        viewGroup = newViewGroup;
                    }
                }
            }

            return null;
        }
        
        
        public static T? FindParentView<T>(this View? view)
            where T : class
        {
            if (view is T t)
                return t;

            while (view != null)
            {
                if (view.Parent is T parent)
                    return parent;
                if (view.Parent is View viewParent)
                    view = viewParent;
            }

            return null;
        }

        public static void SetRoundedRectangularBackground(this View view, double cornerRadius, Color color)
        {
            var shape = new GradientDrawable();
            shape.SetShape(ShapeType.Rectangle);
            shape.SetColor(color.ToAndroid());
            shape.SetCornerRadius((float)cornerRadius);
            view.SetBackground(shape);
            if (view.Parent is View)
            {
                BlankBackGroundOnAllParents((View)view.Parent);
            }
        }

        private static void BlankBackGroundOnAllParents(View view)
        {
            view.SetBackgroundColor(Color.Transparent.ToAndroid());
            if (view.Parent is View parent)
            {
                BlankBackGroundOnAllParents(parent);
            }
        }
        
        public static string? GetViewHierarchy(this View view) {
            var desc = new StringBuilder();
            GetViewHierarchy(view, desc, 0);
            return desc.ToString();
        }

        private static void GetViewHierarchy(View v, StringBuilder desc, int margin) {
            desc.Append(GetViewMessage(v, margin));
            if (v is not ViewGroup vg)
            {
                return;
            }

            margin++;
            for (var i = 0; i < vg.ChildCount; i++)
            {
                var child = vg.GetChildAt(i);
                if (child == null) continue;
                GetViewHierarchy(child, desc, margin);
            }
        }

        public static ICollection<View> GetFlatViewHierarchyCollection(this View view)
        {
            var collection = new List<View>();
            view.AddFlatViewHierarchyToCollection(collection);
            return collection;
        }
        private static void AddFlatViewHierarchyToCollection(this View view, ICollection<View> views) {
            views.Add(view);
            if (view is not ViewGroup vg)
            {
                return;
            }
            
            for (var i = 0; i < vg.ChildCount; i++)
            {
                var child = vg.GetChildAt(i);
                if (child == null) continue;
                child.AddFlatViewHierarchyToCollection(views);
            }
        }

        public static string? GetResourceNameFromView(this View v)
        {
            if (v.Resources != null)
            {
                if (v.Id > 0)
                {
                    var resourceName = v.Resources.GetResourceName(v.Id);
                    if (resourceName != null)
                    {
                        return resourceName;
                    }
                }
            }

            return null;
        }

        private static string GetViewMessage(View v, int marginOffset) {
            var repeated = new String(new char[marginOffset]).Replace("\0", "  ");
            try {
                var resourceId = v.Resources != null ? (v.Id > 0 ? v.Resources.GetResourceName(v.Id) : "no_id") : "no_resources";
                return $"{repeated}[{v.Class.SimpleName}]{resourceId} ({v.Id}) \n";
            } catch (Android.Content.Res.Resources.NotFoundException e) {
                return repeated + "[" + v.Class.SimpleName + "] name_not_found\n";
            }
        }
    }
}