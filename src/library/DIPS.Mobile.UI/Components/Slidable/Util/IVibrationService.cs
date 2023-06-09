namespace DIPS.Mobile.UI.Components.Slidable.Util
{
    internal interface IVibrationService
    {
        void Vibrate(int duration);

        void Click();

        void HeavyClick();

        void DoubleClick();

        void Error();
        
        void Success();
        
        IPlatformFeedbackGenerator Generate();
    }

    internal interface IPlatformFeedbackGenerator
    {
        void SelectionChanged();

        void Prepare();

        void Release();
    }
}