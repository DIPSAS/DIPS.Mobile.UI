using DIPS.Mobile.UI.Sizes.Sizes;

namespace DIPS.Mobile.UI.Resources.Sizes
{
    [ContentProperty(nameof(SizeName))]
    public class SizesExtension : IMarkupExtension<int>
    {
        public SizeName SizeName { get; set; }

        public static int GetSize(string sizeName)
        {
            var sizes = new Sizes();
            if (!sizes.ContainsKey(sizeName))
            {
                return 0;
            }

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

        public int ProvideValue(IServiceProvider serviceProvider)
        {
            return GetSize(SizeName);
        }

        object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider)
        {
            return (this as IMarkupExtension<int>).ProvideValue(serviceProvider);
        }
    }
}