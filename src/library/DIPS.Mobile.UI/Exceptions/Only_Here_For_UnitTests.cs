namespace DIPS.Mobile.UI.Exceptions;

// ReSharper disable once InconsistentNaming
public class Only_Here_For_UnitTests : Exception
{
    public static void Throw() => throw new Only_Here_For_UnitTests();
}