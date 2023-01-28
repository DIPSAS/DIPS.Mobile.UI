using System.Threading.Tasks;

namespace DIPS.Mobile.UI.Components.BottomSheets
{
    internal static class BottomSheetService
    {
        internal static IBottomSheetService? Instance { get; set; }
    }
    
    public interface IBottomSheetService
    {
        Task PushBottomSheet(BottomSheet view);
    }
}