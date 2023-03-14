using System.Threading.Tasks;

namespace DIPS.Mobile.UI.Components.BottomSheets
{
    /// <summary>
    /// The bottom sheet service used to display a bottom sheet for people to see.
    /// </summary>
    public static class BottomSheetService
    {
        #nullable disable
        /// <summary>
        /// The instance set by the platform the app is running under.
        /// </summary>
        public static IBottomSheetService Current { get; internal set; }
        #nullable enable
    }
    
    public interface IBottomSheetService
    {
        /// <summary>
        /// Presents a bottom sheet for people to see.
        /// </summary>
        /// <param name="view">The view to display inside the bottom sheet.</param>
        /// <returns></returns>
        Task OpenBottomSheet(BottomSheet view);

        /// <summary>
        /// Closes the current presented bottom sheet.
        /// </summary>
        /// <returns></returns>
        Task CloseCurrentBottomSheet();

        bool IsBottomSheetOpen();
    }
}