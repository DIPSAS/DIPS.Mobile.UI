using DIPS.Mobile.UI.Internal.Logging;

namespace DIPS.Mobile.UI.Effects.Accessibility.Effects;

internal class GroupEffect : Behavior
{
    private Microsoft.Maui.Controls.Layout? m_layout;

    protected override void OnAttachedTo(BindableObject bindable)
    {
        base.OnAttachedTo(bindable);
        
        if (bindable is not Microsoft.Maui.Controls.Layout layout)
        {
            DUILogService.LogError<GroupEffect>($"{bindable.GetType().Name} is not a Microsoft.Maui.Controls.Layout");
            return;
        }

        m_layout = layout;
        m_layout.HandlerChanged += OnHandlerChanged;
    }

    private void OnHandlerChanged(object? sender, EventArgs e)
    {
        if (m_layout?.Handler is null)
            return;

        UpdateGroupedDescription();
    }

    private void UpdateGroupedDescription()
    {
        if (m_layout == null)
            return;

        var combinedText = GetAllDescendants(m_layout)
            .Select(ExtractTextFromElement)
            .Where(text => !string.IsNullOrWhiteSpace(text))
            .ToList();

        var description = string.Join(", ", combinedText);
        SemanticProperties.SetDescription(m_layout, description);
    }

    private static IEnumerable<IView> GetAllDescendants(IView element)
    {
        if (element is not Microsoft.Maui.Controls.Layout layout)
            yield break;

        foreach (var child in layout.Children)
        {
            yield return child;
                
            foreach (var descendant in GetAllDescendants(child))
            {
                yield return descendant;
            }
        }
    }

    private static string? ExtractTextFromElement(IView element)
    {
        if (element is BindableObject bindableObject)
        {
            var semanticDescription = SemanticProperties.GetDescription(bindableObject);
            if (!string.IsNullOrEmpty(semanticDescription))
            {
                return semanticDescription;
            }
        }

        if (element is Label label)
        {
            return GetLabelText(label);
        }

        return string.Empty;
    }

    private static string? GetLabelText(Label label)
    {
        return label.FormattedText != null ? label.FormattedText.ToString() : label.Text;
    }

    protected override void OnDetachingFrom(BindableObject bindable)
    {
        base.OnDetachingFrom(bindable);

        if (m_layout is not null)
        {
            m_layout.HandlerChanged -= OnHandlerChanged;
        }

        m_layout = null;
    }
}
