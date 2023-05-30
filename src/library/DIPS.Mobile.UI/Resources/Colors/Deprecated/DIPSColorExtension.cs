namespace DIPS.Mobile.UI.Resources.Colors.Deprecated
{
    [Obsolete($"This API is deprecated, please use dui:Colors markup extension instead",true)]
    public class DIPSColorExtension : IMarkupExtension<Color>
    {
        [Obsolete($"This API is deprecated, please use dui:Colors markup extension instead",true)]
        public Theme.Identifier Theme
        {
            get;
            set;
        }
        
        [Obsolete($"This API is deprecated, please use dui:Colors markup extension instead",true)]
        public StatusColorPalette.Identifier StatusColorPalette
        {
            get;
            set;
        }
        
        [Obsolete($"This API is deprecated, please use dui:Colors markup extension instead",true)]
        public ColorPalette.Identifier ColorPalette
        {
            get;
            set;
        }

        object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider)
        {
            return ProvideValue(serviceProvider);
        }

        public Color ProvideValue(IServiceProvider serviceProvider)
        {
            return Microsoft.Maui.Graphics.Colors.Transparent;
        }
    }
}
