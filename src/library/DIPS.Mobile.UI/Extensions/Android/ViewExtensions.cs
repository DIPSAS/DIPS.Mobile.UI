using Android.Graphics.Drawables;
using Android.Views;
using Microsoft.Maui.Controls.Platform;
using Microsoft.Maui.Platform;
using View = Android.Views.View;

namespace DIPS.Mobile.UI.Extensions.Android;

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

                if (childView is ViewGroup newViewGroup)
                {
                    viewGroup = newViewGroup;
                }
            }
        }

        return null;
    }
    
    internal static void SetRoundedRectangularBackground(this View view, CornerRadius cornerRadius, Color color)
    {
        var shape = new GradientDrawable();
        shape.SetShape(ShapeType.Rectangle);
        shape.SetColor(color.ToPlatform());

        ((GradientDrawable)shape.Mutate()).SetCornerRadii(new[]
        {
            (float)cornerRadius.TopLeft, (float)cornerRadius.TopLeft, (float)cornerRadius.TopRight,
            (float)cornerRadius.TopRight, (float)cornerRadius.BottomRight, (float)cornerRadius.BottomRight,
            (float)cornerRadius.BottomLeft, (float)cornerRadius.BottomLeft
        });

        if (view.Background is RippleDrawable rippleDrawable)
        {
            rippleDrawable.SetDrawable(0, shape);
        }
        else
        {
            view.SetBackground(shape);
        }
        if (view.Parent is View parent)
        {
            BlankBackgroundOnAllParents(parent);
        }
    }
    
    private static void BlankBackgroundOnAllParents(View view)
    {
        view.SetBackgroundColor(Colors.Transparent.ToPlatform());
        if (view.Parent is View parent)
        {
            BlankBackgroundOnAllParents(parent);
        }
    }
    
    public static ICollection<View> GetFlatViewHierarchyCollection(this View view)
    {
        var collection = new List<View>();
        view.AddFlatViewHierarchyToCollection(collection);
        return collection;
    }

    private static void AddFlatViewHierarchyToCollection(this View view, ICollection<View> views)
    {
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
}