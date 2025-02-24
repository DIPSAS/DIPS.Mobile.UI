namespace DIPS.Mobile.UI.Resources.Sizes
{
    [RequireService([typeof(IProvideValueTarget)])]
    [ContentProperty(nameof(SizeName))]
    public class SizesExtension : IMarkupExtension
    {
        public SizeName SizeName { get; set; }
        
        /// <summary>
        /// Determines what data type the size extension should return
        /// </summary>
        /// <remarks>This is automatically detected if the extension is used on a <see cref="BindableProperty"/></remarks>
        public SizeDatatype Datatype { get; set; }

        public static double GetSize(string sizeName)
        {
            if (!SizeResources.Sizes.TryGetValue(sizeName, out var value))
            {
                return 0;
            }

            return value;
        }

        public static double GetSize(SizeName sizeName)
        {
            return GetSize(sizeName.ToString());
        }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Datatype == SizeDatatype.Unset)
            {
                //Try to auto detect return type based on XAML information
                var provideValueTarget = serviceProvider.GetService(typeof(IProvideValueTarget)) as IProvideValueTarget;
                if (provideValueTarget?.TargetProperty is BindableProperty bindableProperty)
                {
                    if (bindableProperty.ReturnType == typeof(double))
                    {
                        Datatype = SizeDatatype.Double;
                    }
                    else if (bindableProperty.ReturnType == typeof(float))
                    {
                        Datatype = SizeDatatype.Float;
                    }
                    else if (bindableProperty.ReturnType == typeof(CornerRadius))
                    {
                        Datatype = SizeDatatype.CornerRadiusUniformedSized;
                    }
                    else if (bindableProperty.ReturnType == typeof(Thickness))
                    {
                        Datatype = SizeDatatype.ThicknessUniformedSized;
                    }
                } 
            }

            var size = GetSize(SizeName);
            return Datatype switch
            {
                SizeDatatype.Integer => size,
                SizeDatatype.Double => size,
                SizeDatatype.Float => (float)size,
                SizeDatatype.CornerRadiusUniformedSized => new CornerRadius(size),
                SizeDatatype.ThicknessUniformedSized => new Thickness(size),
                _ => size
            };
        }
    }

    public enum SizeDatatype
    {
        Unset=0,
        Integer,
        Double,
        Float,
        CornerRadiusUniformedSized,
        ThicknessUniformedSized,
    }
}