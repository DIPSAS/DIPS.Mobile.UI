using System.ComponentModel;
using System.Globalization;
using DIPS.Mobile.UI.Effects.Layout.Divider;

namespace DIPS.Mobile.UI.Effects.Layout;

public partial class Layout : RoutingEffect
{
    public static CornerRadius GetCornerRadius(BindableObject view)
    {
        return (CornerRadius)view.GetValue(CornerRadiusProperty);
    }

    /// <summary>
    /// Sets the corner radius
    /// </summary>
    /// <remarks>The highest set corner radius will be set for all corners that are defined</remarks>
    /// <example>If TopLeft = 8, and TopRight = 4, TopRight will be set to 8</example>
    public static void SetCornerRadius(BindableObject view, CornerRadius cornerRadius)
    {
        view.SetValue(CornerRadiusProperty, cornerRadius);
    }
    
    public static bool? GetAutoCornerRadius(BindableObject view)
    {
        return (bool?)view.GetValue(AutoCornerRadiusProperty);
    }

    /// <summary>
    /// Sets automatic uniform corner radius
    /// <remarks>If the View in question is a <see cref="CollectionView"/>, the first and last element will get corner radius</remarks> 
    /// </summary>
    public static void SetAutoCornerRadius(BindableObject view, bool autoCornerRadius)
    {
        view.SetValue(AutoCornerRadiusProperty, autoCornerRadius);
    }
    
    [TypeConverter(typeof(DividerConfiguratorTypeConverter))]
    public static DividerConfigurator? GetAutoDivider(BindableObject view)
    {
        return (DividerConfigurator?)view.GetValue(AutoDividerProperty);
    }

    /// <summary>
    /// Sets automatic divider between elements in a <see cref="CollectionView"/> or <see cref="BindableLayout"/> together with <see cref="VerticalStackLayout"/>
    /// </summary>
    public static void SetAutoDivider(BindableObject view, DividerConfigurator? configurator)
    {
        view.SetValue(AutoDividerProperty, configurator);
    }

    private static void OnLayoutPropertiesChanged(BindableObject bindable, object oldValue, object? newValue)
    {
        if (bindable is not View view)
            return;

        if (newValue is null)
        {
            RemoveEffects(view);
            return;
        }
        
        // Refresh
        RemoveEffects(view);
        view.Effects.Add(new Layout());
        
    }
    
    private static void RemoveEffects(Element view)
    {
        while (view.Effects.Any(e => e is Layout))
        {
            view.Effects.Remove(view.Effects.FirstOrDefault(e => e is Layout));
        }
    }
}

public class DividerConfiguratorTypeConverter : TypeConverter
{
    public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
    {
        if (value is string stringValue)
        {
            if(stringValue.Equals("true", StringComparison.OrdinalIgnoreCase))
            {
                return new DividerConfigurator();
            }

            if(stringValue.Equals("false", StringComparison.OrdinalIgnoreCase))
            {
                return null;
            }
        }
        
        if(value is DividerConfigurator)
        {
            return value;
        }
        
        throw new ArgumentException("Invalid value passed into AutoDivider, use either 'true', 'false' or a `DividerConfigurator`");
    }
}