using System.Windows.Input;

namespace DIPS.Mobile.UI.Components.BottomSheets
{
    [ContentProperty(nameof(BottomSheetType))]
    public class OpenBottomSheetCommandExtension : IMarkupExtension<ICommand>
    {
        public Type? BottomSheetType { get; set; }
        
        public DataTemplate? TheBottomSheet { get; set; }

        public ICommand ProvideValue(IServiceProvider serviceProvider)
        {
            return new Command(() =>
            {
                var potentialBottomSheet = TheBottomSheet?.CreateContent();
                switch (potentialBottomSheet)
                {
                    case not BottomSheet when BottomSheetType != null:
                        {
                            var activatedObject = Activator.CreateInstance(BottomSheetType);
                            if (activatedObject is BottomSheet sheet)
                            {
                                potentialBottomSheet = sheet;
                            }

                            break;
                        }
                    case BottomSheet sheet:
                        potentialBottomSheet = sheet;
                        break;
                }
                
                if (potentialBottomSheet is BottomSheet theBottomSheet)
                {
                    BottomSheetService.OpenBottomSheet(theBottomSheet);
                }
                
            });
        }

        object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider)
        {
            return ProvideValue(serviceProvider);
        }
    }
}