namespace DIPS.Mobile.UI.Resources.Sizes
{
    [ContentProperty(nameof(SizeName))]
    public class SizesExtension : IMarkupExtension
    {
        public SizeName SizeName { get; set; }
        
        /// <summary>
        /// Determines what data type the size extension should return
        /// </summary>
        /// <remarks>This is automatically detected if the extension is used on a <see cref="BindableProperty"/></remarks>
        public SizeDatatype Datatype { get; set; }

        public static int GetSize(string sizeName)
        {
            var sizes = new Sizes();
            if (!sizes.TryGetValue(sizeName, out var value))
            {
                return 0;
            }

            if (value is int size)
            {
                return size;
            }

            return 0;
        }

        public static int GetSize(SizeName sizeName)
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
                SizeDatatype.Double => (double)size,
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