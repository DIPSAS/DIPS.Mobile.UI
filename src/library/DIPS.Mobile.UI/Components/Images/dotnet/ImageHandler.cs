using System.ComponentModel;
using DIPS.Mobile.UI.Exceptions;
using Microsoft.Maui.Handlers;

// ReSharper disable once CheckNamespace
namespace DIPS.Mobile.UI.Components.Images;

public partial class ImageHandler : ViewHandler<Image, View>
{
    private void IOSPropertiesOnPropertyChanged(object? sender, PropertyChangedEventArgs e) => throw new NotSupportedException();

    private static partial void TrySetSystemImage(ImageHandler imageHandler, Image image) => throw new Only_Here_For_UnitTests();

    private static partial void TrySetImageColor(ImageHandler imageHandler, Image image) => throw new Only_Here_For_UnitTests();

    protected override View CreatePlatformView() => throw new Only_Here_For_UnitTests();
}