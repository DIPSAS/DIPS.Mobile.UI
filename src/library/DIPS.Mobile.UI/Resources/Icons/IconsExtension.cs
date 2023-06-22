namespace DIPS.Mobile.UI.Resources.Icons;

[ContentProperty(nameof(IconName))]
public class IconsExtension : IMarkupExtension<ImageSource>
{
    public IconName IconName { get; set; }

    public ImageSource ProvideValue(IServiceProvider serviceProvider)
    {
        if (!IconResources.Icons.TryGetValue(IconName.ToString(), out var value))
        {
            return string.Empty;
        }

        return value;
    } 

    object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider)
    {
        return ProvideValue(serviceProvider);
    }
}