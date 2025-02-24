using Android.Graphics.Drawables;
using Android.Views;
using DIPS.Mobile.UI.API.Library;
using Microsoft.Maui.Controls.Platform;
using Microsoft.Maui.Platform;
using AView = Android.Views.View;
using ARect = Android.Graphics.Rect;
using View = Microsoft.Maui.Controls.View;


namespace DIPS.Mobile.UI.Extensions.Android;

public static class ViewExtensions
{
    
    public static T? FindChildView<T>(this ViewGroup? viewGroup) where T : AView
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
    
    internal static void SetRoundedRectangularBackground(this AView view, CornerRadius cornerRadius, Color color)
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
        if (view.Parent is AView parent)
        {
            BlankBackgroundOnAllParents(parent);
        }
    }
    
    private static void BlankBackgroundOnAllParents(AView view)
    {
        view.SetBackgroundColor(Microsoft.Maui.Graphics.Colors.Transparent.ToPlatform());
        if (view.Parent is AView parent)
        {
            BlankBackgroundOnAllParents(parent);
        }
    }
    
    public static ICollection<AView> GetFlatViewHierarchyCollection(this AView view)
    {
        var collection = new List<AView>();
        view.AddFlatViewHierarchyToCollection(collection);
        return collection;
    }

    private static void AddFlatViewHierarchyToCollection(this AView view, ICollection<AView> views)
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

    public static void SetAdditionalHitBoxSize(this AView aView, Thickness additionalHitBoxSize)
    {
        if(additionalHitBoxSize == Thickness.Zero)
        {
            return;
        }
        
        var rect = new ARect();
        aView.GetHitRect(rect);

        if(rect.IsEmpty)
        {
            return;
        }
        
        rect.Top -= (int)additionalHitBoxSize.Top.ToMauiPixel();
        rect.Left -= (int)additionalHitBoxSize.Left.ToMauiPixel();
        rect.Bottom += (int)additionalHitBoxSize.Bottom.ToMauiPixel();
        rect.Right += (int)additionalHitBoxSize.Right.ToMauiPixel();
        
        if (aView.Parent is AView parentView)
        {
            parentView.TouchDelegate = new TouchDelegate(rect, aView);
        }
    }
}