using DIPS.Mobile.UI.Exceptions;

namespace DIPS.Mobile.UI.API.PictureInPicture;

public static partial class PipService
{
    public static partial bool IsSupported => throw new Only_Here_For_UnitTests();

    public static partial void Enter() => throw new Only_Here_For_UnitTests();

    public static partial void Enter(int ratioWidth, int ratioHeight) => throw new Only_Here_For_UnitTests();
}
