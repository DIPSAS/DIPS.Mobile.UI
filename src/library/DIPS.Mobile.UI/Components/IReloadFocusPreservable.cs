namespace DIPS.Mobile.UI.Components;

internal interface IReloadFocusPreservable
{
    bool HasPreservedFocus { get; }

    bool TryRestoreFocus();
}