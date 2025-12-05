using System.Collections.ObjectModel;
using DIPS.Mobile.UI.MVVM;
using DIPS.Mobile.UI.MVVM.Commands;

namespace Components.Samples.GallerySample;

public class GallerySampleViewModel : ViewModel
{
    private int m_currentPosition;
    
    public GallerySampleViewModel()
    {
        PickPhotoCommand = new AsyncCommand(PickPhotos);
        
        // Add some sample images for demonstration
        Images.Add(ImageSource.FromFile("dotnet_bot.png"));
    }

    public ObservableCollection<ImageSource> Images { get; } = new();

    public int CurrentPosition
    {
        get => m_currentPosition;
        set
        {
            RaiseWhenSet(ref m_currentPosition, value);
            RaisePropertyChanged(nameof(StatusText));
        }
    }

    public string StatusText => $"Image {CurrentPosition + 1} of {Images.Count}";
    
    public bool HasImages => Images.Count > 0;
    
    public bool HasNoImages => !HasImages;
    
    public AsyncCommand PickPhotoCommand { get; }

    private async Task PickPhotos()
    {
        try
        {
            var result = await MediaPicker.Default.PickPhotoAsync();

            if (result != null)
            {
                // Read the stream into memory before creating ImageSource
                using var stream = await result.OpenReadAsync();
                var memoryStream = new MemoryStream();
                await stream.CopyToAsync(memoryStream);
                memoryStream.Position = 0;
                
                // Create ImageSource from the in-memory stream
                var imageSource = ImageSource.FromStream(() => new MemoryStream(memoryStream.ToArray()));
                Images.Add(imageSource);
                
                // Update to show last added image
                CurrentPosition = Images.Count - 1;
                
                RaisePropertyChanged(nameof(HasImages));
                RaisePropertyChanged(nameof(HasNoImages));
                RaisePropertyChanged(nameof(StatusText));
            }
        }
        catch (Exception ex)
        {
            // Handle permission denied or other errors
            await Application.Current!.MainPage!.DisplayAlert("Error", $"Unable to pick photo: {ex.Message}", "OK");
        }
    }
}
