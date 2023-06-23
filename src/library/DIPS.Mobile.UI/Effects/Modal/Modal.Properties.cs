namespace DIPS.Mobile.UI.Effects.Modal;

public partial class Modal
{
    public static readonly BindableProperty HasNavBarProperty = BindableProperty.CreateAttached("HasNavBar",
        typeof(bool),
        typeof(Modal),
        null,
        propertyChanged: OnHasNavBarChanged);
}