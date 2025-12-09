namespace Components.Samples.TiffViewerSample;

public partial class TiffViewerSample
{
    public TiffViewerSample()
    {
        InitializeComponent();
        LoadTiffFile();
    }

    private async void LoadTiffFile()
    {
        try
        {
            // Load the TIFF file from Resources/Raw
            using var stream = await FileSystem.OpenAppPackageFileAsync("multipage_tif_example.tif");
            using var memoryStream = new MemoryStream();
            await stream.CopyToAsync(memoryStream);
            var bytes = memoryStream.ToArray();

            // Set the byte array directly as Source
            TiffViewer.Source = bytes;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error loading TIFF file: {ex.Message}");
        }
    }
}
