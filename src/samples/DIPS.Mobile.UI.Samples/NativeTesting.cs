using System.Threading.Tasks;

namespace DIPS.Mobile.UI.Samples
{
    public static class NativeTesting
    {
        public static INativeTesting Instance { get; set; }
    }

    public interface INativeTesting
    {
        Task OpenMaterialDialog();
    }
}