namespace DIPS.Mobile.UI.Components.BottomSheets
{
    public static partial class BottomSheetService
    {
        public static partial Task OpenBottomSheet(BottomSheet bottomSheet)
        {
            return Task.CompletedTask;
        }
        
        public static partial Task CloseCurrentBottomSheet()
        {
            return Task.CompletedTask;
        }

        public static partial bool IsBottomSheetOpen()
        {
            return true;
        }
    }
}