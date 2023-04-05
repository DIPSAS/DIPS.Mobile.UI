using DIPS.Mobile.UI.Sizes.Sizes;

namespace DIPS.Mobile.UI.Resources.Sizes
{
    [ContentProperty(nameof(UniformSize))]
    public class CornerRadiusExtension : IMarkupExtension<CornerRadius>
    {
        
        
        /// <summary>Gets the radius of the top left corner.</summary>
        /// <value>The radius of the top left corner.</value>
        /// <remarks>To be added.</remarks>
        public SizeName TopLeft { get; set; }

        /// <summary>Gets the radius of the top right corner.</summary>
        /// <value>The radius of the top right corner.</value>
        /// <remarks>To be added.</remarks>
        public SizeName TopRight { get; set; }

        /// <summary>Gets the radius of the top left corner.</summary>
        /// <value>The radius of the top left corner.</value>
        /// <remarks>To be added.</remarks>
        public SizeName BottomLeft { get; set; }

        /// <summary>Gets the radius of the bottom right corner.</summary>
        /// <value>The radius of the bottom right corner.</value>
        /// <remarks>To be added.</remarks>
        public SizeName BottomRight { get; set; }
        
        public SizeName? UniformSize { get; set; }
        

        public CornerRadius ProvideValue(IServiceProvider serviceProvider)
        {
            return UniformSize != null ? new CornerRadius(Sizes.GetSize(UniformSize.Value)) : new CornerRadius(Sizes.GetSize(TopLeft), Sizes.GetSize(TopRight), Sizes.GetSize(BottomLeft), Sizes.GetSize(BottomRight));
        }
        

        object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider)
        {
            return (this as IMarkupExtension<CornerRadius>).ProvideValue(serviceProvider);
        }
    }
}