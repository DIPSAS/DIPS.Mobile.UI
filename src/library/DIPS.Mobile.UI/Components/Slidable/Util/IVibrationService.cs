namespace DIPS.Mobile.UI.Components.Slidable.Util
{
    public interface IPlatformFeedbackGenerator
    {
        void SelectionChanged();

        void Prepare();

        void Release();
    }
}