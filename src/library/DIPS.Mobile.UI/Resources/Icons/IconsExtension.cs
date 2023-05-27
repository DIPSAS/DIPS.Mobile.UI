namespace DIPS.Mobile.UI.Resources.Icons;

[ContentProperty(nameof(IconName))]
public class IconsExtension : IMarkupExtension<ImageSource>
{
    public IconName IconName { get; set; }
    public ImageSource ProvideValue(IServiceProvider serviceProvider) => Icons.GetIcon(IconName);

    object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider)
    {
        return ProvideValue(serviceProvider);
    }
}