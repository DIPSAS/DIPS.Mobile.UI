using System.Threading.Tasks;

namespace DIPS.Mobile.UI
{
    public static class DUI
    {
        #nullable disable
        public static ILibraryService Library { get; internal set; }
        #nullable enable
        public static void Init(){}
    }

    public interface ILibraryService
    {
        /// <summary>
        /// Removes anything that is located out on top of the current page for people to use. Examples of this is bottom sheets, date time pickers etc.
        /// </summary>
        /// <returns></returns>
        Task RemoveViewsLocatedOnTopOfPage();
    }
}
