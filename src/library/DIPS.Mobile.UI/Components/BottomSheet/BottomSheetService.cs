using System.Threading.Tasks;
using Xamarin.Forms;

namespace DIPS.Mobile.UI.Components.BottomSheet
{
    internal static class BottomSheetService
    {
        internal static IBottomSheetService? Instance { get; set; }
    }
    
    public interface IBottomSheetService
    {
        Task<IBottomSheet> PushBottomSheet(BottomSheetView view);
    }

    public interface IBottomSheet
    {
        /// <summary>
        /// Opens a bottom sheet.
        /// </summary>
        /// <returns></returns>
        public Task Open();
        /// <summary>
        /// Closes the current sheet
        /// </summary>
        /// <returns></returns>
        public Task Close();
    }
}