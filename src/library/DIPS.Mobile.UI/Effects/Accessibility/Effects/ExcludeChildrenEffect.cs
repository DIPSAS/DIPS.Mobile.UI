using DIPS.Mobile.UI.Internal.Logging;

namespace DIPS.Mobile.UI.Effects.Accessibility.Effects;

internal class ExcludeChildrenEffect : Behavior
{
    protected override void OnAttachedTo(BindableObject bindable)
    {
        base.OnAttachedTo(bindable);

        if (bindable is not Microsoft.Maui.Controls.Layout layout)
        {
            DUILogService.LogError<GroupEffect>($"{bindable.GetType().Name} is not a Microsoft.Maui.Controls.Layout");
            return;
        }

        ExcludeChildren(layout);
        layout.ChildAdded += OnChildAdded;
    }

    private static void OnChildAdded(object? sender, ElementEventArgs e)
    {
        if (e.Element is VisualElement visualElement)
        {
            AutomationProperties.SetExcludedWithChildren(visualElement, true);
        }
    }

    private static void ExcludeChildren(Microsoft.Maui.Controls.Layout layout)
    {
        foreach (var child in layout.Children)
        {
            if (child is VisualElement visualElement)
            {
                AutomationProperties.SetExcludedWithChildren(visualElement, true);
            }
        }
    }

    protected override void OnDetachingFrom(BindableObject bindable)
    {
        base.OnDetachingFrom(bindable);
        
        if (bindable is Microsoft.Maui.Controls.Layout layout)
        {
            layout.ChildAdded -= OnChildAdded;
        }
    }
}