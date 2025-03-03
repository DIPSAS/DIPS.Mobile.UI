namespace DIPS.Mobile.UI.Effects.ListElement;

public partial class FirstLastElementCornerRadiusPlatformEffect
{
    protected override partial void OnAttached()
    {
        var list = FirstLastElementCornerRadiusEffect.GetList(Element);
        var cornerRadius = FirstLastElementCornerRadiusEffect.GetCornerRadius(Element);
        
        if(list is null || cornerRadius is { TopLeft: 0, TopRight: 0 } and { BottomLeft: 0, BottomRight: 0 })
            return;

        if (list.ElementAtOrDefault(0) == Element)
        {
            Layout.Layout.SetCornerRadius(Element, new CornerRadius(cornerRadius.TopLeft, cornerRadius.TopRight, 0, 0));
        }
        else if(list.ElementAtOrDefault(list.Count - 1) == Element)
        {
            Layout.Layout.SetCornerRadius(Element, new CornerRadius(0, 0, cornerRadius.BottomLeft, cornerRadius.BottomRight));
        }
    }

    protected override partial void OnDetached()
    {
        
    }
}