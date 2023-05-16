using System.ComponentModel;
using DIPS.Mobile.UI.Exceptions;
using Microsoft.Maui.Handlers;

// ReSharper disable once CheckNamespace
namespace DIPS.Mobile.UI.Components.Images;

public partial class NativeIconHandler : ViewHandler<NativeIcon, View>
{
    private void IOSPropertiesOnPropertyChanged(object? sender, PropertyChangedEventArgs e) => throw new NotSupportedException();

    private static partial void TrySetSystemImage(NativeIconHandler nativeIconHandler, NativeIcon nativeIcon) => throw new Only_Here_For_UnitTests();

    private static partial void TrySetImageColor(NativeIconHandler nativeIconHandler, NativeIcon nativeIcon) => throw new Only_Here_For_UnitTests();

    protected override View CreatePlatformView() => throw new Only_Here_For_UnitTests();
}