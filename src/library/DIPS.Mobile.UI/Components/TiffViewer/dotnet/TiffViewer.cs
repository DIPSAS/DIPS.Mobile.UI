namespace DIPS.Mobile.UI.Components.TiffViewer;

public partial class TiffViewer
{
    private partial async Task<int> LoadTiffPagesAsync(CancellationToken? cancellationToken = null)
    {
        await Task.CompletedTask;
        return 1;
    }

    private partial async Task<ImageSource?> GetPageImageSourceAsync(int pageIndex, CancellationToken? cancellationToken = null)
    {
        await Task.CompletedTask;
        return null;
    }
}
