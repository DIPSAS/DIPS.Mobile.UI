namespace DIPS.Mobile.UI.Resources.Sizes
{
    [ContentProperty(nameof(UniformSize))]
    public class ThicknessExtension : IMarkupExtension<Thickness>
    {
        
        /// <summary>The thickness of the left side of a rectangle.</summary>
        /// <value>To be added.</value>
        /// <remarks>To be added.</remarks>
        public SizeName Left { get; set; }

        /// <summary>The thickness of the top of a rectangle.</summary>
        /// <value>To be added.</value>
        /// <remarks>To be added.</remarks>
        public SizeName Top { get; set; }

        /// <summary>The thickness of the right side of a rectangle.</summary>
        /// <value>To be added.</value>
        /// <remarks>To be added.</remarks>
        public SizeName Right { get; set; }

        /// <summary>The thickness of the bottom of a rectangle.</summary>
        /// <value>To be added.</value>
        /// <remarks>To be added.</remarks>
        public SizeName Bottom { get; set; }
        
        public SizeName? UniformSize { get; set; }
        

        public Thickness ProvideValue(IServiceProvider serviceProvider)
        {
            return UniformSize != null ? new Thickness(Sizes.GetSize(UniformSize.Value)) : new Thickness(Sizes.GetSize(Left), Sizes.GetSize(Top), Sizes.GetSize(Right), Sizes.GetSize(Bottom));
        }
        

        object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider)
        {
            return (this as IMarkupExtension<Thickness>).ProvideValue(serviceProvider);
        }
    }
}