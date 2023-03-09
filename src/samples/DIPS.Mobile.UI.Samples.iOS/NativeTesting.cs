using System.Threading.Tasks;

namespace DIPS.Mobile.UI.Samples.iOS
{
    public class NativeTesting : INativeTesting
    {
        public Task OpenMaterialDialog()
        {
            return Task.CompletedTask;
        }
    }
}