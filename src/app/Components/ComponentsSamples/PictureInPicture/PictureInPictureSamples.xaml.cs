namespace Components.ComponentsSamples.PictureInPicture;

public partial class PictureInPictureSamples
{
    public PictureInPictureSamples()
    {
        InitializeComponent();
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        if (BindingContext is PictureInPictureSamplesViewModel vm)
        {
            vm.Unsubscribe();
        }
    }
}
